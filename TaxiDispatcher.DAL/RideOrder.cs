using System;

namespace TaxiDispatcher.DAL
{
    public class RideOrder
    {
        public Location StartLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public int RideType { get; set; }
        public DateTime RideDateTime { get; set; }

        public int Distance() => StartLocation.DistanceTo(DestinationLocation);
    }
}
