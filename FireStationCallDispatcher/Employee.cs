using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{

    public enum Seniority
    {
        Junior,
        Senior,
        Manager,
        Director
    }

    public class Employee
    {
        public Seniority Seniority{ get; }
        public Call Call { get; private set; }

        public Employee(Seniority seniority)
        {
            Seniority = seniority;
        }

        public void AssignCall(Call call)
        {
            this.Call = call;
        }

        public void FinishCall()
        {
            Call = null;
        }

        public bool IsFree()
        {
            return Call == null;
        }

        public bool CanHandleCall(Call call)
        {
            List<Seniority> highSenorities = CallEmployeeMapper.GetCompatibleSeniorities(call.CallPriority);

            // check if, after call escalation, the employee can still handle this call
            if (!highSenorities.Contains(Seniority))
            {
                Logger.InfoLog($"Employee assigned to call {call.CallId} is not senior enough to handle the call after priority escalation. The call will be returned to the queue for re-processing.");
                return false;
            }
            return true;
        }
    }
}
