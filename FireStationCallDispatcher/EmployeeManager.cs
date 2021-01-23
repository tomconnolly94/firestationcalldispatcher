using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public class EmployeeManager
    {
        protected List<Employee> employees;

        public EmployeeManager()
        {
            // create employees
            GenerateEmployees(5, 4, 3, 2);
        }

        public void GenerateEmployees(int numJuniorEmployees, int numSeniorEmployees, int numManagerEmployees, int numDirectorEmployees)
        {
            employees.AddRange(generateEmployeesOfType(numJuniorEmployees, Seniority.Junior));
            employees.AddRange(generateEmployeesOfType(numJuniorEmployees, Seniority.Senior));
            employees.AddRange(generateEmployeesOfType(numJuniorEmployees, Seniority.Manager));
            employees.AddRange(generateEmployeesOfType(numJuniorEmployees, Seniority.Director));
        }

        protected List<Employee> generateEmployeesOfType(int numberOfEmployees, Seniority employeeType)
        {
            List<Employee> employees = new List<Employee>();
            for (int employeeIndex = 0; employeeIndex < numberOfEmployees; ++employeeIndex)
            {
                employees.Add(new Employee(employeeType));

            }
            return employees;
        }

        public bool AssignCall()
        {
            //attempt to assign call
        }
    }
}
