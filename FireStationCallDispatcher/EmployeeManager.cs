using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FireStationCallDispatcher
{
    public class EmployeeManager : IEmployeeManager
    {
        protected Dictionary<Seniority, List<Employee>> employees;
        protected Random randomNumberGenerator;
        protected int maxEscalationChance;


        public EmployeeManager(int maxEscalationChance, string employeeFile)
        {
            employees = new Dictionary<Seniority, List<Employee>>{
                { Seniority.Junior, new List<Employee>() },
                { Seniority.Senior, new List<Employee>() },
                { Seniority.Manager, new List<Employee>() },
                { Seniority.Director, new List<Employee>() },
            }; ;
            randomNumberGenerator = new Random();
            this.maxEscalationChance = maxEscalationChance;

            LoadEmployees(employeeFile);
        }

        protected void LoadEmployees(string employeeFile)
        {
            if (!File.Exists(employeeFile)) {
                Logger.ErrorLog($"The employees file provided({employeeFile}) does not exist. Please make sure the path is given relative to the current working directory.");
                Environment.Exit(2);
            }

            string fileContents = File.ReadAllText(employeeFile);
                
            try 
            {
                List<Employee> allEmployees = JsonConvert.DeserializeObject<List<Employee>>(fileContents);

                foreach (Employee employee in allEmployees)
                    employees[employee.Seniority].Add(employee);

                foreach (KeyValuePair<Seniority, List<Employee>> employeeGroup in employees)
                    Logger.InfoLog($"{employeeGroup.Value.Count} {employeeGroup.Key} employees loaded.");

            }
            catch (JsonReaderException)
            {
                Logger.ErrorLog($"The employees file provided({employeeFile}) is not JSON parsable to a list of employee objects. Please see the data directory for example files.");
                Environment.Exit(1);
            } 
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

        public bool DispatchCall(Call call)
        {
            List<Seniority> compatibleSeniorities = CallEmployeeMapper.GetCompatibleSeniorities(call.CallPriority);
            foreach (Seniority seniority in compatibleSeniorities)
            {
                List<Employee> freeEmployees = employees[seniority].Where(employee => employee.IsFree).ToList();
                if (freeEmployees.Count > 0)
                {
                    Employee selectedEmployee = freeEmployees[0];
                    selectedEmployee.AssignCall(call);


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
                employee.FinishCall();
            }
        }

        public List<Employee> GetBusyEmployees()
        {
            List<Employee> busyEmployees = new List<Employee>();
            foreach (KeyValuePair<Seniority, List<Employee>> employeeGroup in employees)
                busyEmployees.AddRange(employees[employeeGroup.Key].Where(employee => !employee.IsFree).ToList());
            
            return busyEmployees;
        }
    }
}
