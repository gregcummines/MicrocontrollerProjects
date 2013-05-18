using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// This class simply monitors a temperature sensor and turns on a device to heat
    /// </summary>
    public class CoopHeaterTask : Task
    {
        private const int CHECK_INTERVAL = 30000;   // amount of time in mS before checking agian
        private int _tempSetPointF;
        private FEZ_Pin.Digital _pinHeaterOutput;
        private OutputPort _portOutHeater;

        public CoopHeaterTask(FEZ_Pin.Digital pinHeaterOutput, int tempSetPointF) : base()
        {
            _tempSetPointF = tempSetPointF;
            _pinHeaterOutput = pinHeaterOutput;
        }

        protected override void DoWork()
        {
            _portOutHeater = new OutputPort((Cpu.Pin)_pinHeaterOutput, true);

            Thread.Sleep(1500); // wait for sensors to start

            // Monitor the temp and turn on a heater if the temp gets too cold
            // Do this as long as the microcontroller is powered up and running
            while (true)
            {
                if (CoopData.Instance.CoopTemperature != Thermometer.InvalidData)
                {
                    Debug.Print("Temp for coop:" + CoopData.Instance.CoopTemperature.ToString("F0") + "F");

                    if (CoopData.Instance.CoopTemperature > 90)
                    {
                        // we never want the heater on if the above 90 degrees! (failsafe)
                        TurnOffHeater();
                    }
                    else
                    {
                        if (CoopData.Instance.CoopTemperature < _tempSetPointF)
                        {
                            TurnOnHeater();
                        }
                        else
                        {
                            TurnOffHeater();
                        }
                    }
                }
                else
                {
                    // If we get invalid data, turn off the heater
                    TurnOffHeater();
                }
                base.Sleep(CHECK_INTERVAL);    // sleep for 30 seconds, and then check again
            }
        }

        private void TurnOnHeater()
        {
            _portOutHeater.Write(false);
        }

        private void TurnOffHeater()
        {
            _portOutHeater.Write(true);
        }
    }
}
