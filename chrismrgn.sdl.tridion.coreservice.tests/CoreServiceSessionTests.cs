using System.Net;
using System.Net.NetworkInformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chrismrgn.sdl.tridion.coreservice.tests
{
    [TestClass]
    public class CoreServiceSessionTests
    {
        private CoreServiceSession _session;
        private CoreServiceSession _sessionAwareSession;

        [TestInitialize()]
        public void Initialize()
        {
            _session = TridionCoreServiceFactory.CreateCoreServiceSession();
            _sessionAwareSession = TridionCoreServiceFactory.CreateCoreServiceSession(true);
        }
        [TestMethod]
        public void TestCoreServiceSession()
        {
            Assert.IsNotNull(_session);
            Assert.IsNotNull(_session.CoreServiceVersion);
        }
        [TestMethod]
        public void TestSessionAwareCoreServiceSession()
        {
            Assert.IsNotNull(_sessionAwareSession);
            Assert.IsNotNull(_sessionAwareSession.CoreServiceVersion);
        }
    }
}