using System;
using TaxiDispatcher.App;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.BL.Rides
{
    public class NightCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public NightCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DbRide ToDbRide(DbTaxi dbTaxi)
        {
            DbRide dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.City;
            return dbRide;
        }
    }
}
