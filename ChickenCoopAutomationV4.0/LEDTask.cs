using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace ChickenCoopAutomation
{
    public class LEDTask : Task
    {
        private OutputPort outputGreenLED;
        private OutputPort outputYellowLED;
        private OutputPort outputRedLED;

        public LEDTask(FEZ_Pin.Digital pinGreenLED, 
                       FEZ_Pin.Digital pinYellowLED,
                       FEZ_Pin.Digital pinRedLED) : base()
        {
            outputGreenLED = new OutputPort((Cpu.Pin)pinGreenLED, true);
            outputYellowLED = new OutputPort((Cpu.Pin)pinYellowLED, true);
            outputRedLED = new OutputPort((Cpu.Pin)pinRedLED, true);
        }

        protected override void DoWork()
        {
            while (true)
            {
                UpdateLEDs();
                Thread.Sleep(500);
            }
        }

        private void UpdateLEDs()
        {
            // reset the LEDs, we will turn on the one needed below
            outputGreenLED.Write(true);
            outputYellowLED.Write(true);
            outputRedLED.Write(true);

            switch (CoopData.Instance.DoorState)
            {
                case CoopData.DoorStateEnum.Unknown:
                    outputYellowLED.Write(false);
                    break;
                case CoopData.DoorStateEnum.Open:
                    outputGreenLED.Write(false);
                    break;
                case CoopData.DoorStateEnum.Closed:
                    outputRedLED.Write(false);
                    break;
            }
        }
    }
}
