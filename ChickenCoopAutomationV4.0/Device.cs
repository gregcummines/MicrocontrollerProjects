using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace ChickenCoopAutomation
{
    public abstract class Device
    {
        protected OutputPort device;

        public Device(Cpu.Pin pin)
        {
            device = new OutputPort(pin, false);
        }

        public virtual void TurnOn()
        {
            device.Write(true);
        }

        public virtual void TurnOff()
        {
            device.Write(false);
        }

        public bool State
        {
            get { return device.Read(); }
        }
    }
}
