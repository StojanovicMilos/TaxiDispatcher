using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class DayInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public DayInterCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DBRide ToDBRide(DBTaxi dbTaxi)
        {
            DBRide dbRide = ToDBRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.InterCity;
            return dbRide;
        }
    }
}
