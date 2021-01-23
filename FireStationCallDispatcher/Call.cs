using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public enum PriorityLevel
    {
        Low,
        High
    }

    public class Call
    {
        protected PriorityLevel callPriority;
        public Call(PriorityLevel callPriority)
        {
            this.callPriority = callPriority;
        }
    }
}
