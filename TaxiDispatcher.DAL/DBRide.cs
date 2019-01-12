using System;

namespace TaxiDispatcher.DAL
{
    public class DbRide
    {
        public int RideId { get; set; }
        public DbLocation StartLocation { get; set; }
        public DbLocation DestinationLocation { get; set; }
        public DbTaxi RideTaxi { get; set; }
        public int Price { get; set; }
        public DateTime RideDateTime { get; set; }
        public int RideType { get; set; }
    }
}