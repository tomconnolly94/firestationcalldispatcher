using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class CallEmployeeMapperTest
    {
        [TestMethod]
        public void TestMappingValuesLowPriority()
        {
            PriorityLevel priorityLevel = PriorityLevel.Low;
            List<Seniority> seniorities = CallEmployeeMapper.GetCompatibleSeniorities(priorityLevel);

            Assert.AreEqual(3, seniorities.Count);
            Assert.AreEqual(Seniority.Junior, seniorities[0]);
            Assert.AreEqual(Seniority.Senior, seniorities[1]);
            Assert.AreEqual(Seniority.Manager, seniorities[2]);
        }

        [TestMethod]
        public void TestMappingValuesHighPriority()
        {
            PriorityLevel priorityLevel = PriorityLevel.High;
            List<Seniority> seniorities = CallEmployeeMapper.GetCompatibleSeniorities(priorityLevel);

            Assert.AreEqual(2, seniorities.Count);
            Assert.AreEqual(Seniority.Manager, seniorities[0]);
            Assert.AreEqual(Seniority.Director, seniorities[1]);
        }
    }
}
