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
    public class WaterHeaterTask : Task
    {
        private const int CHECK_INTERVAL = 30000;   // amount of time in mS before checking agian
        private const int HIGH_TEMP = 105;          // can't heat water above this temperature

        private int _tempSetPointF;
        private FEZ_Pin.Digital _pinHeaterOutput;
        private OutputPort _portOutHeater;

        public WaterHeaterTask(FEZ_Pin.Digital pinHeaterOutput, int tempSetPointF) : base()
        {
            _tempSetPointF = tempSetPointF;
            _pinHeaterOutput = pinHeaterOutput;

            CoopData.Instance.CoopTemperatureSetPoint = _tempSetPointF;
        }

        protected override void DoWork()
        {
            _portOutHeater = new OutputPort((Cpu.Pin)_pinHeaterOutput, true);

            Thread.Sleep(1000); // wait for sensors to start

            // Monitor the temp and turn on a heater if the temp gets too cold
            // Do this as long as the microcontroller is powered up and running
            while (true)
            {
                if (CoopData.Instance.WaterTemperature != Thermometer.InvalidData)
                {
                    Debug.Print("Temp for water: " + CoopData.Instance.WaterTemperature.ToString("F0") + "F");

                    // If the water temperature is already above the high point, turn off the heater
                    if (CoopData.Instance.WaterTemperature > HIGH_TEMP)
                    {
                        // we never want the heater on if the above 90 degrees! (failsafe)
                        TurnOffHeater();
                    }
                    else
                    {
                        // If we have the coop temperature, use it and the water temperature to determine if 
                        // we need to turn the heater on
                        if (CoopData.Instance.CoopTemperature != Thermometer.InvalidData)
                        {
                            // The further we get below freezing, the warmer the water needs to be in order to 
                            // keep the nipples from freezing. 

                            const int FREEZING = 32;
                            // if it is 50F outside, we turn on the heater at 32-50=-18+32=14+15=30F
                            // if it is 32F outside, we turn on the heater at 32-32=0+32=32+15 = 49F
                            // if it is 20F outside, we turn on the heater at 32-20=12+32=44+15 = 59F
                            // if it is 0F outsside, we turn on the heater at 64 + 15 = 79F
                            // if it is -30F outside, we turn on the heater at 94 + 15 = 109F
                            _tempSetPointF = FREEZING + (FREEZING - (int)CoopData.Instance.CoopTemperature) + 15;

                            CoopData.Instance.CoopTemperatureSetPoint = _tempSetPointF;
                            Debug.Print("Coop Temp Set Point Changed to: " + _tempSetPointF.ToString());

                            // no matter how cold it is in the coop, let's cap the temperature to 105
                            if (_tempSetPointF > HIGH_TEMP)
                                _tempSetPointF = HIGH_TEMP;
                        }


                        if (CoopData.Instance.WaterTemperature < _tempSetPointF)
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
