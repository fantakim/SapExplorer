using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapExplorer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Services.Tests
{
    [TestClass()]
    public class SapRfcServiceTests
    {
        [TestMethod()]
        public void GetFunctionsTest()
        {
            var service = new SapRfcService();
            service.SetDestination("AEKYUNG_DEV");

            var result = service.GetFunctions("Z_*");
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void GetFunctionParametersTest()
        {
            var service = new SapRfcService();
            service.SetDestination("AEKYUNG_DEV");

            var result = service.GetFunctionParameters("Z_HR_EDU_GW_DLS");
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void DestinationChangeTest()
        {
            var service = new SapRfcService();
            service.SetDestination("AEKYUNG_DEV");

            var result = service.GetFunctionParameters("Z_NOTES_RESALES_MM");
            Assert.IsTrue(result.Success);

            service.SetDestination("AKBB_JJQ_SAP");

            var result2 = service.GetFunctionParameters("ZIF_MM_MATREQ_LIST");
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void GetTableTest()
        {
            var service = new SapRfcService();
            service.SetDestination("AEKYUNG_DEV");

            var result = service.GetTable("PS3274");
            Assert.IsTrue(result.Success);
        }
    }
}