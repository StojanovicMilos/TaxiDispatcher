using System;
using TaxiDispatcher.BL.Locations;

namespace TaxiDispatcher.BL.Rides
{
    public enum RideType
    {
        City = 0,
        InterCity = 1
    };

    public class RideOrder
    {
        public Location StartLocation { get; }
        public Location DestinationLocation { get; }
        public DateTime RideDateTime { get; }

        //Client shouldn't know and/or choose weather a ride is city or intercity. That's BL. Assumption: one town is from -infinity to 10, the other one is from 11 to +infinity
        public RideType RideType => StartLocation.City == DestinationLocation.City ? RideType.City : RideType.InterCity;

        public RideOrder(Location startLocation, Location destinationLocation, DateTime rideDateTime)
        {
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            RideDateTime = rideDateTime;
        }
    }
}
