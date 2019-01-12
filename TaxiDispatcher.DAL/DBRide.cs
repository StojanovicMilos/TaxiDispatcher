using System;

namespace TaxiDispatcher.DAL
{
    public class DBRide
    {
        public int RideId { get; set; }
        public DBLocation StartLocation { get; set; }
        public DBLocation DestinationLocation { get; set; }
        public DBTaxi RideTaxi { get; set; }
        public int Price { get; set; }
        public DateTime RideDateTime { get; set; }
        public int RideType { get; set; }
    }
}