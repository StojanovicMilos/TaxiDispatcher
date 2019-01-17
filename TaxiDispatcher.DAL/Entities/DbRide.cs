using System;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.DAL.Entities
{
    public class DbRide
    {
        public int RideId { get; }
        private DbLocation StartLocation { get; }
        private DbLocation DestinationLocation { get; }
        private DbTaxi RideTaxi { get; }
        private int Price { get; }
        private DateTime RideDateTime { get; }
        private int RideType { get; }

        public Ride ToDomain(Taxi taxi)
        {
            bool dayRide = RideDateTime.Hour >= 6 && RideDateTime.Hour <= 22;
            bool cityRide = RideType == (int) BL.Rides.RideType.City;

            if (dayRide && cityRide)
                return new DayCityRide(StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, RideId);
            if (dayRide && !cityRide)
                return new DayInterCityRide(StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, RideId);
            if (!dayRide && cityRide)
                return new NightCityRide(StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, RideId);
            return new NightInterCityRide(StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, RideId);
        }

        public DbRide(int rideId, Ride ride, DbTaxi dbTaxi)
        {
            RideId = rideId;
            StartLocation = new DbLocation(ride.StartLocation);
            DestinationLocation = new DbLocation(ride.DestinationLocation);
            RideTaxi = dbTaxi;
            Price = ride.Price;
            RideDateTime = ride.RideDateTime;
            RideType = ride.RideType;
        }
    }
}