using System;

namespace TaxiDispatcher.Abstractions.DbDTO
{
    public class DbRideDTO
    {
        public int RideId { get; set; }
        public DbLocationDTO StartLocation { get; set; }
        public DbLocationDTO DestinationLocation { get; set; }
        public DbTaxiDTO RideTaxi { get; set; }
        public int Price { get; set; }
        public DateTime RideDateTime { get; set; }
        public int RideType { get; set; }
    }
}