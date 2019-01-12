using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
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

        public abstract DBRide ToDBRide(DBTaxi dbTaxi);

        protected DBRide ToDBRideBase(DBTaxi dbTaxi)
        {
            return new DBRide
            {
                RideId = RideId,
                StartLocation = StartLocation.ToDBLocation(),
                DestinationLocation = DestinationLocation.ToDBLocation(),
                RideTaxi = dbTaxi,
                Price = Price,
                RideDateTime = RideDateTime,
            };
        }
    }
}
