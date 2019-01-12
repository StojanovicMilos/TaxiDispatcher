using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class DayCityRide : Ride
    {
        protected override int RidePriceMultiplier => 1;

        public DayCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DBRide ToDBRide(DBTaxi dbTaxi)
        {
            DBRide dbRide = ToDBRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.City;
            return dbRide;
        }
    }
}
