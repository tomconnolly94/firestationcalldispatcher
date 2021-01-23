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
        protected Seniority seniority;
        protected bool busy;

        public Employee(Seniority seniority)
        {
            this.seniority = seniority;
        }

        public void HandleCall()
        {
            busy = true;

        }
    }
}
