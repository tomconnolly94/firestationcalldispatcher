using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireStationCallDispatcher
{
    public class EmployeeManager : IEmployeeManager
    {
        protected List<Employee> employees;
        protected Random randomNumberGenerator;
        protected int maxEscalationChance;

        public EmployeeManager(int maxEscalationChance, int numJuniorEmployees, int numSeniorEmployees, int numManagerEmployees, int numDirectorEmployees)
        {
            employees = new List<Employee>();
            randomNumberGenerator = new Random();
            this.maxEscalationChance = maxEscalationChance;

            employees.AddRange(GenerateEmployeesOfType(numJuniorEmployees, Seniority.Junior));
            employees.AddRange(GenerateEmployeesOfType(numSeniorEmployees, Seniority.Senior));
            employees.AddRange(GenerateEmployeesOfType(numManagerEmployees, Seniority.Manager));
            employees.AddRange(GenerateEmployeesOfType(numDirectorEmployees, Seniority.Director));
        }

        protected List<Employee> GenerateEmployeesOfType(int numberOfEmployees, Seniority employeeType)
        {
            List<Employee> employees = new List<Employee>();
            for (int employeeIndex = 0; employeeIndex < numberOfEmployees; ++employeeIndex)
            {
                employees.Add(new Employee(employeeType));

            }
            return employees;
        }

        protected bool CallIsEscalated(Call call)
        {
            //check if call has been escalated (1/10 chance) 
            if (call.CallPriority == PriorityLevel.Low && randomNumberGenerator.Next(0, maxEscalationChance) == 0)
            {
                call.CallPriority = PriorityLevel.High;
                Logger.InfoLog($"Call {call.CallId} has been escalated from Low to High priority.");
                return true;
            }
            return false;
        }

        public bool AssignCallToAnEmployee(Call call)
        {
            List<Seniority> compatibleSeniorities = CallEmployeeMapper.GetCompatibleSeniorities(call.CallPriority);
            foreach (Seniority seniority in compatibleSeniorities)
            {
                List<Employee> freeEmployees = employees.Where(employee => employee.IsFree() && employee.Seniority == seniority).ToList();
                if (freeEmployees.Count > 0)
                {
                    Employee selectedEmployee = freeEmployees[0];
                    selectedEmployee.AssignCall(call);

                    Logger.InfoLog($"Call {call.CallId} with {call.CallPriority} priority has been assigned to a {selectedEmployee.Seniority} employee.");

                    if(CallIsEscalated(call) && !selectedEmployee.CanHandleCall(call))
                    {
                        selectedEmployee.FinishCall();
                        return false;
                    }

                    return true;
                }
            }

            Logger.InfoLog($"No employees with the correct seniority could be found to handle call {call.CallId}, it will be re-added to the queue list.");
            return false;
        }

        public void FinishCalls()
        {
            List<Employee> busyEmployees = GetBusyEmployees();
            int numberOfCallsToBeFinished = randomNumberGenerator.Next(0, busyEmployees.Count + 1);

            for (var employeeIndex = 0; employeeIndex < numberOfCallsToBeFinished; employeeIndex++)
            {
                Employee employee = busyEmployees[employeeIndex];
                Logger.InfoLog($"Call {employee.Call.CallId} has been successfully completed.");
                busyEmployees[employeeIndex].FinishCall();
            }
        }

        public List<Employee> GetBusyEmployees()
        {
            return employees.Where(employee => !employee.IsFree()).ToList();
        }
    }
}
