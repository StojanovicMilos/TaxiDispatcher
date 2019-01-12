using System;

namespace TaxiDispatcher.App
{
    public enum RideType { City = 0, InterCity = 1 };

    public class RideOrder
    {
        public Location StartLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public RideType RideType { get; set; }
        public DateTime RideDateTime { get; set; }
    }
}
