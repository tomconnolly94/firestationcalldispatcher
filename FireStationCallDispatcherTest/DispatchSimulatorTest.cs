﻿using FireStationCallDispatcher;
using FireStationCallDispatcher.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class DispatchSimulatorTest
    {
        [TestMethod]
        public void TestSimulationSuccessfulCallAssignment()
        {
            Call call = new Call(PriorityLevel.Low, 1, "TestCaller");

            Mock<ICallManager> callManagerMock = new Mock<ICallManager>();
            callManagerMock.SetupSequence(cm => cm.HasUnhandledCalls())
                .Returns(true)
                .Returns(false);
            callManagerMock.Setup(cm => cm.GetNextCall())
                .Returns(call);

            Mock<IEmployeeManager> employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(em => em.DispatchCall(call))
                .Returns(true);
            employeeManagerMock.Setup(em => em.GetBusyEmployees())
                .Returns(new List<Employee>() { });

            DispatchSimulator.TriggerSimulation(callManagerMock.Object, employeeManagerMock.Object);

            callManagerMock.Verify(cm => cm.HasUnhandledCalls(), Times.Exactly(2));
            callManagerMock.Verify(cm => cm.GetNextCall(), Times.Once);
            employeeManagerMock.Verify(em => em.DispatchCall(call), Times.Once);
            employeeManagerMock.Verify(em => em.FinishCalls(), Times.Once);
            callManagerMock.Verify(cm => cm.ReAddCall(call), Times.Never);
        }

        [TestMethod]
        public void TestSimulationFailingCallAssignment()
        {
            Call call = new Call(PriorityLevel.Low, 1, "TestCaller");

            Mock<ICallManager> callManagerMock = new Mock<ICallManager>();
            callManagerMock.SetupSequence(cm => cm.HasUnhandledCalls())
                .Returns(true)
                .Returns(false);
            callManagerMock.Setup(cm => cm.GetNextCall())
                .Returns(call);

            Mock<IEmployeeManager> employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(em => em.DispatchCall(call))
                .Returns(false);
            employeeManagerMock.Setup(em => em.GetBusyEmployees())
                .Returns(new List<Employee>() { });

            DispatchSimulator.TriggerSimulation(callManagerMock.Object, employeeManagerMock.Object);

            callManagerMock.Verify(cm => cm.HasUnhandledCalls(), Times.Exactly(2));
            callManagerMock.Verify(cm => cm.GetNextCall(), Times.Once);
            employeeManagerMock.Verify(em => em.DispatchCall(call), Times.Once);
            employeeManagerMock.Verify(em => em.FinishCalls(), Times.Once);
            callManagerMock.Verify(cm => cm.ReAddCall(call), Times.Once);
        }
    }
}
