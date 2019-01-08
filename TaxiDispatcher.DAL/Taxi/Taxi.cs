using System;

namespace TaxiDispatcher.DAL
{
    public abstract class Taxi
    {
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public string Taxi_company { get; set; }
        public Location CurrentLocation { get; set; }

        public int DistanceTo(Location startLocation) => startLocation.DistanceTo(CurrentLocation);

        protected abstract int PricePerDistance { get; }

        public int CalculateInitialRidePrice(RideOrder rideOrder) => rideOrder.Distance() * PricePerDistance;
    }
}
