using System;
using Microsoft.SPOT;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT.Hardware;

namespace ChickenCoopAutomation
{
    public class TemperatureSensorTask : Task
    {
        public enum TemperatureSensorType { Unknown, Coop, Water };
        private TemperatureSensorType _tempSensorType;
        private FEZ_Pin.Digital _pinTempSensor;

        public TemperatureSensorTask(FEZ_Pin.Digital pinTempSensor, TemperatureSensorType type) : base()
        {
            _pinTempSensor = pinTempSensor;
            _tempSensorType = type;
        }

        protected override void DoWork()
        {
            ThermometerWatcher thermometerWatcher = new ThermometerWatcher(new Thermometer((Cpu.Pin)_pinTempSensor), Thermometer.DataToF);

            Thread.Sleep(750); // wait for sensors to start

            // Monitor the temp and turn on a heater if the temp gets too cold
            // Do this as long as the microcontroller is powered up and running
            while (true)
            {
                double temp = thermometerWatcher.Thermometer.ReadTemp(Thermometer.DataToF);
                Debug.Print("Temp:" + _tempSensorType.ToString() + " " + temp.ToString("F1") + "F");

                if (temp != Thermometer.InvalidData)
                {
                    switch (_tempSensorType)
                    {
                        case TemperatureSensorType.Coop:
                            CoopData.Instance.CoopTemperature = temp;
                            break;
                        case TemperatureSensorType.Water:
                            CoopData.Instance.WaterTemperature = temp;
                            break;
                    }

                    base.Sleep(5000);
                }
            }
        }
    }
}
