using System;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace ChickenCoopAutomation
{
    public static class DS18B20
    {
        public const byte SearchROM = 0xF0;
        public const byte ReadROM = 0x33;
        public const byte MatchROM = 0x55;
        public const byte SkipROM = 0xCC;
        public const byte AlarmSearch = 0xEC;
        public const byte StartTemperatureConversion = 0x44;
        public const byte ReadScratchPad = 0xBE;
        public const byte WriteScratchPad = 0x4E;
        public const byte CopySratchPad = 0x48;
        public const byte RecallEEPROM = 0xB8;
        public const byte ReadPowerSupply = 0xB4;
    }

    public enum BitResolution
    {
        NineBit,
        TenBit,
        ElevenBit, 
        TwelveBit
    }

    public class Thermometer
    {
        private OneWire _DataPin;

        public static long InvalidData = -32767;

        public static float DataToF(long d)
        {
            if (d != InvalidData)
                return (float)(32.0 + (9.0 / 5.0) * (d / 16.0));
            else
                return InvalidData;
        }

        public delegate float UnitConverter(long data);
        public static float DataToC(long d)
        {
            if (d != InvalidData)
                return (float)(d / 16.0);
            else
                return InvalidData;
        }

        private byte[][] ids = new byte[][] { new byte[8], new byte[8], new byte[8], new byte[8] };

        public Thermometer(Cpu.Pin pin)
        {
            _DataPin = new OneWire(pin);
            _DataPin.Search_Restart();
            int i = 0;
            while (_DataPin.Search_GetNextDevice(ids[i++]))
            {
                Debug.Print("Found OneWire thermometer " + i + "...");
            }
        }

        private OneWire DataPin
        {
            get { return _DataPin ?? (_DataPin = new OneWire((Cpu.Pin)FEZ_Pin.Digital.Di3)); }
        }

        public float ReadTemp(UnitConverter c)
        {
            return c(ReadTemperatureData());
        }

        public long ReadTemperatureData()
        {
            long data = InvalidData;
            OneWire one = DataPin;
            if (one.Reset())
            {
                one.WriteByte(DS18B20.SkipROM);
                one.WriteByte(DS18B20.StartTemperatureConversion);

                int tickcount = System.Environment.TickCount;
                bool bDataRead = false;

                // Let's keep pinging the DS18B20 until we get some data. It will take some
                // time to convert the temperature. 
                while (true) 
                {
                    // If we read some data, break out
                    bDataRead = one.ReadByte() != 0;
                    if (bDataRead)
                        break;

                    // If the current time reaches the future timeout time, abort
                    if (System.Environment.TickCount > (tickcount + 3000))
                    {
                        break;
                    }
                }

                if (bDataRead)
                {
                    one.Reset();
                    one.WriteByte(DS18B20.SkipROM);
                    one.WriteByte(DS18B20.ReadScratchPad);

                    data = one.ReadByte(); // LSB 
                    data |= (ushort)(one.ReadByte() << 8); // MSB

                    //byte byte3 = one.ReadByte();
                    //byte byte4 = one.ReadByte();
                    //byte byte5 = one.ReadByte();
                }
            }
            return data;
        }

        public void SetHighResolutionMode(BitResolution resolution)
        {
            OneWire one = DataPin;
            if (one.Reset())
            {
                one.WriteByte(DS18B20.SkipROM);
                one.WriteByte(DS18B20.WriteScratchPad);
                one.WriteByte(125);
                one.WriteByte(202);

                switch (resolution)
                {
                    case BitResolution.NineBit:
                        one.WriteByte(0x1F);
                        break;
                    case BitResolution.TenBit:
                        one.WriteByte(0x3F);
                        break;
                    case BitResolution.ElevenBit:
                        one.WriteByte(0x5F);
                        break;
                    case BitResolution.TwelveBit:
                        one.WriteByte(0x7F);
                        break;
                }

                one.Reset();

                one.WriteByte(DS18B20.SkipROM);
                one.WriteByte(DS18B20.CopySratchPad);
                Thread.Sleep(15);
            }
        }
    }
}

