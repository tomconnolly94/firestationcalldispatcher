using System.Collections.Generic;

namespace FireStationCallDispatcher
{
    public interface IEmployeeManager
    {
        bool DispatchCall(Call call);
        void FinishCalls();
        List<Employee> GetBusyEmployees();
    }
}