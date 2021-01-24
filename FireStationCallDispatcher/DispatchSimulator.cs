using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public class DispatchSimulator
    {
        protected CallEmployeeMapper callEmployeeMapper;
        protected CallManager callManager;
        protected EmployeeManager employeeManager;

        public DispatchSimulator()
        {
            callEmployeeMapper = new CallEmployeeMapper();
            callManager = new CallManager();
            employeeManager = new EmployeeManager(callEmployeeMapper);
        }

        public void TriggerSimulation()
        {
            Logger.InfoLog("DispatchSimulator started");
            Logger.InfoLog("");

            while (callManager.HasUnhandledCalls())
            {
                Call call = callManager.GetNextCall();

                List<Seniority> compatibleSeniorities = callEmployeeMapper.GetCompatibleSeniorities(call.CallPriority);

                bool callSuccessfullyAssigned = employeeManager.AssignCall(call, compatibleSeniorities);

                employeeManager.FinishCalls();

                if (!callSuccessfullyAssigned)
                {
                    //handle an unassignable call
                    callManager.ReAddCall(call);
                }
            }

            Logger.InfoLog("");
            Logger.InfoLog($"DispatchSimulator finished. {callManager.CompletedCallCount} calls handled successfully.");
        }
    }
}
