using System;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    public class CoopData
    {
        private static CoopData _instance;

        public const int InvalidData = -32767;
        public enum DoorStateEnum { Unknown, Open, Closed };

        public DoorStateEnum DoorState { get; set; } 
        public double WaterTemperature { get; set; }
        public double CoopTemperature { get; set; }
        public double CoopTemperatureSetPoint { get; set; }
        public bool WaterHeaterOn { get; set; }
        public bool CoopLightOn { get; set; }
        public bool FoodLevelLow { get; set; }
        public int InstantLightReading { get; set; }
        public int AverageLightReading { get; set; }

        private CoopData() 
        {
            CoopTemperature = InvalidData;
            CoopTemperatureSetPoint = InvalidData;
            WaterTemperature = InvalidData;
            DoorState = DoorStateEnum.Unknown;
            InstantLightReading = InvalidData;
            AverageLightReading = InvalidData;
        }

        public static CoopData Instance
        {
            get
            {
                // lazy initialization
                if (_instance == null)
                {
                    _instance = new CoopData();
                }
                return _instance;
            }
        }
    }
}
