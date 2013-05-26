using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using ChickenCoopBaseStation.Commands;
using System.Threading;
using System.Configuration;

namespace ChickenCoopBaseStation
{
	public class ChickenCoopWirelessProtocol : IDisposable
	{
		//public delegate void DataReceivedHandler(object sender, string s);
		//public event DataReceivedHandler DataReceived = delegate { };
		private static SerialPort port;
		private static ChickenCoopWirelessProtocol instance;
		private static byte[] _data;
		private static int _endIndex;
		private static byte _payLoadSize;
		private static EventWaitHandle _waitHandle = new AutoResetEvent(false);
		private static bool _allDataReceived = false;
		
		private ChickenCoopWirelessProtocol()
		{
			string xbeeComPort = ConfigurationManager.AppSettings["ComPort"];
			port = new SerialPort(xbeeComPort, 115200, Parity.None, 8, StopBits.One);
			port.Open();
			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
		}

		public static ChickenCoopWirelessProtocol Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ChickenCoopWirelessProtocol();
				}
				return instance;
			}
		}
		
		public bool SendCommand(byte command, int timeOut)
		{
            _data = null;
            //if (_data != null)
            //    throw new Exception("Unexpected data buffer is not null and thus not ready");

            _allDataReceived = false;

            // put the command in a single byte - byte array
            byte[] commandBuffer = new byte[1] { (byte)command };
            // write the command to the SerialPort
            port.Write(commandBuffer, 0, commandBuffer.Length);
            // wait for a response (the signal will be set in the DataReceived event)
            _waitHandle.WaitOne(timeOut);

            if (_allDataReceived && _data != null)
                return true;
            else
                return false;
				
		}

		// Synchronous method to get the water temperature from the coop
		public float GetWaterTemperature()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetWaterTemperature, 500);
            float f = -32767;
            if (success)
            {
                f = BitConverter.ToSingle(_data);
                _data = null;
            }
			return f;			
		}

		public float GetCoopTemperature()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetCoopTemperature, 500);
            float f = -32767;
            if (success)
            {
                f = BitConverter.ToSingle(_data);
                _data = null;
            }
			return f;			
		}
		
		public float GetWaterTemperatureSetPoint()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetWaterTemperatureSetPoint, 500);
            float f = -32767;
            if (success)
            {
                f = BitConverter.ToSingle(_data);
                _data = null;
            }
			return f;
		}

		public DateTime GetCoopCurrentDateTime()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetCoopDateTime, 500);
            DateTime dt = DateTime.Now;
            if (success)
            {
                long l = BitConverter.ToInt64(_data);
                dt = new DateTime(l);
                _data = null;
            }
			return dt;
		}

		public CoopData.DoorStateEnum GetDoorState()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetDoorState, 500);
            CoopData.DoorStateEnum ds = CoopData.DoorStateEnum.Unknown;
            if (success)
            {
                int i = BitConverter.ToInt32(_data);
                _data = null;
                ds = (CoopData.DoorStateEnum)i;
            }
			return ds;
		}

		public CoopData.DoorOperatingModeEnum GetDoorOperatingMode()
		{
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetDoorOperatingMode, 500);
            CoopData.DoorOperatingModeEnum ds = CoopData.DoorOperatingModeEnum.Unknown;
            if (success)
            {
                int i = BitConverter.ToInt32(_data);
                _data = null;
                ds = (CoopData.DoorOperatingModeEnum)i;
            }
			return ds;
		}

		public CoopData GetAllStatistics()
		{
            CoopData coopData = null;
			bool success = SendCommand((byte)ChickenCoopCommandEnum.GetAllStats, 3000);
            if (success)
            {
                coopData = new CoopData();

                byte[] dataWaterTemperature = new byte[4];
                byte[] dataCoopTemperature = new byte[4];
                byte[] dataWaterTemperatureSetPoint = new byte[4];
                byte[] dataAverageLightReading = new byte[4];
                byte[] dataInstantLightReading = new byte[4];
                byte[] dataCoopDateTime = new byte[8];
                byte[] dataCoopLightOn = new byte[1];
                byte[] dataFoodLevelLow = new byte[4];
                byte[] dataWaterHeaterOn = new byte[1];
                byte[] dataDoorState = new byte[4];
                byte[] dataDoorOperatingMode = new byte[4];

                Array.Copy(_data, 0, dataWaterTemperature, 0, dataWaterTemperature.Length);
                Array.Copy(_data, 4, dataCoopTemperature, 0, dataCoopTemperature.Length);
                Array.Copy(_data, 8, dataWaterTemperatureSetPoint, 0, dataWaterTemperatureSetPoint.Length);
                Array.Copy(_data, 12, dataAverageLightReading, 0, dataAverageLightReading.Length);
                Array.Copy(_data, 16, dataInstantLightReading, 0, dataInstantLightReading.Length);
                Array.Copy(_data, 20, dataCoopDateTime, 0, dataCoopDateTime.Length);
                Array.Copy(_data, 28, dataCoopLightOn, 0, dataCoopLightOn.Length);
                Array.Copy(_data, 29, dataFoodLevelLow, 0, dataFoodLevelLow.Length);
                Array.Copy(_data, 33, dataWaterHeaterOn, 0, dataWaterHeaterOn.Length);
                Array.Copy(_data, 34, dataDoorState, 0, dataDoorState.Length);
                Array.Copy(_data, 38, dataDoorOperatingMode, 0, dataDoorOperatingMode.Length);

                coopData.WaterTemperature = BitConverter.ToSingle(dataWaterTemperature);
                coopData.CoopTemperature = BitConverter.ToSingle(dataCoopTemperature);
                coopData.WaterTemperatureSetPoint = BitConverter.ToSingle(dataWaterTemperatureSetPoint);
                coopData.AverageLightReading = BitConverter.ToInt32(dataAverageLightReading);
                coopData.InstantLightReading = BitConverter.ToInt32(dataInstantLightReading);
                long l = BitConverter.ToInt64(dataCoopDateTime);
                DateTime dt = new DateTime(l);
                coopData.CoopDateTime = dt;
                coopData.CoopLightOn = BitConverter.ToBoolean(dataCoopLightOn);
                coopData.FoodLevelLow = BitConverter.ToInt32(dataFoodLevelLow);
                coopData.WaterHeaterOn = BitConverter.ToBoolean(dataWaterHeaterOn);
                coopData.DoorState = (CoopData.DoorStateEnum)BitConverter.ToInt32(dataDoorState);
                coopData.DoorOperatingMode = (CoopData.DoorOperatingModeEnum)BitConverter.ToInt32(dataDoorOperatingMode);

                _data = null;
            }
			return coopData;
		}


		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{

				int bytesToRead = port.BytesToRead;

				// Create the buffer when we have read the first byte, which is the command
				if (_data == null)
				{
					// Read the first byte to get the payload size
					// The payload size = command (1 byte) + data (n bytes)
					_payLoadSize = (byte)port.ReadByte();

					// Create a buffer big enough to hold the payload
					_data = new byte[_payLoadSize];
				}
				bytesToRead = port.BytesToRead;
				if (bytesToRead > 0)
				{
					if (bytesToRead == _payLoadSize)
					{
						_endIndex = 0;
					}

					// The payload exceeds the size indicated by the payload. 
					// This is not expected, so throw an exception
					if (bytesToRead + _endIndex > _payLoadSize)
					{
						throw new Exception("Payload larger than expected!");
					}

					port.Read(_data, _endIndex, bytesToRead);

					// If we read all of our data process it
					if ((bytesToRead + _endIndex) == _payLoadSize)
					{

						_allDataReceived = true;
						_endIndex = 0;

						// signal the wait object that it is time to get the data
						_waitHandle.Set();
					}
					else
					{
						_endIndex += bytesToRead;
					}
				}
			}
			catch (Exception)
			{
				_data = null;
				_endIndex = 0;
			}
		}
		
		public void Dispose()
		{
			port.Close();
			instance = null;
		}
	}
}
