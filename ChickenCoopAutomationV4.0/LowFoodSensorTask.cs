using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// Refactor the sensor and hardware pin connections from the warning "_foodLevelOK".
    /// </summary>
    public class LowFoodSensorTask : Task
    {
        #region Delegates

        public delegate void LowFoodSensorDataChanged(object sender, bool bFoodLevelOK, DateTime dateTimeChanged);

        #endregion

        public event LowFoodSensorDataChanged DataChanged = delegate { };

        private static uint[] _savedPulse;
        private static int _savedPulseCount;
        private static OutputPort _onBoardLed;
        private static InfraredSender _sender;
        private static InfraredReceiver _receiver;
        private static uint[] _pulseWidths;
        private readonly Timer _signalTimer;
        private static DateTime _lastSignalReceived;
        private static bool _foodLevelOK;


        public LowFoodSensorTask(FEZ_Pin.Interrupt infraredReceiverInterrupt, FEZ_Pin.Digital infraredLEDPin, FEZ_Pin.PWM PWMPin, int checkTimeInMS)  
        {
            _onBoardLed = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, false);
            _sender = new InfraredSender((Cpu.Pin)infraredLEDPin, true, (Cpu.Pin)PWMPin, 38000);
            _receiver = new InfraredReceiver((Cpu.Pin)infraredReceiverInterrupt);
            _receiver.DataReceived += OnDataReceived;
            _signalTimer = new Timer(SignalCheck, null, checkTimeInMS*2, checkTimeInMS);
            _foodLevelOK = true;
            _pulseWidths = new uint[] { 346, 1730, 349, 1758, 348, 1738, 341, 793, 340, 764, 340, 794, 337, 1749, 330, 824, 334, 780, 351 };
        }

        protected override void DoWork()
        {
            _lastSignalReceived = DateTime.Now;
            while (true)
            {
                _sender.Send(_pulseWidths, _pulseWidths.Length);
                Thread.Sleep(1000);
            }

        }

        public bool FoodLevelOK
        {
            get { return _foodLevelOK; }
        }

        private void SignalCheck(object state)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan span = dateTimeNow - _lastSignalReceived;
            TimeSpan fiveSeconds = new TimeSpan(0,0,5);
            bool bLastFoodLevelOK = _foodLevelOK;
            if (span > fiveSeconds)
            {
                _foodLevelOK = true;
                if ((DataChanged != null) && (bLastFoodLevelOK != _foodLevelOK))
                    DataChanged(this, _foodLevelOK, dateTimeNow);
            }
            else
            {
                _foodLevelOK = false;
                if ((DataChanged != null) && (bLastFoodLevelOK != _foodLevelOK))
                    DataChanged(this, _foodLevelOK, dateTimeNow);
            }
        }  

        private static void OnDataReceived(object sender, uint[] pulseWidths, int pulseCount)
        {
            // remember this pulse so that it can be mocked on button press
            _savedPulse = pulseWidths;
            _savedPulseCount = pulseCount;
            _lastSignalReceived = DateTime.Now; 
            Blink(2);
        }

        private static void Blink(int count)
        {
            for (var i = 0; i < count * 2; i++)
            {
                _onBoardLed.Write(!_onBoardLed.Read());
                Thread.Sleep(100);
            }
        }

    }
}
