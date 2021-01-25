using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class EmployeeManagerTest: EmployeeManager
    {

        public EmployeeManagerTest() : base(1, 1, 0, 1, 0)
        {}

        [TestMethod]
        public void TestEmployeeManagerCreation()
        { 
            Assert.AreEqual(2, employees.Count);
        }

        [TestMethod]
        public void TestCallIsEscalated()
        {
            Call call = new Call(PriorityLevel.Low, 1);
            bool callIsEscalated = CallIsEscalated(call);

            Assert.AreEqual(true, callIsEscalated);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
        }

        [TestMethod]
        public void TestAssignEscalatingCallToAnEmployee()
        {
            Call call = new Call(PriorityLevel.Low, 1);

            bool assignmentSuccess = AssignCallToAnEmployee(call);

            Assert.AreEqual(false, assignmentSuccess);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
        }

        [TestMethod]
        public void TestAssignCallToAnEmployee()
        {
            Call call = new Call(PriorityLevel.High, 1);

            bool assignmentSuccess = AssignCallToAnEmployee(call);

            Assert.AreEqual(true, assignmentSuccess);
            Assert.AreEqual(PriorityLevel.High, call.CallPriority);
            Assert.AreEqual(1, GetBusyEmployees().Count);
        }

    }
}
