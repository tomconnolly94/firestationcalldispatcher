using FireStationCallDispatcher.Interfaces;

namespace FireStationCallDispatcher
{
    public static class DispatchSimulator
    {
        public static void TriggerSimulation(ICallManager callManager, IEmployeeManager employeeManager)
        {
            Logger.InfoLog("DispatchSimulator started\n");

            while (callManager.HasUnhandledCalls())
            {
                Call call = callManager.GetNextCall();
                bool callSuccessfullyAssigned = employeeManager.DispatchCall(call);

                employeeManager.FinishCalls();

                if (!callSuccessfullyAssigned)
                    callManager.ReAddCall(call);
            }

            while(employeeManager.GetBusyEmployees().Count != 0)
                employeeManager.FinishCalls();

            Logger.InfoLog($"\nDispatchSimulator finished. {callManager.GetCompletedCallCount()} calls handled successfully.");
        }
    }
}
