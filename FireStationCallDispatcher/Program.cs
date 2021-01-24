using System;

namespace FireStationCallDispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            CallManager callManager = new CallManager();
            EmployeeManager employeeManager = new EmployeeManager();
            DispatchSimulator.TriggerSimulation(callManager, employeeManager);
        }
    }
}
