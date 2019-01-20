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
        private int Price { get; }
        private DateTime RideDateTime { get; }

        public DbRide(int rideId, Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            RideId = rideId >= 0 ? rideId : throw new ArgumentException(nameof(rideId));
            StartLocation = new DbLocation(ride.StartLocation);
            DestinationLocation = new DbLocation(ride.DestinationLocation);
            Price = ride.Price;
            RideDateTime = ride.RideDateTime;
        }

        public Ride ToDomain(Taxi taxi)
        {
            if (taxi == null) throw new ArgumentNullException(nameof(taxi));
            return new Ride(RideId, StartLocation.ToDomain(), DestinationLocation.ToDomain(), RideDateTime, taxi, Price);
        }
    }
}