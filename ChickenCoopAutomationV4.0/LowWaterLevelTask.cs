using System;
using Microsoft.SPOT;

namespace ChickenCoopAutomation
{
    public class LowWaterLevelTask : Task
    {
        public LowWaterLevelTask() { }

        // The DoWork method is called the the Task is asked to Start, and this 
        // is always done with a new thread
        protected override void DoWork()
        {
            while (true)
            {

            }
        }
    }
}
