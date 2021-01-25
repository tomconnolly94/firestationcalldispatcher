using FireStationCallDispatcher.Interfaces;

namespace FireStationCallDispatcher
{
    public static class DispatchSimulator
    {
        public static void TriggerSimulation(ICallManager callManager, IEmployeeManager employeeManager)
        {
            Logger.InfoLog("DispatchSimulator started");
            Logger.InfoLog("");

            while (callManager.HasUnhandledCalls())
            {
                Call call = callManager.GetNextCall();

                bool callSuccessfullyAssigned = employeeManager.AssignCallToAnEmployee(call);

                employeeManager.FinishCalls();

                if (!callSuccessfullyAssigned)
                {
                    //handle an unassignable call
                    callManager.ReAddCall(call);
                }
            }

            while(employeeManager.GetBusyEmployees().Count != 0)
            {
                employeeManager.FinishCalls();
            }

            Logger.InfoLog("");
            Logger.InfoLog($"DispatchSimulator finished. {callManager.GetCompletedCallCount()} calls handled successfully.");
        }
    }
}
