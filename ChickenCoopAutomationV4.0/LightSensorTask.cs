using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using System.Collections;

namespace ChickenCoopAutomation
{
    public class LightSensorTask : Task
    {
        private const int NUM_LIGHT_AVERAGES = 30;
        private static AnalogIn lightSensor;
        private FEZ_Pin.AnalogIn _portInputLightSensorPin;
        private ArrayList lightReadings;

        public LightSensorTask(FEZ_Pin.AnalogIn portInputLightSensorPin)
        {
            _portInputLightSensorPin = portInputLightSensorPin;
            lightReadings = new ArrayList();
        }   

        protected override void DoWork()
        {
            lightSensor = new AnalogIn((AnalogIn.Pin)_portInputLightSensorPin);

            while (true)
            {
                CoopData.Instance.InstantLightReading = lightSensor.Read();
                DateTime dateTime = DateTime.Now;
                Debug.Print("Light at: " + dateTime.ToString() + " = " + CoopData.Instance.InstantLightReading.ToString());

                lightReadings.Add(CoopData.Instance.InstantLightReading);
                if (lightReadings.Count > NUM_LIGHT_AVERAGES)
                    lightReadings.RemoveAt(0);

                // If we have sufficient samples we can set the averaged figure
                if (lightReadings.Count == NUM_LIGHT_AVERAGES)
                {
                    CoopData.Instance.AverageLightReading = CalculateAverageLightReading(lightReadings);
                }

                base.Sleep(5000);
            }
        }

        private int CalculateAverageLightReading(ArrayList lightReadings)
        {
            int avgLightReading = 0;
            // Average all readings to get a reading
            foreach (int x in lightReadings)
            {
                avgLightReading += x;
            }
            avgLightReading = avgLightReading / NUM_LIGHT_AVERAGES;
            return avgLightReading;
        }
    }
}
