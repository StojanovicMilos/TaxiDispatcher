using System;
using TaxiDispatcher.App;
using TaxiDispatcher.App.CustomExceptions;
using TaxiDispatcher.Client.Tests.AppTests.SelectingClosestTaxi;
using TaxiDispatcher.Client.Tests.HelperClasses;
using TaxiDispatcher.DAL;
using Xunit;

namespace TaxiDispatcher.Client.Tests.AppTests.SelectingScosestTaxi
{
    public class RideOrderSelectsClosestTaxiTests
    {
        [Theory]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiRideOrdersUsedInClientTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiTestDataEdgeCases), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        public void GivenRideOrderSchedulerSelectsDriver(RideOrder rideOrder, int expectedDriverId)
        {
            //Arrange
            Scheduler scheduler = new Scheduler(new TestDatabase());

            //Act
            var ride = scheduler.OrderRide(rideOrder);
            scheduler.AcceptRide(ride);

            //Assert
            Assert.Equal(expectedDriverId, ride.RideTaxi.Taxi_driver_id);
        }

        [Theory]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.TooFarRideOrdersTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        public void GivenTooFarRideOrderSchedulerThrowsException(RideOrder rideOrder)
        {
            //Arrange
            const string expectedExceptionMessage = "There are no available taxi vehicles!";
            Scheduler scheduler = new Scheduler(new TestDatabase());

            //Act
            Action action = () => scheduler.OrderRide(rideOrder);

            //Assert
            var exception = Assert.Throws<NoAvailableTaxiVehiclesException>(action);
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
