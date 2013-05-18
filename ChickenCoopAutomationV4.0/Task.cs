using System;
using System.Threading;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    public abstract class Task
    {
        private Thread thread;
        private int ID;

        public Task()
        {
            Random rdm = new Random();
            ID = rdm.Next(10000);
        }

        public void Start()
        {
            thread = new Thread(DoWork);
            thread.Start();
        }

        protected void Sleep(int timeInMS)
        {
            //int tickCount = System.Environment.TickCount;
            //while (true)
            //{
            //    //Debug.Print("Thread " + ID.ToString() + " is sleeping");
            //    System.Threading.Thread.Sleep(1000);
            //    if (System.Environment.TickCount > (tickCount + timeInMS))
            //        break;
            //}
            //Debug.Print("Thread " + ID.ToString() + " is back alive at " + DateTime.Now.TimeOfDay.ToString());
            Thread.Sleep(timeInMS);
        }
        protected abstract void DoWork();
    }
}
