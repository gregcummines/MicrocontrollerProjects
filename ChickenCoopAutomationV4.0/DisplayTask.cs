using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// Class to display coop information on an LCD display
    /// </summary>
    public class DisplayTask : Task
    {
        private LCD lcd;

        private FEZ_Pin.Digital _D4;
        private FEZ_Pin.Digital _D5;
        private FEZ_Pin.Digital _D6;
        private FEZ_Pin.Digital _D7;
        private FEZ_Pin.Digital _E;
        private FEZ_Pin.Digital _RS;

        public DisplayTask(FEZ_Pin.Digital D4, FEZ_Pin.Digital D5, FEZ_Pin.Digital D6, FEZ_Pin.Digital D7, FEZ_Pin.Digital E, FEZ_Pin.Digital RS)
            : base()
        {
            _D4 = D4;
            _D5 = D5;
            _D6 = D6;
            _D7 = D7;
            _E = E;
            _RS = RS;
        }

        protected override void DoWork()
        {
            lcd = new LCD(LCD.LCDType.LCD4x20, _D4, _D5, _D6, _D7, _E, _RS);

            lcd.SetCursor(1, 1);
            lcd.Print("Coop of the Future!!");
            lcd.SetCursor(2, 1);
            lcd.Print("Version 4.0");
            lcd.SetCursor(3, 1);
            lcd.Print("Built on 11 /27/11");
            lcd.SetCursor(4, 1);
            lcd.Print("Loading");
            for (int i = 0; i < 13; i++)
            {
                lcd.Print(".");
                Thread.Sleep(300);
            }
            Thread.Sleep(500);
            lcd.Clear();

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
            PrintLightLevel(lcd);
            PrintWaterTemp(lcd);
            PrintWaterHeaterSetTemp(lcd);
        }
        
        private static void PrintDateTime(LCD lcd)
        {
            DateTime dateTime = DateTime.Now;
            string strTime = GetFormattedTime(dateTime);
            string strDate = dateTime.Month + "/" + dateTime.Day + "/" + dateTime.Year;
            lcd.SetCursor(1, 1);
            lcd.Print(strDate + " " + strTime);
        }

        private static void PrintCoopTemp(LCD lcd)
        {
            lcd.SetCursor(2, 1);
            lcd.Print("Coop:" + CoopData.Instance.CoopTemperature.ToString("F0") + "F");
        }

        private static void PrintWaterTemp(LCD lcd)
        {
            lcd.SetCursor(2, 11);
            lcd.Print("Wtr:" + CoopData.Instance.WaterTemperature.ToString("F0") + "F");
        }

        private static void PrintWaterHeaterSetTemp(LCD lcd)
        {
            lcd.SetCursor(3, 1);
            lcd.Print("Htr:" + CoopData.Instance.CoopTemperatureSetPoint.ToString("F0") + "F");
        }

        private static void PrintLightLevel(LCD lcd)
        {
            lcd.SetCursor(3, 11);
            lcd.Print("Light:" + CoopData.Instance.InstantLightReading.ToString());
        }

        private static void PrintFoodLevel(LCD lcd)
        {
            lcd.SetCursor(4, 1);
            if (CoopData.Instance.FoodLevelLow)
                lcd.Print("Food:Low");
            else
                lcd.Print("Food:OK");
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
