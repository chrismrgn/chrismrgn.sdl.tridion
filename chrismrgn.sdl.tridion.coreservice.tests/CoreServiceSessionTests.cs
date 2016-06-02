using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chrismrgn.sdl.tridion.coreservice.tests
{
    [TestClass]
    public class CoreServiceSessionTests
    {
        [TestMethod]
        public void TestCoreServiceSession()
        {
            var _session = TridionCoreServiceFactory.CreateCoreServiceSession();
            Assert.IsNotNull(_session);
            Assert.IsNotNull(_session.CoreServiceVersion);
            _session.Dispose();
        }

        [TestMethod]
        public void TestSessionAwareCoreServiceSession()
        {
            var _sessionAwareSession = TridionCoreServiceFactory.CreateCoreServiceSession(true);
            Assert.IsNotNull(_sessionAwareSession);
            Assert.IsNotNull(_sessionAwareSession.CoreServiceVersion);
            _sessionAwareSession.Dispose();
        }
    }
}