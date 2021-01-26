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
        public int CallId { get; }
        public string CallerName { get; }
        public Call(PriorityLevel callPriority, int callId, string callerName)
        {
            CallPriority = callPriority;
            CallId = callId;
            CallerName = callerName;
        }
    }
}
