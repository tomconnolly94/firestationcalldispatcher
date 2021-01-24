using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public enum PriorityLevel
    {
        Low = 0,
        High = 1
    }

    public class Call
    {
        public PriorityLevel CallPriority { get; set; }
        public int CallId { get; private set; }
        public Call(PriorityLevel callPriority, int callId)
        {
            CallPriority = callPriority;
            CallId = callId;
        }
    }
}
