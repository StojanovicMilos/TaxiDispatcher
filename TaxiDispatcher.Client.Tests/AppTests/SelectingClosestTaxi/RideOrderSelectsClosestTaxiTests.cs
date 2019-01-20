using System;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Schedulers;
using TaxiDispatcher.Tests.HelperClasses;
using Xunit;

namespace TaxiDispatcher.Tests.AppTests.SelectingClosestTaxi
{
    public class RideOrderSelectsClosestTaxiTests
    {
        [Theory]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiRideOrdersUsedInClientTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.SelectingClosestTaxiTestDataEdgeCases), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        public void GivenRideOrderSchedulerSelectsDriver(RideOrder rideOrder, int expectedTaxiId)
        {
            //Arrange
            Scheduler scheduler = new Scheduler(new TestDatabase());

            //Act
            var ride = scheduler.OrderRide(rideOrder);
            scheduler.AcceptRide(ride);

            //Assert
            Assert.Equal(expectedTaxiId, ride.RideTaxi.TaxiId);
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
