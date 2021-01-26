namespace FireStationCallDispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string employeeFile = args[0];

                EmployeeManager employeeManager = new EmployeeManager(6, employeeFile);
                CallManager callManager = new CallManager();
                DispatchSimulator.TriggerSimulation(callManager, employeeManager);

            }
            else
            {
                Logger.ErrorLog("No employees JSON file provided at command line.");
                Logger.ErrorLog("Usage: FireStationCallDispatcher.exe employees.json");
                Logger.ErrorLog("See the ../../../data directory for example files...");
                Logger.ErrorLog("Exiting.");
            }
        }
    }
}
