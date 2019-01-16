using System;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public static class RideFactory
    {
        public static Ride CreateRide(RideOrder rideOrder, Taxi closestTaxi)
        {
            bool dayRide = rideOrder.RideDateTime.Hour >= 6 && rideOrder.RideDateTime.Hour <= 22;
            bool cityRide = rideOrder.RideType == RideType.City;
            return CreateRide(rideOrder.StartLocation, rideOrder.DestinationLocation, rideOrder.RideDateTime, closestTaxi, dayRide, cityRide);
        }
        
        private static Ride CreateRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, bool dayRide, bool cityRide, int rideId = 0)
        {
            if (dayRide && cityRide)
                return new DayCityRide(startLocation, destinationLocation, rideDateTime, taxi, rideId);
            if (dayRide && !cityRide)
                return new DayInterCityRide(startLocation, destinationLocation, rideDateTime, taxi, rideId);
            if (!dayRide && cityRide)
                return new NightCityRide(startLocation, destinationLocation, rideDateTime, taxi, rideId);
            return new NightInterCityRide(startLocation, destinationLocation, rideDateTime, taxi, rideId);
        }

        public static Ride CreateRide(DbRideDTO dbRide, Taxi taxi)
        {
            bool dayRide = dbRide.RideDateTime.Hour >= 6 && dbRide.RideDateTime.Hour <= 22;
            bool cityRide = dbRide.RideType == (int)RideType.City;
            return CreateRide(new Location(dbRide.StartLocation), new Location(dbRide.DestinationLocation), dbRide.RideDateTime, taxi, dayRide, cityRide, dbRide.RideId);
        }
    }
}
