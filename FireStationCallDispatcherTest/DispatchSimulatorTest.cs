using FireStationCallDispatcher;
using FireStationCallDispatcher.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class DispatchSimulatorTest
    {
        [TestMethod]
        public void TestSimulationSuccessfulCallAssignment()
        {
            Call call = new Call(PriorityLevel.Low, 1);

            Mock<ICallManager> callManagerMock = new Mock<ICallManager>();
            callManagerMock.SetupSequence(cm => cm.HasUnhandledCalls())
                .Returns(true)
                .Returns(false);
            callManagerMock.Setup(cm => cm.GetNextCall())
                .Returns(call);

            Mock<IEmployeeManager> employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(em => em.AssignCall(call))
                .Returns(true);

            DispatchSimulator.TriggerSimulation(callManagerMock.Object, employeeManagerMock.Object);

            callManagerMock.Verify(cm => cm.HasUnhandledCalls(), Times.Exactly(2));
            callManagerMock.Verify(cm => cm.GetNextCall(), Times.Once);
            employeeManagerMock.Verify(em => em.AssignCall(call), Times.Once);
            employeeManagerMock.Verify(em => em.FinishCalls(), Times.Once);
            callManagerMock.Verify(cm => cm.ReAddCall(call), Times.Never);
        }

        [TestMethod]
        public void TestSimulationFailingCallAssignment()
        {
            Call call = new Call(PriorityLevel.Low, 1);

            Mock<ICallManager> callManagerMock = new Mock<ICallManager>();
            callManagerMock.SetupSequence(cm => cm.HasUnhandledCalls())
                .Returns(true)
                .Returns(false);
            callManagerMock.Setup(cm => cm.GetNextCall())
                .Returns(call);

            Mock<IEmployeeManager> employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(em => em.AssignCall(call))
                .Returns(false);

            DispatchSimulator.TriggerSimulation(callManagerMock.Object, employeeManagerMock.Object);

            callManagerMock.Verify(cm => cm.HasUnhandledCalls(), Times.Exactly(2));
            callManagerMock.Verify(cm => cm.GetNextCall(), Times.Once);
            employeeManagerMock.Verify(em => em.AssignCall(call), Times.Once);
            employeeManagerMock.Verify(em => em.FinishCalls(), Times.Once);
            callManagerMock.Verify(cm => cm.ReAddCall(call), Times.Once);
        }
    }
}
