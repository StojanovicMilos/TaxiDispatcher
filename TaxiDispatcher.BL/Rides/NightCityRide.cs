using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public class NightCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;
        public override int RideType { get; } = (int) Rides.RideType.City;

        public NightCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0)
            : base(startLocation, destinationLocation, rideDateTime, taxi, rideId)
        {
        }
    }
}
