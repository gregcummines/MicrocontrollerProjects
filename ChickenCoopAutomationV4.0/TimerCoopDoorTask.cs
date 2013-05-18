using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// This class manages the automatic and manual operation of the coop door
    /// </summary>
    public class TimerCoopDoorTask : Task
    {
        private enum DoorOperatingMode { Manual, Automatic };

        /////////////////////////////////////////////////////
        // ports that control the motor
        /////////////////////////////////////////////////////
        // port 1 & 2 toggle to form a forward-reverse mechanism
        // for the motor.
        private static OutputPort portOutMotorPolarityA;
        private static OutputPort portOutMotorPolarityB;

        // These ports are hooked up to reed relays that are activated with a magnet
        // on the door. There is a reed switch to indicate if the door is open, and
        // one to indicate if the door is closed
        private static InputPort portInputDoorClosed;
        private static InputPort portInputDoorOpen;

        private static DoorOperatingMode mode;
        private static AnalogIn lightSensor;

        private static object lockOpenClose = new object();
        
        /// <summary>
        /// Buttons to manually operate the motor to put the door up or down
        /// </summary>
        private static InterruptPort portButtonForDoorUp;
        private static InterruptPort portButtonForDoorDown;

        private static TimeSpan DAYTIME_BEGINNING = new TimeSpan(6, 0, 0);
        private static TimeSpan NIGHTTIME_BEGINNING = new TimeSpan(18, 0, 0);

        private FEZ_Pin.Digital _portOutMotorPolarityAPin;
        private FEZ_Pin.Digital _portOutMotorPolarityBPin;
        private FEZ_Pin.Digital _portInputDoorOpenPin;
        private FEZ_Pin.Digital _portInputDoorClosedPin;
        private FEZ_Pin.Interrupt _portInputButtonForDoorUpPin;
        private FEZ_Pin.Interrupt _portInputButtonForDoorDownPin;        

        // maximum amount of time to have the motor on, in case switch detect fails
        private const int MOTOR_ON_TIMEOUT = 14;

        /// <summary>
        /// Direction of motor travel
        /// </summary>
        private enum DirectionEnum
        {
            Forward = 0,
            Reverse = 1
        }

        public TimerCoopDoorTask(FEZ_Pin.Digital portOutMotorPolarityAPin,
                            FEZ_Pin.Digital portOutMotorPolarityBPin,
                            FEZ_Pin.Digital portInputDoorOpenPin,
                            FEZ_Pin.Digital portInputDoorClosedPin,
                            FEZ_Pin.Interrupt portInputButtonForDoorUpPin,
                            FEZ_Pin.Interrupt portInputButtonForDoorDownPin
                            ) : base()
        {
            _portOutMotorPolarityAPin = portOutMotorPolarityAPin;
            _portOutMotorPolarityBPin = portOutMotorPolarityBPin;
            _portInputDoorOpenPin = portInputDoorOpenPin;
            _portInputDoorClosedPin = portInputDoorClosedPin;
            _portInputButtonForDoorUpPin = portInputButtonForDoorUpPin;
            _portInputButtonForDoorDownPin = portInputButtonForDoorDownPin; 
        }

        protected override void DoWork()
        {
            // initialize the output ports
            portOutMotorPolarityA = new OutputPort((Cpu.Pin)_portOutMotorPolarityAPin, true);
            portOutMotorPolarityB = new OutputPort((Cpu.Pin)_portOutMotorPolarityBPin, true);

            // initialize the input ports, using a glitch filter, and pull up resistors
            portInputDoorOpen = new InputPort((Cpu.Pin)_portInputDoorOpenPin, true, Port.ResistorMode.PullUp);
            portInputDoorClosed = new InputPort((Cpu.Pin)_portInputDoorClosedPin, true, Port.ResistorMode.PullUp);

            // initialize the button ports
            portButtonForDoorUp = new InterruptPort((Cpu.Pin)_portInputButtonForDoorUpPin, true, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
            portButtonForDoorUp.OnInterrupt += new NativeEventHandler(portButtonForDoorUp_OnInterrupt);
            portButtonForDoorDown = new InterruptPort((Cpu.Pin)_portInputButtonForDoorDownPin, true, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
            portButtonForDoorDown.OnInterrupt += new NativeEventHandler(portButtonForDoorDown_OnInterrupt);

            lightSensor = new AnalogIn(AnalogIn.Pin.Ain0);

            mode = DoorOperatingMode.Automatic;

            // start the loop
            while (true)
            {
                // The following if statements will only operate the motor if the switch sensors
                // are connected to the door and functioning, indicating that the door is in the 
                // open or closed position.

                // update the door status
                UpdateDoorStatus();

                // If we are in Automatic mode, operate the door automatically
                // Note that we go into Manual mode when the up/down push buttons are used
                if (mode == DoorOperatingMode.Automatic)
                {
                    // If it is daytime and the door is not open
                    if (IsDaytime() && !IsDoorOpen())
                    {
                        OpenDoor();
                    }
                    // Otherwise if it's nighttime and the door is not closed, close it
                    else if (IsNightime() && !IsDoorClosed())
                    {
                        CloseDoor();
                    }
                    // Otherwise do nothing, and continue to check the above at regular intervals
                    else
                    {
                        // nothing else to do
                    }
                }
                // since we may have opened or closed the door, update the door status
                UpdateDoorStatus();

                base.Sleep(30000);
            }
        }

        private static void UpdateDoorStatus()
        {
            if (IsDoorOpen())
            {
                // door is open, indicate it
                CoopData.Instance.DoorState = CoopData.DoorStateEnum.Open;
            }
            else if (IsDoorClosed())
            {
                // door is closed, indicate it
                CoopData.Instance.DoorState = CoopData.DoorStateEnum.Closed;
            }
            else
            {
                // sensors are not detecting door open or closed
                CoopData.Instance.DoorState = CoopData.DoorStateEnum.Unknown;
            }
        }

        private static void portButtonForDoorUp_OnInterrupt(uint port, uint state, DateTime time)
        {
            Debug.Print(port.ToString() + " - " + state.ToString() + " - " + time.ToString());
            if (state == 0)
            {
                // go into manual mode until reboot
                mode = DoorOperatingMode.Manual;

                // button is pressed
                // start the motor in the direction to open the door
                OpenDoor();
            }

            UpdateDoorStatus();
        }

        private static void portButtonForDoorDown_OnInterrupt(uint port, uint state, DateTime time)
        {
            Debug.Print(port.ToString() + " - " + state.ToString() + " - " + time.ToString());
            if (state == 0)
            {
                // Go into manual mode until reboot
                mode = DoorOperatingMode.Manual;

                CloseDoor();
            }

            UpdateDoorStatus();
        }

        private static void StartMotor(DirectionEnum dir)
        {
            bool bDir = true;
            if (dir == DirectionEnum.Forward)
                bDir = false;

            portOutMotorPolarityA.Write(bDir);
            portOutMotorPolarityB.Write(!bDir);
        }

        private static void StopMotor()
        {
            // turn off the motor
            portOutMotorPolarityA.Write(true);
            portOutMotorPolarityB.Write(true);
        }

        private static bool IsDoorOpen()
        {
            // get the state of the reed switch that would
            // indicate that the door is in the open position 
            // we have a pullup resistor on this port and are using ground for detect
            return !portInputDoorOpen.Read();
        }

        private static bool IsDoorClosed()
        {
            // get the state of the reed switch that would
            // indicate that the door is in the closed position 
            // we have a pullup resistor on this port and are using ground for detect
            return !portInputDoorClosed.Read();
        }

        private static bool IsDaytime()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            if ((now > DAYTIME_BEGINNING) && (now < NIGHTTIME_BEGINNING))
                return true;
            else
                return false;
        }

        private static bool IsNightime()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            if ((now > NIGHTTIME_BEGINNING) || (now < DAYTIME_BEGINNING))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to open the door. This blocks all other activity on the microcontroller
        /// </summary>
        private static void OpenDoor()
        {
            lock (lockOpenClose)
            {
                CoopData.Instance.DoorState = CoopData.DoorStateEnum.Unknown;

                // check to make sure the door isn't already open
                if (IsDoorOpen())
                    return;

                // start the motor in the direction to open the door
                StartMotor(DirectionEnum.Forward);

                // setup some variables for the timeout
                TimeSpan timeout = new TimeSpan(0, 0, MOTOR_ON_TIMEOUT);   // x seconds
                TimeSpan startTime = DateTime.Now.TimeOfDay;

                while (true)
                {
                    // door is open, stop the motor and break out of here
                    if (IsDoorOpen())
                    {
                        StopMotor();
                        break;
                    }

                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    // If the current time reaches the future timeout time, abort
                    if (currentTime > (startTime + timeout))
                    {
                        StopMotor();
                        // we shouldn't get here, but timeout exceeded, aborting
                        break;
                    }

                    Thread.Sleep(5);
                }
            }
        }

        /// <summary>
        /// Method to close the door. This blocks all other activity on the microcontroller
        /// </summary>
        private static void CloseDoor()
        {
            lock (lockOpenClose)
            {
                CoopData.Instance.DoorState = CoopData.DoorStateEnum.Unknown;

                // check to make sure the door isn't already closed
                if (IsDoorClosed())
                    return;

                // start the motor in the direction to open the door
                StartMotor(DirectionEnum.Reverse);

                // setup some variables for the timeout
                TimeSpan timeout = new TimeSpan(0, 0, MOTOR_ON_TIMEOUT);   // 5 seconds
                TimeSpan startTime = DateTime.Now.TimeOfDay;

                while (true)
                {
                    // door is closed, stop the motor and break out of here
                    if (IsDoorClosed())
                    {
                        StopMotor();
                        break;
                    }

                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    // If the current time reaches the future timeout time, abort
                    if (currentTime > (startTime + timeout))
                    {
                        StopMotor();
                        // we shouldn't get here, but timeout exceeded, aborting
                        break;
                    }

                    Thread.Sleep(5);
                }
            }
        }
    }
}
