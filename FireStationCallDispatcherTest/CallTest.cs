using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class CallTest
    {
        [TestMethod]
        public void TestCallCreation()
        {
            int callId = 1;
            PriorityLevel callPriority = PriorityLevel.Low;
            Call call = new Call(callPriority, callId);

            Assert.AreEqual(callId, call.CallId);
            Assert.AreEqual(callPriority, call.CallPriority);
        }
    }
}
