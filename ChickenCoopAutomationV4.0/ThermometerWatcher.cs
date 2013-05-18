using System;
using System.Threading;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    public class ThermometerWatcher
    {
        private readonly Thermometer _Thermometer;
        private readonly TimeSpan _Interval = new TimeSpan(TimeSpan.TicksPerSecond);
        private readonly Thermometer.UnitConverter _Converter = d => d;

        private Thread _threadMonitor;

        private long _Data;
        private float _Temperature;

        public delegate void TemperatureDataChangeDelegate(long from, long to);
        public event TemperatureDataChangeDelegate TemperatureDataChanged;

        public delegate void TemperatureChangeDelegate(float from, float to);
        public event TemperatureChangeDelegate TemperatureChanged;

        public ThermometerWatcher(Thermometer thermometer) : this(thermometer, Thermometer.DataToF) { }

        public ThermometerWatcher(Thermometer thermometer, Thermometer.UnitConverter converter)
        {
            _Thermometer = thermometer;
            _Converter = converter;

            _Thermometer.SetHighResolutionMode(BitResolution.TwelveBit);

            _threadMonitor = new Thread(Monitor);
            //_threadMonitor.Priority = ThreadPriority.AboveNormal;
        }

        public Thermometer Thermometer
        {
            get { return _Thermometer; }
        }

        public void Start()
        {
            if (_threadMonitor.ThreadState != ThreadState.Running)
                _threadMonitor.Start();
        }

        public void Stop()
        {
            if (_threadMonitor.ThreadState != ThreadState.Suspended)
                _threadMonitor.Suspend();
        }

        private void Monitor()
        {
            while (true)
            {
                long data = _Thermometer.ReadTemperatureData();
                if (_Data != data)
                {
                    if (TemperatureDataChanged != null)
                        TemperatureDataChanged(_Data, data);
                    _Data = data;
                    float temp = _Converter(data);
                    if (_Temperature != temp)
                    {
                        if (TemperatureChanged != null)
                            TemperatureChanged(_Temperature, temp);
                        _Temperature = temp;
                    }
                }
                Thread.Sleep(750);
            }
        }
    }
}
