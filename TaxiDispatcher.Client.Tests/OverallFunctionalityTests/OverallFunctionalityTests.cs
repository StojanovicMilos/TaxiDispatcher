using System;
using TaxiDispatcher.BL;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.Client;
using TaxiDispatcher.Tests.HelperClasses;
using Xunit;

namespace TaxiDispatcher.Tests.OverallFunctionalityTests
{
    public class OverallFunctionalityTests
    {
        [Fact]
        public void OverallFunctionalityTest()
        {
            //arrange
            TestLogger logger = new TestLogger();
            TestDatabase testDatabase = new TestDatabase();
            Scheduler testDatabaseScheduler = new Scheduler(testDatabase);
            TaxiContext testDatabaseTaxiContext = new TaxiContext(testDatabase);
            TaxiDispatcherClient taxiDispatcherClient = new TaxiDispatcherClient(logger, testDatabaseScheduler, testDatabaseTaxiContext);
            string expectedMessages = "Ordering ride from 5 to 0..." + Environment.NewLine + "Ride ordered, price: 100" + Environment.NewLine + 
                                      "Ride accepted, waiting for driver: Nenad" + Environment.NewLine + Environment.NewLine + 
                                      "Ordering ride from 0 to 12..." + Environment.NewLine + "Ride ordered, price: 240" + Environment.NewLine + 
                                      "Ride accepted, waiting for driver: Nenad" + Environment.NewLine + Environment.NewLine + 
                                      "Ordering ride from 5 to 0..." + Environment.NewLine + "Ride ordered, price: 75" + Environment.NewLine + 
                                      "Ride accepted, waiting for driver: Dragan" + Environment.NewLine + Environment.NewLine + 
                                      "Ordering ride from 35 to 12..." + Environment.NewLine + 
                                      "There are no available taxi vehicles!" + Environment.NewLine + Environment.NewLine + 
                                      "Driver with ID = 2 earned today:" + Environment.NewLine + "Price: 100" + Environment.NewLine + "Price: 240" + 
                                      Environment.NewLine + "Total: 340" + Environment.NewLine;
            //act
            taxiDispatcherClient.Run();

            //assert
            Assert.Equal(expectedMessages, logger.AllMessages);
        }
    }
}
