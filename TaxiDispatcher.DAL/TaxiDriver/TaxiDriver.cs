﻿using System;

namespace TaxiDispatcher.DAL
{
    public abstract class Taxi
    {
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public string Taxi_company { get; set; }
        public int Location { get; set; }

        public int DistanceTo(int start) => Math.Abs(start - Location);

        protected abstract int PricePerDistance { get; }

        public int CalculateInitialRidePrice(RideOrder rideOrder) => rideOrder.Distance() * PricePerDistance;
    }
}
