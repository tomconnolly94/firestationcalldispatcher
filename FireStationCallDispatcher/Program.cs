using System;

namespace FireStationCallDispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            CallManager callManager = new CallManager();
            EmployeeManager employeeManager = new EmployeeManager(10, 7, 5, 4, 2);
            DispatchSimulator.TriggerSimulation(callManager, employeeManager);
        }
    }
}
