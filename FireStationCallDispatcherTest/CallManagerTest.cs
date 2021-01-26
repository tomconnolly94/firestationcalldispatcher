using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class CallManagerTest
    {
        [TestMethod]
        public void TestCallManagerCreation()
        {
            CallManager callManager = new CallManager();

            Assert.AreEqual(0, callManager.GetCompletedCallCount());
            Assert.IsTrue(callManager.HasUnhandledCalls());
        }

        [TestMethod]
        public void TestGetCall()
        {
            CallManager callManager = new CallManager(1);
            Call call = callManager.GetNextCall();

            Assert.IsNotNull(call);
            Assert.IsFalse(callManager.HasUnhandledCalls());
            Assert.AreEqual(1, callManager.GetCompletedCallCount());
        }

        [TestMethod]
        public void TestReAddCall()
        {
            CallManager callManager = new CallManager(1);
            Call call = callManager.GetNextCall();

            Assert.IsFalse(callManager.HasUnhandledCalls());

            callManager.ReAddCall(call);

            Assert.IsTrue(callManager.HasUnhandledCalls());
            Assert.AreEqual(0, callManager.GetCompletedCallCount());

        }
    }
}
