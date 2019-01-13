using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Rides
{
    public class NightInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 4;

        public NightInterCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0) 
            : base(startLocation, destinationLocation, rideDateTime, taxi, rideId)
        {
        }

        public override DbRide ToDbRide(DbTaxi dbTaxi)
        {
            DbRide dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.InterCity;
            return dbRide;
        }
    }
}
