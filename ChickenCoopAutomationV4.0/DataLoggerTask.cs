using System;
using System.IO;
using System.Threading;
using System.Text;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.IO;
using GHIElectronics.NETMF.Hardware;

namespace ChickenCoopAutomation
{
    public class DataLoggerTask : Task
    {
        private static InputPort sdDetectPin;
        private static bool readyToWrite;
        private PersistentStorage sdPS = null;
        private static OutputPort led;

        public DataLoggerTask() : base()
        {
            readyToWrite = false;

            led = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, false);

            RemovableMedia.Insert += new InsertEventHandler(RemovableMedia_Insert);
            RemovableMedia.Eject += new EjectEventHandler(RemovableMedia_Eject);

            // Set the SD detect pin
            sdDetectPin = new InputPort((Cpu.Pin)FEZ_Pin.Digital.SD_Detect, false, Port.ResistorMode.PullUp);
        }

        protected override void DoWork()
        {
            while (true)
            {
                const int POLL_TIME = 5000; // log data every interval

                bool sdExists;
                while (true)
                {
                    try // If SD card was removed while mounting, it may throw exceptions
                    {
                        sdExists = sdDetectPin.Read() == false;

                        // make sure it is fully inserted and stable
                        if (sdExists)
                        {
                            Thread.Sleep(50);
                            sdExists = sdDetectPin.Read() == false;
                        }

                        if (sdExists && sdPS == null)
                        {
                            sdPS = new PersistentStorage("SD");
                            sdPS.MountFileSystem();
                        }
                        else if (!sdExists && sdPS != null)
                        {
                            sdPS.UnmountFileSystem();
                            sdPS.Dispose();
                            sdPS = null;
                            readyToWrite = false;
                        }

                        if (readyToWrite)
                        {
                            LogData();
                        }
                    }
                    catch
                    {
                        if (sdPS != null)
                        {
                            sdPS.Dispose();
                            sdPS = null;
                        }
                    }

                    base.Sleep(POLL_TIME);
                }
            }
        }

        static void RemovableMedia_Eject(object sender, MediaEventArgs e)
        {
            readyToWrite = false;
            Debug.Print("SD card ejected");
        }

        static void RemovableMedia_Insert(object sender, MediaEventArgs e)
        {
            Debug.Print("SD card inserted");

            if (e.Volume.IsFormatted)
            {
                readyToWrite = true;
            }
            else
            {
                readyToWrite = false;
                Debug.Print("SD card is not formatted");
            }
        }

        private void LogData()
        {
            DateTime dateTime = DateTime.Now;
            string fileName = "AutomaticChickenCoopV3DataLogFile_" + dateTime.Month.ToString() + dateTime.Day.ToString() + dateTime.Year.ToString() + ".txt";

            if (sdPS != null && readyToWrite)
            {
                try
                {
                    const string header = "DateTime,CoopTemperature,WaterTemperature,WaterHeaterOn,DoorState,InstantLightLevel,AverageLightLevel,FoodLevelLow\r\n";
                    byte[] data = null;

                    string dataToFile = dateTime.ToString() + "," +
                        CoopData.Instance.CoopTemperature.ToString() + "," +
                        CoopData.Instance.WaterTemperature.ToString() + "," +
                        CoopData.Instance.WaterHeaterOn.ToString() + "," +
                        CoopData.Instance.DoorState.ToString() + "," +
                        CoopData.Instance.InstantLightReading.ToString() + "," +
                        CoopData.Instance.AverageLightReading.ToString() + "," +
                        CoopData.Instance.FoodLevelLow.ToString() + "\r\n";

                    string root = VolumeInfo.GetVolumes()[0].RootDirectory;
                    string fileToWrite = root + @"\" + fileName;
                    FileStream dataWriter = null;

                    if (!File.Exists(fileToWrite))
                    {
                        dataWriter = new FileStream(fileToWrite, FileMode.OpenOrCreate, FileAccess.Write);

                        data = Encoding.UTF8.GetBytes(header);
                        dataWriter.Write(data, 0, data.Length);
                        dataWriter.Flush();
                    }
                    else
                    {
                        dataWriter = new FileStream(fileToWrite, FileMode.Append, FileAccess.Write);
                    }

                    data = Encoding.UTF8.GetBytes(dataToFile);
                    Debug.Print(header);
                    Debug.Print(dataToFile);
                    dataWriter.Write(data, 0, data.Length);
                    dataWriter.Flush();
                    dataWriter.Close();

                    // flash the LED to indicate that the write was successful
                    for (int i = 0; i < 3; i++)
                    {
                        led.Write(true);
                        Thread.Sleep(100);
                        led.Write(false);
                        Thread.Sleep(100);
                    }
                }
                catch
                {
                    Debug.Print("Write to SD failed");
                }
            }
        }
    }
}
