﻿using System;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public class DayInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public DayInterCityRide(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId = 0) 
            : base(startLocation, destinationLocation, rideDateTime, taxi, rideId)
        {
        }

        public override DbRideDTO ToDbRide(DbTaxiDTO dbTaxi)
        {
            DbRideDTO dbRide = ToDbRideBase(dbTaxi);
            dbRide.RideType = (int)RideType.InterCity;
            return dbRide;
        }
    }
}