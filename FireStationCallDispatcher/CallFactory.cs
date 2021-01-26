using System;
using System.Collections.Generic;

namespace FireStationCallDispatcher
{
    public static class CallGenerator
    {
        public static List<Call> GenerateCalls(int callCount)
        {
            List<Call> calls = new List<Call>();
            Random random = new Random();

            for (int callIndex = 0; callIndex < callCount; callIndex++)
            {
                var priorityIndex = random.Next(0, Enum.GetNames(typeof(PriorityLevel)).Length);
                PriorityLevel callPriority = (PriorityLevel)priorityIndex;
                calls.Add(new Call(callPriority, callIndex + 1, $"Caller No {callIndex + 1}"));
            }

            Logger.InfoLog($"Calls created. {callCount} calls in the call queue.");
            return calls;
        }
    }
}
