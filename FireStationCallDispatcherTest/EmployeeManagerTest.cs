using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class EmployeeManagerTest: EmployeeManager
    {

        public EmployeeManagerTest() : base(1, "FireStationCallDispatcher/data/test-employees.json")
        {}

        [TestMethod]
        public void TestEmployeeManagerCreation()
        {
            List<Employee> allEmployees = new List<Employee>();
            foreach (KeyValuePair<Seniority, List<Employee>> employeeGroup in employees)
                allEmployees.AddRange(employeeGroup.Value);
            Assert.AreEqual(2, allEmployees.Count);
        }

        [TestMethod]
        public void TestCallIsEscalated()
        {
            Call call = new Call(PriorityLevel.Low, 1, "TestCaller" );
            bool callIsEscalated = CallIsEscalated(call);

            Assert.AreEqual(true, callIsEscalated);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
        }

        [TestMethod]
        public void TestAssignEscalatingCallToAnEmployee()
        {
            Call call = new Call(PriorityLevel.Low, 1, "TestCaller");

            bool assignmentSuccess = DispatchCall(call);

            Assert.AreEqual(false, assignmentSuccess);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
        }

        [TestMethod]
        public void TestAssignCallToAnEmployee()
        {
            Call call = new Call(PriorityLevel.High, 1, "TestCaller");

            bool assignmentSuccess = DispatchCall(call);

            Assert.AreEqual(true, assignmentSuccess);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
            Assert.AreEqual(1, GetBusyEmployees().Count);
        }

    }
}
