using System;
using TaxiDispatcher.App.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App.Rides
{
    public class NightInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 4;

        public NightInterCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DbRide ToDbRide(DbTaxi dbTaxi)
        {
            DbRide dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.InterCity;
            return dbRide;
        }
    }
}
