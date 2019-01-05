using System;
using TaxiDispatcher.App;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client.Tests.AppTests.SharedTestData
{
    public static class SharedRideDateTime
    {
        public static DateTime NightRideDateTime = new DateTime(2018, 1, 1, 23, 0, 0);
        public static DateTime DayRideDayTime = new DateTime(2018, 1, 1, 12, 0, 0);
    }

    public static class SharedRideOrderTestData
    {
        public static readonly RideOrder RideOrderUsedInClient1 = new RideOrder { Start = 5, Destination = 0, RideType = Constants.City, RideDateTime = new DateTime(2018, 1, 1, 23, 0, 0) };
        public static readonly RideOrder RideOrderUsedInClient2 = new RideOrder { Start = 0, Destination = 12, RideType = Constants.InterCity, RideDateTime = new DateTime(2018, 1, 1, 9, 0, 0) };
        public static readonly RideOrder RideOrderUsedInClient3 = new RideOrder { Start = 5, Destination = 0, RideType = Constants.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) };

        public static readonly RideOrder RideOrderNaxiTaxi = new RideOrder { Start = 0, Destination = 5, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayNaxiTaxi = new RideOrder { Start = 1, Destination = 0, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightNaxiTaxi = new RideOrder { Start = 1, Destination = 0, RideType = Constants.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayNaxiTaxi = new RideOrder { Start = 1, Destination = 0, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightNaxiTaxi = new RideOrder { Start = 1, Destination = 0, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };

        public static readonly RideOrder RideOrderAlfaTaxi = new RideOrder { Start = 6, Destination = 9, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayAlfaTaxi = new RideOrder { Start = 6, Destination = 5, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightAlfaTaxi = new RideOrder { Start = 6, Destination = 5, RideType = Constants.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayAlfaTaxi = new RideOrder { Start = 6, Destination = 5, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightAlfaTaxi = new RideOrder { Start = 6, Destination = 5, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };

        public static readonly RideOrder RideOrderGoldTaxi = new RideOrder { Start = 9, Destination = 5, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayGoldTaxi = new RideOrder { Start = 7, Destination = 8, RideType = Constants.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightGoldTaxi = new RideOrder { Start = 7, Destination = 8, RideType = Constants.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayGoldTaxi = new RideOrder { Start = 7, Destination = 8, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightGoldTaxi = new RideOrder { Start = 7, Destination = 8, RideType = Constants.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };
    }
}
