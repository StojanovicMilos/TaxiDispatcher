﻿namespace TaxiDispatcher.DAL
{
    public class Ride
    {
        public int Ride_id { get; set; }
        public int Location_from { get; set; }
        public int Location_to { get; set; }
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public int Price { get; set; }
    }
}
