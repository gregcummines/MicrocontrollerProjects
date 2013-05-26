using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Collections;

namespace ChickenCoopAutomation
{
    // This class uses the XBee as a UART buffer, and receives commands and sends data back to the client
    public class XBeeTask : Task
    {
        private static SerialPort port;

        protected override void DoWork()
        {
            port = new SerialPort("COM1", 115200, Parity.None, 8, StopBits.One);
            port.Open();

            // Listen for data on the event and send back immediately
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            Thread.Sleep(Timeout.Infinite);
        }

        static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Command received by the desktop PC; let's process it and send some data back

            byte[] firstByte = new byte[1];
            port.Read(firstByte, 0, 1);

            ChickenCoopCommandEnum command = (ChickenCoopCommandEnum)(byte)firstByte[0];

            byte[] temp = null;
            byte[] data = null;
            byte payloadSize = 0;

            ArrayList list = new ArrayList();

            switch(command)
            {
                
                case ChickenCoopCommandEnum.GetWaterTemperature:
                    temp = BitConverter.GetBytes(CoopData.Instance.WaterTemperature);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetCoopTemperature:
                    temp = BitConverter.GetBytes(CoopData.Instance.CoopTemperature);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetWaterTemperatureSetPoint:
                    temp = BitConverter.GetBytes(CoopData.Instance.WaterTemperatureSetPoint);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetAverageLightReading:
                    temp = BitConverter.GetBytes(CoopData.Instance.AverageLightReading);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetInstantLightReading:
                    temp = BitConverter.GetBytes(CoopData.Instance.InstantLightReading);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetCoopDateTime:
                    DateTime now = DateTime.Now;
                    long ticks = now.Ticks;
                    temp = BitConverter.GetBytes(ticks);
                    payloadSize = 8;
                    break;
                case ChickenCoopCommandEnum.GetCoopLightOn:
                    temp = BitConverter.GetBytes(CoopData.Instance.CoopLightOn);
                    payloadSize = 1;
                    break;
                case ChickenCoopCommandEnum.GetFoodLevelLow:
                    temp = BitConverter.GetBytes(CoopData.Instance.FoodLevelLow);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetWaterHeaterOn:
                    temp = BitConverter.GetBytes(CoopData.Instance.WaterHeaterOn);
                    payloadSize = 1;
                    break;
                case ChickenCoopCommandEnum.GetDoorState:
                    temp = BitConverter.GetBytes((int)CoopData.Instance.DoorState);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetDoorOperatingMode:
                    temp = BitConverter.GetBytes((int)CoopData.Instance.DoorOperatingMode);
                    payloadSize = 4;
                    break;
                case ChickenCoopCommandEnum.GetAllStats:
                    list.Add(BitConverter.GetBytes(CoopData.Instance.WaterTemperature));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.CoopTemperature));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.WaterTemperatureSetPoint));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.AverageLightReading));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.InstantLightReading));
                    now = DateTime.Now;
                    ticks = now.Ticks;
                    list.Add(BitConverter.GetBytes(ticks));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.CoopLightOn));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.FoodLevelLow));
                    list.Add(BitConverter.GetBytes(CoopData.Instance.WaterHeaterOn));
                    list.Add(BitConverter.GetBytes((int)CoopData.Instance.DoorState));
                    list.Add(BitConverter.GetBytes((int)CoopData.Instance.DoorOperatingMode));
                    payloadSize = 42;
                    temp = new byte[payloadSize];
                    ArrayList newList = new ArrayList();
                    foreach (byte[] bytes in list)
                    {
                        foreach (byte b in bytes)
                        {
                            newList.Add(b);
                        }
                    }
                    newList.CopyTo(temp);
                    break;
                default:
                    temp = new byte[1];
                    temp[0] = 5;    // invalid command
                    payloadSize = 1;
                    break;
            }

            data = new byte[payloadSize + 1];
            data[0] = payloadSize;  // first byte is size of the payload
            Array.Copy(temp, 0, data, 1, temp.Length);
            port.Write(data, 0, data.Length);
        }
    }
}
