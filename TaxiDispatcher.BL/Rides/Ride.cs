using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public abstract class Ride
    {
        public int RideId { get; }
        public Location StartLocation { get; }
        public DateTime RideDateTime { get; }

        public Location DestinationLocation { get; }
        public Taxi RideTaxi { get; }
        public int Price { get; }

        protected abstract int RidePriceMultiplier { get; }
        public abstract int RideType { get; }

        protected Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0)
        {
            RideTaxi = taxi ?? throw new ArgumentNullException(nameof(taxi));
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            RideDateTime = rideDateTime;
            Price = taxi.CalculateInitialRidePrice(startLocation, destinationLocation) * RidePriceMultiplier;
            RideId = rideId >= 0 ? rideId : throw new ArgumentException(nameof(rideId));
        }
    }
}