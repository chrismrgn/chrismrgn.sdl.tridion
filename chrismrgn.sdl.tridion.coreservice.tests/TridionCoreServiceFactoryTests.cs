using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.tests
{
    [TestClass]
    public class TridionCoreServiceFactoryTests
    {
        [TestMethod]
        public void ReadInvalidComponent()
        {
            var item = TridionCoreServiceFactory.Get<ComponentData>("INVALID URI");
            Assert.IsNull(item);
        }
        [TestMethod]
        public void ReadComponent()
        {
            var item = TridionCoreServiceFactory.Get<ComponentData>("tcm:4-326");
            Assert.AreEqual("tcm:4-326", item.Id);
            Assert.IsInstanceOfType(item, typeof(ComponentData));
        }
        [TestMethod]
        public void ReadPageAsComponent()
        {
            var item = TridionCoreServiceFactory.Get<ComponentData>("tcm:4-299-64");
            Assert.IsNull(item);
        }
        [TestMethod]
        public void ReadPage()
        {
            var item = TridionCoreServiceFactory.Get<PageData>("tcm:4-299-64");
            Assert.AreEqual("tcm:4-299-64", item.Id);
            Assert.IsInstanceOfType(item, typeof(PageData));
        }
    }
}