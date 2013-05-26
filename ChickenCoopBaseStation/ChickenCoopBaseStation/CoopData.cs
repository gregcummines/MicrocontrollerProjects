using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChickenCoopBaseStation
{
    public class CoopData
    {
        public const int InvalidData = -32767;

        public enum DoorStateEnum { Unknown, Open, Closed };
        public enum DoorOperatingModeEnum { Unknown, Manual, Automatic };

        public DoorStateEnum DoorState { get; set; }
        public DoorOperatingModeEnum DoorOperatingMode { get; set; }
        public float WaterTemperature { get; set; }
        public float CoopTemperature { get; set; }
        public float WaterTemperatureSetPoint { get; set; }
        public bool WaterHeaterOn { get; set; }
        public bool CoopLightOn { get; set; }
        public int FoodLevelLow { get; set; }
        public int InstantLightReading { get; set; }
        public int AverageLightReading { get; set; }
        public DateTime CoopDateTime { get; set; }

        public CoopData()
        {
            CoopTemperature = InvalidData;
            WaterTemperatureSetPoint = InvalidData;
            WaterTemperature = InvalidData;
            DoorState = DoorStateEnum.Unknown;
            InstantLightReading = InvalidData;
            AverageLightReading = InvalidData;
            FoodLevelLow = InvalidData;
        }
    }
}
