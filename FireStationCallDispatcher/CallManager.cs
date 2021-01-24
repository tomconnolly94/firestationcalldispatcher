using FireStationCallDispatcher.Interfaces;
using System;
using System.Collections.Generic;

namespace FireStationCallDispatcher
{
    public class CallManager : ICallManager
    {
        protected List<Call> unHandledCalls;
        protected int completedCallCount;

        public CallManager(int callCount = 100)
        {
            CreateCalls(callCount);
            completedCallCount = 0;
        }

        protected void CreateCalls(int callCount)
        {
            unHandledCalls = new List<Call>();
            Random random = new Random();

            for (int callIndex = 0; callIndex < callCount; callIndex++)
            {
                var numberOfPriorityLevels = Enum.GetNames(typeof(PriorityLevel)).Length;
                var priorityIndex = random.Next(0, numberOfPriorityLevels);
                PriorityLevel callPriority = (PriorityLevel)priorityIndex;
                unHandledCalls.Add(new Call(callPriority, callIndex + 1));
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
            completedCallCount++;
            return call;
        }

        public void ReAddCall(Call call)
        {
            Logger.InfoLog($"Call {call.CallId} re-added to the call queue.");
            completedCallCount--;
            unHandledCalls.Insert(0, call);
        }

        public int GetCompletedCallCount()
        {
            return completedCallCount;
        }
    }
}
