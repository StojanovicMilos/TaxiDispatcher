using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public abstract class Taxi
    {
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public string Taxi_company { get; set; }
        public Location CurrentLocation { get; set; }
        public List<Ride> Rides { get; set; } = new List<Ride>();
        protected abstract int PricePerDistance { get; }

        public int CalculateInitialRidePrice(RideOrder rideOrder) => rideOrder.Distance() * PricePerDistance;

        public int DistanceTo(Location startLocation) => startLocation.DistanceTo(CurrentLocation);

        public void PerformRide(Ride ride) => CurrentLocation = ride.DestinationLocation;

        public void AcceptRide(Ride ride) => Rides.Add(ride);
    }
}
