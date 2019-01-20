using System;
using System.Collections.Generic;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.Tests.AppTests.SharedTestData;

namespace TaxiDispatcher.Tests.AppTests.SelectingClosestTaxi
{
    public class RideOrderSelectsClosestTaxiTestData
    {
        private static int TaxiId(int id) => id;

        public static IEnumerable<object[]> SelectingClosestTaxiRideOrdersUsedInClientTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient1, TaxiId(2)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient2, TaxiId(1)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient3, TaxiId(2)}
            };

        public static IEnumerable<object[]> SelectingClosestTaxiTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderNaxiTaxi, TaxiId(1)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayNaxiTaxi, TaxiId(1)},
                new object[] {SharedRideOrderTestData.RideOrderAlfaTaxi, TaxiId(3)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayAlfaTaxi, TaxiId(3)},
                new object[] {SharedRideOrderTestData.RideOrderGoldTaxi, TaxiId(4)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayGoldTaxi, TaxiId(4)}
            };

        public static IEnumerable<object[]> SelectingClosestTaxiTestDataEdgeCases =>
            new List<object[]>
            {
                new object[] {new RideOrder (startLocation: new Location(-11), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)), TaxiId(1)},
                new object[] {new RideOrder (startLocation: new Location(19), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)), TaxiId(4)},
            };

        public static IEnumerable<object[]> TooFarRideOrdersTestData =>
            new List<object[]>
            {
                new object[] {new RideOrder (startLocation: new Location(-15), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)) },
                new object[] {new RideOrder (startLocation: new Location(23), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)) },
                new object[] {new RideOrder (startLocation: new Location(int.MinValue), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)) },
                new object[] {new RideOrder (startLocation: new Location(int.MaxValue), destinationLocation: new Location(0), rideDateTime:  new DateTime(2018, 1, 1, 23, 0, 0)) },
            };
    }
}
