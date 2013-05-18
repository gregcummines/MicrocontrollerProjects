using System;
using Microsoft.SPOT;
using System.Collections;

namespace ChickenCoopAutomation
{
    public class TaskManager
    {
        private static TaskManager _instance;
        private ArrayList tasks;

        private TaskManager()
        {
            tasks = new ArrayList();
        }

        public ArrayList Tasks 
        {
            get { return tasks; }
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public static TaskManager Instance
        {
            get
            {
                // lazy initialization
                if (_instance == null)
                {
                    _instance = new TaskManager();
                }
                return _instance;
            }
        }
    }
}
