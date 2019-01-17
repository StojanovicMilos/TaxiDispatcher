using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public class DayCityRide : Ride
    {
        protected override int RidePriceMultiplier => 1;
        public override int RideType { get; } = (int) Rides.RideType.City;

        public DayCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0) 
            : base(startLocation, destinationLocation, rideDateTime, taxi, rideId)
        {
        }
    }
}
