using FireStationCallDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FireStationCallDispatcherTest
{
    [TestClass]
    public class CallGeneratorTest
    {
        [TestMethod]
        public void TestGenerateCalls()
        {
            int callCount = 10;

            List<Call> calls = CallGenerator.GenerateCalls(callCount);

            Assert.AreEqual(callCount, calls.Count);
            foreach(Call call in calls)
            {
                Assert.IsNotNull(call.CallerName);
                Assert.IsNotNull(call.CallId);
                Assert.IsNotNull(call.CallPriority);
            }
        }
    }
}
