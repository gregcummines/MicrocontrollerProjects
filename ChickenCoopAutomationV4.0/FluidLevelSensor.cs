using System;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    ////////////////////////////////////////////////////////////////////////////////////////////
    // eTape Continuous Fluid Level Sensor PN-12110215TC-8
    //
    // Connect to the eTape by attaching alligator clips or by soldering leads to the crimp
    // pin connectors with low temperature solder. Do not over heat with soldering iron.
    // The inner two pins (pins 2 and 3) are the sensor output (Rsense). The outer pins
    // (pins 1 and 4) are the reference resistor (Rref) which can be used for temperature compensation.
    // Suspend the eTape
    // sensor in the fluid to be measured. To work properly the sensor must
    // remain straight and must
    // not be bent vertically or longitudinally. Double sided adhesive tape may be applied to the
    // upper back portion of the sensor to adhere the sensor to the inside
    // wall of the container to be
    // measured. Only apply tape to the upper back portion of the sensor as shown in the figure
    // below. If adhesive tape is applied to any other portion of the sensor it may
    // not work properly.
    // The vent hole located above the max line
    // allows the eTape to equilibr
    // ate with atmospheric
    // pressure.
    // The vent hole is fitted with a hydrophobic filter membrane to prevent the eTape from
    // being swamped if inadvertently submerged.
    //
    // Reference Resistor (Rref):1500, ±10% 
    // Sensor Output:1500 empty, 300 full, ±10%
    // Temperature Range:15°F-150°F (-9°C-65°C)
    // Resistance Gradient:140/inch (56/cm),±10%
    //
    // The eTape can be modeled as a variable resistor (300–1500 ? ± 10%).  
    //
    // Datasheet at: http://dlnmh9ip6v2uc.cloudfront.net/datasheets/Sensors/Pressure/eTape%20Datasheet%2012110215TC-8.pdf
    //
    public class FluidLevelSensor
    {
        public enum Pins
        {
            Pin1_Rref,
            Pin4_Rref,
            Pin2_Rsense,
            Pin3_Rsense
        }
    }
}
