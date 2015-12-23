using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsMiracle.WCF.Config;
using JsMiracle.Framework.Serialized;

namespace Jsmiracle.Infrastructure.UnitTests
{
    [TestClass]
    public class UnitTestSerialize
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ent = new WCFServiceConfigurationEntity();
            ent.Services = new WcfService[1];
            ent.Services[0] = new WcfService()
            {
                Address = "a另",
                ID = "001",
                ClientLibraryFile = "c:\\",
                ServiceID = "aa",
                Contract = "bb",
                ServiceLibraryFile = "d:\\",
                ServiceName = "bb"
            };

            var st = XmlSerialization.SerializeString(ent);

            var cpEnt = XmlSerialization.DeserializeString<WCFServiceConfigurationEntity>(st);

            if (cpEnt == null || cpEnt.Services == null || cpEnt.Services.Length != 1)
                Assert.Fail("initlization");

            if (ent.Services[0].ID != cpEnt.Services[0].ID)
                Assert.Fail("ID");

            if (ent.Services[0].ServiceID != cpEnt.Services[0].ServiceID)
                Assert.Fail("ServiceID");
            if (ent.Services[0].Contract != cpEnt.Services[0].Contract)
                Assert.Fail("Contract");
            if (ent.Services[0].Address != cpEnt.Services[0].Address)
                Assert.Fail("Address");
            if (ent.Services[0].ClientLibraryFile != cpEnt.Services[0].ClientLibraryFile)
                Assert.Fail("ClientLibraryFile");

            if (ent.Services[0].ServiceLibraryFile != cpEnt.Services[0].ServiceLibraryFile)
                Assert.Fail("ServiceLibraryFile");
        }
    }
}
