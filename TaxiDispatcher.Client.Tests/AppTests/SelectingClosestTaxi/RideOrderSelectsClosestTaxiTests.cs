using System;
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
            var rideOrderResult = scheduler.OrderRide(rideOrder);
            var ride = rideOrderResult.Ride;
            scheduler.AcceptRide(ride);

            //Assert
            Assert.Equal(expectedTaxiId, ride.RideTaxi.TaxiId);
        }

        [Theory]
        [MemberData(nameof(RideOrderSelectsClosestTaxiTestData.TooFarRideOrdersTestData), MemberType = typeof(RideOrderSelectsClosestTaxiTestData))]
        public void GivenTooFarRideOrderSchedulerDoesNotReturnTaxi(RideOrder rideOrder)
        {
            //Arrange
            string expectedErrorMessage = "There are no available taxi vehicles!" + Environment.NewLine;
            Scheduler scheduler = new Scheduler(new TestDatabase());

            //Act
            var rideOrderResult = scheduler.OrderRide(rideOrder);

            //Assert
            Assert.False(rideOrderResult.Success);
            Assert.Equal(expectedErrorMessage, rideOrderResult.ErrorMessage);
        }
    }
}
