using System.Net;
using System.Net.NetworkInformation;
using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.tests
{
    [TestClass]
    public class TridionCoreServiceExtentionMethodTests
    {
        [TestMethod]
        public void CheckOutPage()
        {
            var item = TridionCoreServiceFactory.Get<PageData>("tcm:4-299-64");
            item = item.CheckOutItem();
            Assert.IsTrue(item.LockInfo.LockType == LockType.CheckedOut);
        }

        [TestMethod]
        public void CheckInPage()
        {
            var item = TridionCoreServiceFactory.Get<PageData>("tcm:4-299-64");
            item = item.CheckOutItem();
            Assert.IsTrue(item.LockInfo.LockType == LockType.CheckedOut);
            item = item.CheckInItem();
            Assert.IsTrue(item.LockInfo.LockType == LockType.None);
        }
    }
}