using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;


namespace ChickenCoopAutomation
{
    /// <summary>
    /// Allows to receive infrared data using a receiver like TSOP312
    /// </summary>
    public class InfraredReceiver : IDisposable
    {
        #region Delegates

        public delegate void InfraredDataHandler(object sender, uint[] _pulseWidths, int _pulseCount);

        #endregion

        /// <summary>
        /// Minimum number of pulses (high or low) that must occur in order to generate DataReceived event
        /// </summary>
        public const int MinPulseCount = 20;

        /// <summary>
        /// Maximum number of pulses (high or low) that can occur in one data packet
        /// </summary>
        public const int MaxPulseCount = 255;

        /// <summary>
        /// Received data is considered complete if this time (ms) has elapsed and no more pulses were received.
        /// </summary>
        public const int ReceiveTimeout = 10;

        private readonly Timer _notifyDataReceivedTimer;
        private readonly InterruptPort _infraredPort;
        private DateTime _lastInterruptTime;
        private uint[] _pulseWidths;
        private int _pulseCount;

        public InfraredReceiver(Cpu.Pin infraredReceiverPin)
        {
            _infraredPort = new InterruptPort(infraredReceiverPin, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            _infraredPort.OnInterrupt += OnInfraredInterrupt;
            _lastInterruptTime = DateTime.Now;
            _pulseWidths = new uint[MaxPulseCount];
            _notifyDataReceivedTimer = new Timer(NotifyDataReceived, null, int.MaxValue, 0);
        }

        public event InfraredDataHandler DataReceived = delegate { };

        private void OnInfraredInterrupt(uint data1, uint data2, DateTime time)
        {
            var pulseWidth = (int)time.Subtract(_lastInterruptTime).Ticks / 10; // µs

            _lastInterruptTime = time;
            _notifyDataReceivedTimer.Change(ReceiveTimeout, 0);

            // first interrupt of a new data packet - pulseWidth cannot be calculated
            if (data2 == 0 && _pulseCount <= 0) return;

            if (_pulseCount < MaxPulseCount)
            {
                _pulseWidths[_pulseCount] = (uint)pulseWidth;
                _pulseCount++;
            }
        }

        private void NotifyDataReceived(object state)
        {
            if (_pulseCount >= MinPulseCount)
            {
                DataReceived(this, _pulseWidths, _pulseCount);
            }

            _pulseCount = 0;
            _pulseWidths = new uint[MaxPulseCount];
        }

        #region IDisposable Members

        public void Dispose()
        {
            _infraredPort.ClearInterrupt();
            _infraredPort.Dispose();
        }

        #endregion
    }
}
