﻿using System;
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace ChickenCoopAutomation
{
    public class Program
    {
        public static void Main()
        {
            Debug.EnableGCMessages(false);

            InitializeDateTime();

            StartWatchdog();

            StartWaterLevelSensorTask();
            StartWaterTempSensorTask();
            StartCoopTempSensorTask();
            StartLightSensorTask();
            StartCoopDoorTask();
            StartWaterHeaterTask();
            StartCoopHeaterTask();
            StartDaylightExtenderTask();
            StartDisplayTask();
            StartLEDTask();
            StartFoodLevelTask();
            StartDataloggerTask();
            StartWirelessTask();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void StartWaterLevelSensorTask()
        {
            LowWaterLevelTask taskLowWaterLevel = new LowWaterLevelTask();
            taskLowWaterLevel.Start();
            TaskManager.Instance.AddTask(taskLowWaterLevel);
        }

        private static void StartWirelessTask()
        {
            XBeeTask task = new XBeeTask();
            task.Start();
            TaskManager.Instance.AddTask(task);
        }

        private static void StartCoopHeaterTask()
        {
            // Start the task that monitors coop temperature and turns heatlamp on, if necessary
            CoopHeaterTask coopHeaterTask = new CoopHeaterTask(
                                                       FEZ_Pin.Digital.Di10,             /*OutputHeater*/
                                                       -10);                             /*TempSetPointinF*/
            coopHeaterTask.Start();
        }

        private static void StartDataloggerTask()
        {
            DataLoggerTask dataLogger = new DataLoggerTask();
            dataLogger.Start();
            TaskManager.Instance.AddTask(dataLogger);
        }

        private static void StartFoodLevelTask()
        {
            LowFoodSensorTask lowFoodSensorTask = new LowFoodSensorTask(FEZ_Pin.Interrupt.Di4, FEZ_Pin.Digital.Di7, FEZ_Pin.PWM.Di5, 5000);
            lowFoodSensorTask.Start();
            lowFoodSensorTask.DataChanged += new LowFoodSensorTask.LowFoodSensorDataChanged(lowFoodSensor_DataChanged);
            TaskManager.Instance.AddTask(lowFoodSensorTask);
        }

        static void lowFoodSensor_DataChanged(object sender, bool bFoodLevelOK, DateTime dateTimeChanged)
        {
            if (!bFoodLevelOK)
            {
                Debug.Print("Detected Food level low at " + dateTimeChanged.ToString());
                CoopData.Instance.FoodLevelLow = true;
                //blueLED.Write(true);
            }
            else
            {
                Debug.Print("Detected Food level corrected at " + dateTimeChanged.ToString());
                CoopData.Instance.FoodLevelLow = false;
                //blueLED.Write(false);
            }
        }

        private static void StartLEDTask()
        {
            LEDTask ledTask = new LEDTask(FEZ_Pin.Digital.Di13,                          /*Green LED*/
                                          FEZ_Pin.Digital.Di11,                          /*Yellow LED*/
                                          FEZ_Pin.Digital.Di12);                         /*Red LED*/
            ledTask.Start();
            TaskManager.Instance.AddTask(ledTask);
        }

        private static void StartDisplayTask()
        {
            //DisplayTask(FEZ_Pin.Digital D4, FEZ_Pin.Digital D5, FEZ_Pin.Digital D6, FEZ_Pin.Digital D7, FEZ_Pin.Digital E, FEZ_Pin.Digital RS)
            DisplayTask displayTask = new DisplayTask(FEZ_Pin.Digital.Di46, FEZ_Pin.Digital.Di48, FEZ_Pin.Digital.Di42, FEZ_Pin.Digital.Di44, FEZ_Pin.Digital.Di52, FEZ_Pin.Digital.Di50);              /*LCD Display*/
            displayTask.Start();
            TaskManager.Instance.AddTask(displayTask);
        }

        private static void StartDaylightExtenderTask()
        {
            DaylightExtenderTask taskDaylightExtender = new DaylightExtenderTask(FEZ_Pin.Digital.Di10);
            taskDaylightExtender.Start();
            TaskManager.Instance.AddTask(taskDaylightExtender);
        }

        private static void StartWaterHeaterTask()
        {
            // Start the task that monitors water temperature and turns water heater on, if necessary 
            WaterHeaterTask waterHeaterTask = new WaterHeaterTask(
                                                        FEZ_Pin.Digital.Di8,               /*OutputHeater*/
                                                        70);                               /*TempSetPointinF*/
            waterHeaterTask.Start();
            TaskManager.Instance.AddTask(waterHeaterTask);
        }

        private static void StartCoopDoorTask()
        {
            // Start the task that automatically opens coop door on a schedule
            CoopDoorTask coopDoorTask = new CoopDoorTask(FEZ_Pin.Digital.Di26,   /*OutMotorPolarityAPin*/
                                                         FEZ_Pin.Digital.Di2,              /*OutMotorPolarityBPin*/
                                                         FEZ_Pin.Digital.Di3,              /*InputDoorOpenPin*/
                                                         FEZ_Pin.Digital.Di21,             /*InputDoorClosedPin*/
                                                         FEZ_Pin.Interrupt.Di32,           /*InputButtonForManualDoorUpPin*/
                                                         FEZ_Pin.Interrupt.Di6             /*InputButtonForManualDoorDownPin*/
                                                         );
            coopDoorTask.Start();
            TaskManager.Instance.AddTask(coopDoorTask);
        }

        private static void StartLightSensorTask()
        {
            LightSensorTask lightSensorTask = new LightSensorTask(FEZ_Pin.AnalogIn.An0);
            lightSensorTask.Start();
            TaskManager.Instance.AddTask(lightSensorTask);
        }

        private static void StartCoopTempSensorTask()
        {
            // Startup the coop temperature sensor task
            TemperatureSensorTask tempSensorCoopTask = new TemperatureSensorTask(FEZ_Pin.Digital.Di9, TemperatureSensorTask.TemperatureSensorType.Coop);
            tempSensorCoopTask.Start();
            TaskManager.Instance.AddTask(tempSensorCoopTask);
        }

        private static void StartWaterTempSensorTask()
        {   
            // Startup the water temperature sensor task
            TemperatureSensorTask tempSensorWaterTask = new TemperatureSensorTask(FEZ_Pin.Digital.Di25, TemperatureSensorTask.TemperatureSensorType.Water);
            tempSensorWaterTask.Start();
            TaskManager.Instance.AddTask(tempSensorWaterTask);
        }

        /// <summary>
        /// If the RTC hasn't been set yet, set it.
        /// Also take the RTC DateTime and put it in the System Clock
        /// </summary>
        private static void InitializeDateTime()
        {
            // To keep track of time, 
            // set it at the beginning of your application from the RTC.
            // If it was NOT set before and currently running using 
            // the battery (not exhausted), set it to a fixed time.
            //RealTimeClock.SetTime(new DateTime(2011, 11, 26, 18, 51, 0));
            DateTime rtcDateTime = RealTimeClock.GetTime();
            Utility.SetLocalTime(rtcDateTime);
        }

        private static Thread restartThread = null;
        private static bool HasReset = false;
        private static void StartWatchdog()
        {
            if (GHIElectronics.NETMF.Hardware.LowLevel.Watchdog.LastResetCause == GHIElectronics.NETMF.Hardware.LowLevel.Watchdog.ResetCause.WatchdogReset)
            {
                HasReset = true;
                Debug.Print("Restarted after watchdog reset");
            }

            //device will reboot if counter hasn't been reset within 20 seconds
            GHIElectronics.NETMF.Hardware.LowLevel.Watchdog.Enable(20000);
            Debug.Print("Watchdog enabled");

            //reset every 3 seconds in background thread
            restartThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(3000);
                        GHIElectronics.NETMF.Hardware.LowLevel.Watchdog.ResetCounter();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print("Unable to reset watchdog counter: " + ex.Message);
                }
            });
            restartThread.Start();
            Debug.Print("Watchdog reset thread started");
        }
 
    }
}
