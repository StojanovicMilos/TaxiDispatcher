using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxiDispatcher.Tests.HelperClasses;

namespace TaxiDispatcher.Client.Tests.OverallFunctionalityTests
{
    [TestClass]
    public class OverallFunctionalityTests
    {
        [TestMethod]
        public void OverallFunctionalityTest()
        {
            //arrange
            TestLogger logger = new TestLogger();
            TaxiDispatcherClient taxiDispatcherClient = new TaxiDispatcherClient(logger);
            string expectedMessages = "Ordering ride from 5 to 0..." + Environment.NewLine + Environment.NewLine + "Ordering ride from 0 to 12..."
                + Environment.NewLine + Environment.NewLine + "Ordering ride from 5 to 0..." + Environment.NewLine + Environment.NewLine + "Ordering ride from 35 to 12..."
                + Environment.NewLine + "There are no available taxi vehicles!" + Environment.NewLine + Environment.NewLine + "Driver with ID = 2 earned today:"
                + Environment.NewLine + "Price: 100" + Environment.NewLine + "Price: 240" + Environment.NewLine + "Total: 340" + Environment.NewLine;

            //act
            taxiDispatcherClient.Run();

            //assert
            Assert.AreEqual(expectedMessages, logger.AllMessages);
        }
    }
}
