using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class NightCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public NightCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DBRide ToDBRide(DBTaxi dbTaxi)
        {
            DBRide dbRide = ToDBRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.City;
            return dbRide;
        }
    }
}
