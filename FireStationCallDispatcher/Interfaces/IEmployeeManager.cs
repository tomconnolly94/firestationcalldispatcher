using System.Collections.Generic;

namespace FireStationCallDispatcher
{
    public interface IEmployeeManager
    {
        bool AssignCallToAnEmployee(Call call);
        void FinishCalls();
        List<Employee> GetBusyEmployees();
    }
}