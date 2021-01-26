using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class CallTest
    {
        [TestMethod]
        public void TestCallCreationLowPriority()
        {
            int callId = 1;
            string callerName = "testCaller";
            PriorityLevel callPriority = PriorityLevel.Low;
            Call call = new Call(callPriority, callId, callerName);

            Assert.AreEqual(callId, call.CallId);
            Assert.AreEqual(callPriority, call.CallPriority);
            Assert.AreEqual(callerName, call.CallerName);
        }

        [TestMethod]
        public void TestCallCreationHighPriority()
        {
            int callId = 1;
            string callerName = "testCaller";
            PriorityLevel callPriority = PriorityLevel.High;
            Call call = new Call(callPriority, callId, callerName);

            Assert.AreEqual(callId, call.CallId);
            Assert.AreEqual(callPriority, call.CallPriority);
            Assert.AreEqual(callerName, call.CallerName);
        }
    }
}
