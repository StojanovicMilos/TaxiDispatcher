﻿using TaxiDispatcher.App;
using TaxiDispatcher.Client.Tests.HelperClasses;
using Xunit;

namespace TaxiDispatcher.Client.Tests.AppTests.CalculatingRidePrice
{
    public class CalculatingRidePriceTests
    {  
        [Theory]
        [MemberData(nameof(CalculatingRidePriceTestData.CalculatingRidePricesForRideOrdersUsedInClientTestData), MemberType = typeof(CalculatingRidePriceTestData))]
        [MemberData(nameof(CalculatingRidePriceTestData.CalculatingRidePricesForNaxiTaxiTestData), MemberType = typeof(CalculatingRidePriceTestData))]
        [MemberData(nameof(CalculatingRidePriceTestData.CalculatingRidePricesForAlfaTaxiTestData), MemberType = typeof(CalculatingRidePriceTestData))]
        [MemberData(nameof(CalculatingRidePriceTestData.CalculatingRidePricesForGoldTaxiTestData), MemberType = typeof(CalculatingRidePriceTestData))]
        public void GivenRideOrderPriceIs(RideOrder rideOrder, int expectedRidePrice)
        {
            //Arrange
            Scheduler scheduler = new Scheduler(new TestDatabase());

            //Act
            var ride = scheduler.OrderRide(rideOrder.Start, rideOrder.Destination, rideOrder.RideType, rideOrder.RideDateTime);
            scheduler.AcceptRide(ride);

            //Assert
            Assert.Equal(expectedRidePrice, ride.Price);
        }
    }    
}