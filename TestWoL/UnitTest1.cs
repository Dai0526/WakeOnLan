using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WakeOnLan;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace TestWoL
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfiguration()
        {
            Configuration cfg = new Configuration();
            cfg.ReadConfigXml(@"E:\Development\FMITools\WakeOnLan\TestWoL\data\WoL.xml");

            // Test constructor
            Assert.AreNotEqual(cfg, null);
            Assert.AreNotEqual(cfg.m_pcInfo, null);

            // Test Record
            var data = cfg.m_pcInfo;
            Assert.AreEqual(data.Count, 2);

            // Test Record Details
            ComputerInfo info = null;
            bool status = data.TryGetValue("tianhua", out info);
            Assert.AreEqual(status, true);

            Assert.AreEqual(info.description, "");
            Assert.AreEqual(info.id, "tianhua");

            IPAddress ip = IPAddress.Parse("192.168.68.141");
            Assert.AreEqual(info.IP, ip);

            PhysicalAddress mac = PhysicalAddress.Parse("24-6E-96-24-66-44"); ;
            Assert.AreEqual(info.macAddress, mac);
        }
    }
}
