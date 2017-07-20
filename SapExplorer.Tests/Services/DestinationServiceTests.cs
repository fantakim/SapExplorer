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
    public class DestinationServiceTests
    {
        [TestMethod()]
        public void GetAllDestinationsTest()
        {
            var service = new DestinationService();
            var result = service.GetAllDestinations();
        }
    }
}