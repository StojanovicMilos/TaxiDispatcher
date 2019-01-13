using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.BL.Rides
{
    public abstract class Ride
    {
        public int RideId { get; set; }
        public Location StartLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public Taxi RideTaxi { get; set; }
        public int Price { get; set; }
        public DateTime RideDateTime { get; set; }

        protected abstract int RidePriceMultiplier { get; }

        protected Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi)
        {
            RideTaxi = taxi;
            StartLocation = startLocation;
            DestinationLocation = destinationLocation;
            RideDateTime = rideDateTime;
            Price = taxi.CalculateInitialRidePrice(startLocation, destinationLocation) * RidePriceMultiplier;
        }

        public abstract DbRide ToDbRide(DbTaxi dbTaxi);

        protected DbRide ToDbRideBase(DbTaxi dbTaxi)
        {
            return new DbRide
            {
                RideId = RideId,
                StartLocation = StartLocation.ToDbLocation(),
                DestinationLocation = DestinationLocation.ToDbLocation(),
                RideTaxi = dbTaxi,
                Price = Price,
                RideDateTime = RideDateTime,
            };
        }
    }
}
