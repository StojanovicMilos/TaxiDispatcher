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

        public Ride ToDomain(Taxi taxi) => new Ride(RideId, StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, Price);

        public DbRide(int rideId, Ride ride, DbTaxi dbTaxi)
        {
            RideId = rideId;
            StartLocation = new DbLocation(ride.StartLocation);
            DestinationLocation = new DbLocation(ride.DestinationLocation);
            RideTaxi = dbTaxi;
            Price = ride.Price;
            RideDateTime = ride.RideDateTime;
        }
    }
}