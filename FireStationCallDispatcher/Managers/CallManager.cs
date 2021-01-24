using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public class CallManager
    {
        protected List<Call> unHandledCalls;
        public int CompletedCallCount { get; private set; }

        public CallManager()
        {
            unHandledCalls = new List<Call>();
            CreateCalls(99, 100);
            CompletedCallCount = 0;
        }

        protected void CreateCalls(int minCallCount, int maxCallCount)
        {
            Random random = new Random();
            int callCount = random.Next(minCallCount, maxCallCount + 1);

            for (int callIndex = 0; callIndex < callCount; callIndex++)
            {
                var numberOfPriorityLevels = Enum.GetNames(typeof(PriorityLevel)).Length;
                var priorityIndex = random.Next(0, numberOfPriorityLevels);
                PriorityLevel callPriority = (PriorityLevel)priorityIndex;
                unHandledCalls.Add(new Call(callPriority, callIndex));
            }

            Logger.InfoLog($"Calls created. {callCount} calls in the call queue.");
        }

        public bool HasUnhandledCalls()
        {
            return unHandledCalls.Count > 0;
        }

        public Call GetNextCall()
        {
            int index = 0;
            Call call = unHandledCalls[index];
            unHandledCalls.RemoveAt(index);
            CompletedCallCount++;
            return call;
        }

        public void ReAddCall(Call call)
        {
            Logger.InfoLog($"Call {call.CallId} re-added to the call queue.");
            CompletedCallCount--;
            unHandledCalls.Insert(0, call);
        }
    }
}
