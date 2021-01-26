using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void TestEmployeeCreation()
        {
            Employee juniorEmployee = new Employee(Seniority.Junior, "testJunior", 1);
            Employee seniorEmployee = new Employee(Seniority.Senior, "testJunior", 2);
            Employee managerEmployee = new Employee(Seniority.Manager, "testJunior", 3);
            Employee directorEmployee = new Employee(Seniority.Director, "testJunior", 4);

            Assert.AreEqual(Seniority.Junior, juniorEmployee.Seniority);
            Assert.AreEqual(null, juniorEmployee.Call);
            Assert.IsTrue(juniorEmployee.IsFree);
            Assert.AreEqual(Seniority.Senior, seniorEmployee.Seniority);
            Assert.AreEqual(null, seniorEmployee.Call);
            Assert.IsTrue(seniorEmployee.IsFree);
            Assert.AreEqual(Seniority.Manager, managerEmployee.Seniority);
            Assert.AreEqual(null, managerEmployee.Call);
            Assert.IsTrue(managerEmployee.IsFree);
            Assert.AreEqual(Seniority.Director, directorEmployee.Seniority);
            Assert.AreEqual(null, directorEmployee.Call);
            Assert.IsTrue(directorEmployee.IsFree);
        }

        [TestMethod]
        public void TestAssignCall()
        {
            int callId = 1;
            Employee juniorEmployee = new Employee(Seniority.Junior, "testJunior", 1);
            Call call = new Call(PriorityLevel.Low, callId, "testCaller");
            juniorEmployee.AssignCall(call);

            Assert.IsNotNull(juniorEmployee.Call);
            Assert.AreEqual(callId, juniorEmployee.Call.CallId);
            Assert.IsFalse(juniorEmployee.IsFree);
        }

        [TestMethod]
        public void TestFinishCall()
        {
            int callId = 2;
            Employee juniorEmployee = new Employee(Seniority.Junior, "testJunior", 1);
            Call call = new Call(PriorityLevel.Low, callId, "testCaller");
            juniorEmployee.AssignCall(call);
            juniorEmployee.FinishCall();

            Assert.IsNull(juniorEmployee.Call);
            Assert.IsTrue(juniorEmployee.IsFree);
        }
    }
}
