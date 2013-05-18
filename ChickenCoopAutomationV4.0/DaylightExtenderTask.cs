using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;


namespace ChickenCoopAutomation
{
    public class DaylightExtenderTask : Task
    {
        private const int CHECK_INTERVAL = 60000;   // amount of time in mS before checking agian
        private FEZ_Pin.Digital _pinRelayOutlet;
        private OutputPort _portRelayOutlet;


        public DaylightExtenderTask(FEZ_Pin.Digital pinRelayOutlet)
            : base()
        {
            _pinRelayOutlet = pinRelayOutlet;
        }

        protected override void DoWork()
        {
            _portRelayOutlet = new OutputPort((Cpu.Pin)_pinRelayOutlet, true);

            while (true)
            {
                DateTime dateTime = DateTime.Now;

                // Add some extra light in the morning
                if ((dateTime.Hour > 3) && (dateTime.Hour < 8))
                    TurnOnLight();
                else
                    TurnOffLight();
                
                base.Sleep(CHECK_INTERVAL);    // sleep for CHECK_INTERVAL seconds, and then check again
            }
        }

        private void TurnOnLight()
        {
            _portRelayOutlet.Write(false);
        }

        private void TurnOffLight()
        {
            _portRelayOutlet.Write(true);
        }
    }
}
