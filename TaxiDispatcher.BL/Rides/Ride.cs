using System;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public abstract class Ride
    {
        private readonly int _rideId;
        private readonly Location _startLocation;
        private readonly DateTime _rideDateTime;

        public Location DestinationLocation { get; }
        public Taxi RideTaxi { get; }
        public int Price { get; }

        protected abstract int RidePriceMultiplier { get; }

        protected Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0)
        {
            RideTaxi = taxi ?? throw new ArgumentNullException(nameof(taxi));
            _startLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            _rideDateTime = rideDateTime;
            Price = taxi.CalculateInitialRidePrice(startLocation, destinationLocation) * RidePriceMultiplier;
            _rideId = rideId >= 0 ? rideId : throw new ArgumentException(nameof(rideId));
        }

        public abstract DbRideDTO ToDbRide(DbTaxiDTO dbTaxi);

        protected DbRideDTO ToDbRideBase(DbTaxiDTO dbTaxi) =>
            new DbRideDTO
            {
                RideId = _rideId,
                StartLocation = _startLocation.ToDbLocation(),
                DestinationLocation = DestinationLocation.ToDbLocation(),
                RideTaxi = dbTaxi,
                Price = Price,
                RideDateTime = _rideDateTime,
            };
    }
}
