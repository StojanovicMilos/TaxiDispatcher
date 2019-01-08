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
        public static readonly RideOrder RideOrderUsedInClient1 = new RideOrder { StartLocation = new Location{ CoordinateX =  5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 23, 0, 0) };
        public static readonly RideOrder RideOrderUsedInClient2 = new RideOrder { StartLocation = new Location{ CoordinateX =  0 }, DestinationLocation = new Location { CoordinateX = 12 }, RideType = RideType.InterCity, RideDateTime = new DateTime(2018, 1, 1, 9, 0, 0) };
        public static readonly RideOrder RideOrderUsedInClient3 = new RideOrder { StartLocation = new Location { CoordinateX = 5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) };

        public static readonly RideOrder RideOrderNaxiTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 0 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayNaxiTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 1 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightNaxiTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 1 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayNaxiTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 1 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightNaxiTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 1 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };

        public static readonly RideOrder RideOrderAlfaTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 6 }, DestinationLocation = new Location { CoordinateX = 9 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayAlfaTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 6 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightAlfaTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 6 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayAlfaTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 6 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightAlfaTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 6 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };

        public static readonly RideOrder RideOrderGoldTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 9 }, DestinationLocation = new Location { CoordinateX = 5 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityDayGoldTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 7 }, DestinationLocation = new Location { CoordinateX = 8 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderCityNightGoldTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 7 }, DestinationLocation = new Location { CoordinateX = 8 }, RideType = RideType.City, RideDateTime = SharedRideDateTime.NightRideDateTime };
        public static readonly RideOrder RideOrderInterCityDayGoldTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 7 }, DestinationLocation = new Location { CoordinateX = 8 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.DayRideDayTime };
        public static readonly RideOrder RideOrderInterCityNightGoldTaxi = new RideOrder { StartLocation = new Location { CoordinateX = 7 }, DestinationLocation = new Location { CoordinateX = 8 }, RideType = RideType.InterCity, RideDateTime = SharedRideDateTime.NightRideDateTime };
    }
}
