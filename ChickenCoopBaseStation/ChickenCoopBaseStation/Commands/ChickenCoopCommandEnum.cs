using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChickenCoopBaseStation.Commands
{
    // Defines all the possible commands that can be sent to the coop
    // We only need room for 0-255 commands
    public enum ChickenCoopCommandEnum : byte
    {
        // Commands below 10 are reserved for future use
        GetWaterTemperature = 10,
        GetCoopTemperature = 11,
        GetCoopDateTime = 12,
        GetDoorState = 13,
        GetDoorOperatingMode = 14,
        GetWaterTemperatureSetPoint = 15,
        GetWaterHeaterOn = 16,
        GetCoopLightOn = 17,
        GetFoodLevelLow = 18,
        GetInstantLightReading = 19,
        GetAverageLightReading = 20,
        GetAllStats = 21,
    }

}
