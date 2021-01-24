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
    }
}
