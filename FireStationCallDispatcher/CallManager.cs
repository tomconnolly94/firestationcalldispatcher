using FireStationCallDispatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FireStationCallDispatcher
{
    public class CallManager : ICallManager
    {
        protected List<Call> unhandledCalls;
        protected int completedCallCount;

        public CallManager(int callCount = 100)
        {
            unhandledCalls = CallGenerator.GenerateCalls(callCount);
            completedCallCount = 0;
        }

        public bool HasUnhandledCalls()
        {
            return unhandledCalls.Count > 0;
        }

        public Call GetNextCall()
        {
            // random call interval
            Thread.Sleep(new Random().Next(0, 1000));
            int index = 0;
            Call call = unhandledCalls[index];
            unhandledCalls.RemoveAt(index);
            completedCallCount++;
            return call;
        }

        public void ReAddCall(Call call)
        {
            int newQueuePosition = 0;
            Logger.InfoLog($"Call {call.CallId} re-added to the call queue in position {newQueuePosition}.");
            completedCallCount--;
            unhandledCalls.Insert(newQueuePosition, call);
        }

        public int GetCompletedCallCount()
        {
            return completedCallCount;
        }
    }
}
