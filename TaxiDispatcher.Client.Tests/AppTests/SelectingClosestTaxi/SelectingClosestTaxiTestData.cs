﻿using System;
using System.Collections.Generic;
using TaxiDispatcher.App;
using TaxiDispatcher.Client.Tests.AppTests.SharedTestData;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client.Tests.AppTests.SelectingClosestTaxi
{
    public class RideOrderSelectsClosestTaxiTestData
    {
        private static int DriverId(int id) => id;

        public static IEnumerable<object[]> SelectingClosestTaxiRideOrdersUsedInClientTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient1, DriverId(2)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient2, DriverId(1)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient3, DriverId(2)}
            };

        public static IEnumerable<object[]> SelectingClosestTaxiTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderNaxiTaxi, DriverId(1)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayNaxiTaxi, DriverId(1)},
                new object[] {SharedRideOrderTestData.RideOrderAlfaTaxi, DriverId(3)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayAlfaTaxi, DriverId(3)},
                new object[] {SharedRideOrderTestData.RideOrderGoldTaxi, DriverId(4)},
                new object[] {SharedRideOrderTestData.RideOrderCityDayGoldTaxi, DriverId(4)}
            };

        public static IEnumerable<object[]> SelectingClosestTaxiTestDataEdgeCases =>
            new List<object[]>
            {
                new object[] {new RideOrder {StartLocation = new Location(-11), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)}, DriverId(1)},
                new object[] {new RideOrder {StartLocation = new Location(19), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)}, DriverId(4)},
            };

        public static IEnumerable<object[]> TooFarRideOrdersTestData =>
            new List<object[]>
            {
                new object[] {new RideOrder {StartLocation = new Location(-15), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {StartLocation = new Location(23), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {StartLocation = new Location(int.MinValue), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {StartLocation = new Location(int.MaxValue), DestinationLocation = new Location(0), RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
            };
    }
}
