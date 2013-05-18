using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;


namespace ChickenCoopAutomation
{
    /// <summary>
    /// Allows to send infrared data using a simple infrared led
    /// </summary>
    public class InfraredSender : IDisposable
    {
        /// <summary>
        /// Minimum number of pulses (high or low) that can be send in single Send() call
        /// </summary>
        public const int MaxPulseCount = 255;

        private OutputCompare _infraredLed;
        private PWM _frequencyGenerator;
        private bool _ledInitialValue;

        public InfraredSender(Cpu.Pin ledPin, bool initialValue, Cpu.Pin frequencyPwmPin, int frequency)
        {
            _ledInitialValue = initialValue;
            _infraredLed = new OutputCompare(ledPin, initialValue, MaxPulseCount);
            _frequencyGenerator = new PWM((PWM.Pin)frequencyPwmPin);
            _frequencyGenerator.Set(frequency, 50);
        }

        public void Send(uint[] pulseWidths, int pulseCount)
        {
            if (pulseCount > MaxPulseCount)
            {
                throw new ArgumentException("Pulse count should not be higher than " + MaxPulseCount);
            }

            _infraredLed.Set(!_ledInitialValue, pulseWidths, 0, pulseCount, false);
        }

        public void Dispose()
        {
            _frequencyGenerator.Dispose();
            _infraredLed.Dispose();
            _frequencyGenerator = null;
            _infraredLed = null;
        }
    }
}
