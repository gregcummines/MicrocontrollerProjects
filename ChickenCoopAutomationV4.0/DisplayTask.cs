using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// class to display coop information
    /// </summary>
    public class DisplayTask : Task
    {
        private LCD lcd;

        public DisplayTask(FEZ_Pin.Digital D4, 
                           FEZ_Pin.Digital D5, 
                           FEZ_Pin.Digital D6, 
                           FEZ_Pin.Digital D7, 
                           FEZ_Pin.Digital E, 
                           FEZ_Pin.Digital RS)
            : base()
        {
            lcd = new LCD(LCD.LCDType.LCD4x20, D4, D5, D6, D7, E, RS);
        }

        protected override void DoWork()
        {
            lcd.SetCursor(1, 1);
            lcd.Print("Coop of the Future!!");
            lcd.SetCursor(2, 1);
            lcd.Print("Version 5.0");
            lcd.SetCursor(3, 1);
            lcd.Print("Built on 11/27/11");
            lcd.SetCursor(4, 1);
            lcd.Print("Loading");
            for (int i = 0; i < 8; i++)
            {
                lcd.Print(".");
                Thread.Sleep(200);
            }
            Thread.Sleep(1000);

            while (true)
            {
                UpdateLCDDisplay();
                               
                base.Sleep(30000);
            }
        }

        private void UpdateLCDDisplay()
        {
            lcd.Clear();
            PrintDateTime(lcd);
            PrintCoopTemp(lcd);
            PrintWaterTemp(lcd);
            PrintWaterHeaterSetTemp(lcd);
            PrintLightLevel(lcd);
            PrintFoodLevel(lcd);
        }
        
        private static void PrintDateTime(LCD lcd)
        {
            DateTime dateTime = DateTime.Now;
            string strTime = GetFormattedTime(dateTime);
            string strDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();
            lcd.SetCursor(1, 1);
            lcd.Print(strDate + " " + strTime);
        }

        private static void PrintCoopTemp(LCD lcd)
        {
            string strTemp = "NA";
            lcd.SetCursor(2, 1);
            if (CoopData.Instance.CoopTemperature != CoopData.InvalidData)
            {
                strTemp = CoopData.Instance.CoopTemperature.ToString("F0") + "F";                
            }
            lcd.Print("Coop:" + strTemp);
        }

        private static void PrintWaterTemp(LCD lcd)
        {
            string strTemp = "NA";
            lcd.SetCursor(2, 11);
            if (CoopData.Instance.WaterTemperature != CoopData.InvalidData)
            {
                strTemp = CoopData.Instance.WaterTemperature.ToString("F0") + "F";
            }
            lcd.Print("Wtr:" + strTemp);
        }

        private static void PrintWaterHeaterSetTemp(LCD lcd)
        {
            string strData = "NA";
            lcd.SetCursor(3, 1);
            if (CoopData.Instance.WaterTemperatureSetPoint != CoopData.InvalidData)
            {
                strData = CoopData.Instance.WaterTemperatureSetPoint.ToString("F0") + "F";
            }
            lcd.Print("Htr:" + strData);
        }

        private static void PrintLightLevel(LCD lcd)
        {
            string strData = "NA";
            lcd.SetCursor(3, 11);
            if (CoopData.Instance.InstantLightReading != CoopData.InvalidData)
            {
                strData = CoopData.Instance.InstantLightReading.ToString();
            }
            lcd.Print("Light:" + strData);
        }

        private static void PrintFoodLevel(LCD lcd)
        {
            lcd.SetCursor(4, 1);
            switch(CoopData.Instance.FoodLevelLow)
            {
                case 0:
                    lcd.Print("Food:OK");
                    break;
                case 1:
                    lcd.Print("Food:Low");
                    break;
                case CoopData.InvalidData:
                    lcd.Print("Food:NA");
                    break;
            }
        }

        /// <summary>
        /// Returns a formatted date time string that is 16 characters long or less
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string GetFormattedTime(DateTime dateTime)
        {
            string s = string.Empty;
            int hours = 0;
            if (dateTime.Hour > 12)
            {
                hours = dateTime.TimeOfDay.Hours - 12;
            }
            else
            {
                hours = dateTime.TimeOfDay.Hours;
            }
            if (hours < 12)
            {
                s += " ";
            }
            s += hours.ToString();
            s += ":";
            if (dateTime.TimeOfDay.Minutes < 10)
            {
                s += "0";
            }
            s += dateTime.TimeOfDay.Minutes;
            if (dateTime.Hour > 12)
            {
                s += "PM";
            }
            else
            {
                s += "AM";
            }

            return s;
        }
    }
}
