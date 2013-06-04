using System;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    public class CoopData
    {
        private static CoopData _instance;

        public const int InvalidData = -32767;

        public enum DoorStateEnum { Unknown, Open, Closed };
        public enum DoorOperatingModeEnum { Unknown, Manual, Automatic };

        public DoorStateEnum DoorState { get; set; }
        public DoorOperatingModeEnum DoorOperatingMode { get; set; }
        public float WaterTemperature { get; set; }
        public float CoopTemperature { get; set; }
        public float WaterTemperatureSetPoint { get; set; }
        public float CoopTemperatureSetPoint { get; set; }
        public bool WaterHeaterOn { get; set; }
        public bool CoopLightOn { get; set; }
        public int FoodLevelLow { get; set; }
        public int InstantLightReading { get; set; }
        public int AverageLightReading { get; set; }
        public int WaterLevel { get; set; }

        private CoopData() 
        {
            DoorState = DoorStateEnum.Unknown;
            DoorOperatingMode = DoorOperatingModeEnum.Unknown;
            WaterTemperature = InvalidData;
            CoopTemperature = InvalidData;
            WaterTemperatureSetPoint = InvalidData;
            CoopTemperatureSetPoint = InvalidData;
            WaterHeaterOn = false;
            CoopLightOn = false;
            FoodLevelLow = InvalidData;
            InstantLightReading = InvalidData;
            AverageLightReading = InvalidData;
            WaterLevel = InvalidData;
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
