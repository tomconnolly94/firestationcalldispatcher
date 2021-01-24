namespace FireStationCallDispatcher
{
    public interface IEmployeeManager
    {
        bool AssignCall(Call call);
        void FinishCalls();
        void GenerateEmployees(int numJuniorEmployees, int numSeniorEmployees, int numManagerEmployees, int numDirectorEmployees);
    }
}