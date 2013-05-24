using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace ChickenCoopAutomation
{
    public class LEDTask : Task
    {
        private OutputPort _outputGreenLED;
        private OutputPort _outputYellowLED;
        private OutputPort _outputRedLED;

        private FEZ_Pin.Digital _pinGreenLED;
        private FEZ_Pin.Digital _pinYellowLED;
        private FEZ_Pin.Digital _pinRedLED;

        public LEDTask(FEZ_Pin.Digital pinGreenLED, 
                       FEZ_Pin.Digital pinYellowLED,
                       FEZ_Pin.Digital pinRedLED) : base()
        {
            _pinGreenLED = pinGreenLED;
            _pinYellowLED = pinYellowLED;
            _pinRedLED = pinRedLED;            
        }

        protected override void DoWork()
        {
            _outputGreenLED = new OutputPort((Cpu.Pin)_pinGreenLED, true);
            _outputYellowLED = new OutputPort((Cpu.Pin)_pinYellowLED, true);
            _outputRedLED = new OutputPort((Cpu.Pin)_pinRedLED, true);

            while (true)
            {
                UpdateLEDs();
                Thread.Sleep(500);
            }
        }

        private void UpdateLEDs()
        {
            // reset the LEDs, we will turn on the one needed below
            _outputGreenLED.Write(true);
            _outputYellowLED.Write(true);
            _outputRedLED.Write(true);

            switch (CoopData.Instance.DoorState)
            {
                case CoopData.DoorStateEnum.Unknown:
                    _outputYellowLED.Write(false);
                    break;
                case CoopData.DoorStateEnum.Open:
                    _outputGreenLED.Write(false);
                    break;
                case CoopData.DoorStateEnum.Closed:
                    _outputRedLED.Write(false);
                    break;
            }
        }
    }
}
