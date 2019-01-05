﻿using System;
using System.Collections.Generic;
using TaxiDispatcher.App;
using TaxiDispatcher.Client.Tests.AppTests.SharedTestData;

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
                new object[] {new RideOrder {Start = -11, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)}, DriverId(1)},
                new object[] {new RideOrder {Start = 19, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)}, DriverId(4)},
            };

        public static IEnumerable<object[]> TooFarRideOrdersTestData =>
            new List<object[]>
            {
                new object[] {new RideOrder {Start = -15, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {Start = 23, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {Start = int.MaxValue, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} },
                new object[] {new RideOrder {Start = int.MinValue, Destination = 0, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)} }
            };
    }
}
