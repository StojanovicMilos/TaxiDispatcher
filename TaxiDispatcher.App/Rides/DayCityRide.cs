using System;
using TaxiDispatcher.App.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App.Rides
{
    public class DayCityRide : Ride
    {
        protected override int RidePriceMultiplier => 1;

        public DayCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DbRide ToDbRide(DbTaxi dbTaxi)
        {
            DbRide dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.City;
            return dbRide;
        }
    }
}
