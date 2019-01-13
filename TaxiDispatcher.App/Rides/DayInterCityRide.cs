using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Rides
{
    public class DayInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public DayInterCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi) : base(startLocation, destinationLocation, rideDateTime, taxi) { }

        public override DbRide ToDbRide(DbTaxi dbTaxi)
        {
            DbRide dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.InterCity;
            return dbRide;
        }
    }
}
