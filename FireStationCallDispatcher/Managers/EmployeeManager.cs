using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireStationCallDispatcher
{
    public class EmployeeManager
    {
        protected List<Employee> employees;
        protected Random randomNumberGenerator;
        protected CallEmployeeMapper callEmployeeMapper;

        public EmployeeManager(CallEmployeeMapper callEmployeeMapper)
        {
            employees = new List<Employee>();
            GenerateEmployees(5, 4, 3, 2);
            randomNumberGenerator = new Random();
            this.callEmployeeMapper = callEmployeeMapper;
        }

        public void GenerateEmployees(int numJuniorEmployees, int numSeniorEmployees, int numManagerEmployees, int numDirectorEmployees)
        {
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

        protected bool CallIsNotEscalated(Call call, Employee selectedEmployee)
        {
            //check if call has been escalated (1/10 chance) 
            if (call.CallPriority == PriorityLevel.Low && randomNumberGenerator.Next(0, 10) == 0)
            {
                call.CallPriority = PriorityLevel.High;
                Logger.InfoLog($"Call {call.CallId} has been escalated from Low to High priority.");
                List<Seniority> highSenorities = callEmployeeMapper.GetCompatibleSeniorities(PriorityLevel.High);

                // check if, after call escalation, the employee can still handle this call
                if (!highSenorities.Contains(selectedEmployee.Seniority))
                {
                    Logger.InfoLog($"Employee assigned to call {call.CallId} is not senior enough to handle the call after priority escalation. The call will be returned to the front of the queue for re-processing.");
                    selectedEmployee.FinishCall();
                    return false;
                }
            }
            return true;
        }

        public bool AssignCall(Call call, List<Seniority> compatibleSeniorities)
        {
            foreach (Seniority seniority in compatibleSeniorities)
            {
                List<Employee> freeEmployees = employees.Where(employee => employee.IsFree() && employee.Seniority == seniority).ToList();
                if(freeEmployees.Count > 0)
                {
                    Employee selectedEmployee = freeEmployees[0];
                    selectedEmployee.AssignCall(call);
                    Logger.InfoLog($"Call {call.CallId} with {call.CallPriority} priority has been assigned to a {selectedEmployee.Seniority} employee.");

                    return CallIsNotEscalated(call, selectedEmployee);
                }
            }

            Logger.InfoLog($"No employees with the correct seniority could be found to handle call {call.CallId}, it will be re-added to the queue list.");
            return false;
        }

        public void FinishCalls()
        {
            List<Employee> busyEmployees = employees.Where(employee => !employee.IsFree()).ToList();
            int numberOfCallsToBeFinished = randomNumberGenerator.Next(0, busyEmployees.Count / 2);

            for (var employeeIndex = 0; employeeIndex < numberOfCallsToBeFinished; employeeIndex++)
            {
                Employee employee = busyEmployees[employeeIndex];
                Logger.InfoLog($"Call {employee.Call.CallId} has been successfully completed.");
                busyEmployees[employeeIndex].FinishCall();
            }
        }
    }
}
