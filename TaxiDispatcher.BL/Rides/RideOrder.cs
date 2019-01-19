using System;
using TaxiDispatcher.BL.Locations;

namespace TaxiDispatcher.BL.Rides
{


    public class RideOrder
    {
        public Location StartLocation { get; }
        public Location DestinationLocation { get; }
        public DateTime RideDateTime { get; }

        public RideOrder(Location startLocation, Location destinationLocation, DateTime rideDateTime)
        {
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            RideDateTime = rideDateTime;
        }
    }
}
