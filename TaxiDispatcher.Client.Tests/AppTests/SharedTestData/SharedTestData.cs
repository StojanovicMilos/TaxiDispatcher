using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.Tests.AppTests.SharedTestData
{
    public static class SharedRideDateTime
    {
        public static DateTime NightRideDateTime = new DateTime(2018, 1, 1, 23, 0, 0);
        public static DateTime DayRideDayTime = new DateTime(2018, 1, 1, 12, 0, 0);
    }

    public static class SharedRideOrderTestData
    {
        public static readonly RideOrder RideOrderUsedInClient1 = new RideOrder (startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 23, 0, 0));
        public static readonly RideOrder RideOrderUsedInClient2 = new RideOrder (startLocation: new Location(0), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 9, 0, 0));
        public static readonly RideOrder RideOrderUsedInClient3 = new RideOrder (startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0));

        public static readonly RideOrder RideOrderNaxiTaxi = new RideOrder (startLocation: new Location(0), destinationLocation: new Location(5), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityDayNaxiTaxi = new RideOrder(startLocation: new Location(1), destinationLocation: new Location(0), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityNightNaxiTaxi = new RideOrder(startLocation: new Location(1), destinationLocation: new Location(0), rideDateTime: SharedRideDateTime.NightRideDateTime);
        public static readonly RideOrder RideOrderInterCityDayNaxiTaxi = new RideOrder(startLocation: new Location(1), destinationLocation: new Location(11), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderInterCityNightNaxiTaxi = new RideOrder(startLocation: new Location(1), destinationLocation: new Location(11), rideDateTime: SharedRideDateTime.NightRideDateTime);

        public static readonly RideOrder RideOrderAlfaTaxi = new RideOrder(startLocation: new Location(6), destinationLocation: new Location(9), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityDayAlfaTaxi = new RideOrder(startLocation: new Location(6), destinationLocation: new Location(5), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityNightAlfaTaxi = new RideOrder(startLocation: new Location(6), destinationLocation: new Location(5), rideDateTime: SharedRideDateTime.NightRideDateTime);
        public static readonly RideOrder RideOrderInterCityDayAlfaTaxi = new RideOrder(startLocation: new Location(6), destinationLocation: new Location(16), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderInterCityNightAlfaTaxi = new RideOrder(startLocation: new Location(6), destinationLocation: new Location(16), rideDateTime: SharedRideDateTime.NightRideDateTime);

        public static readonly RideOrder RideOrderGoldTaxi = new RideOrder(startLocation: new Location(9), destinationLocation: new Location(5), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityDayGoldTaxi = new RideOrder(startLocation: new Location(7), destinationLocation: new Location(8), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderCityNightGoldTaxi = new RideOrder(startLocation: new Location(7), destinationLocation: new Location(8), rideDateTime: SharedRideDateTime.NightRideDateTime);
        public static readonly RideOrder RideOrderInterCityDayGoldTaxi = new RideOrder(startLocation: new Location(7), destinationLocation: new Location(17), rideDateTime: SharedRideDateTime.DayRideDayTime);
        public static readonly RideOrder RideOrderInterCityNightGoldTaxi = new RideOrder(startLocation: new Location(7), destinationLocation: new Location(17), rideDateTime: SharedRideDateTime.NightRideDateTime);
    }
}
