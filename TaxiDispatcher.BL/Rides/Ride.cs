using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public class Ride
    {
        private enum RideType
        {
            City = 0,
            InterCity = 1
        };

        public int RideId { get; }
        public Location StartLocation { get; }
        public Location DestinationLocation { get; }
        public DateTime RideDateTime { get; }
        public Taxi RideTaxi { get; }
        public int Price { get; }

        public Ride(RideOrder rideOrder, Taxi taxi) : this(rideOrder.StartLocation, rideOrder.DestinationLocation, rideOrder.RideDateTime, taxi)
        {
            RideId = 0;
            Price = CalculateRidePrice(rideOrder.StartLocation, rideOrder.DestinationLocation, rideOrder.RideDateTime, taxi);
        }

        private Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi)
        {
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            RideDateTime = rideDateTime;
            RideTaxi = taxi ?? throw new ArgumentNullException(nameof(taxi));
        }

        private int CalculateRidePrice(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi)
        {
            //Client shouldn't know and/or choose weather a ride is city or intercity. That's BL. Assumption: one town is from -infinity to 10, the other one is from 11 to +infinity
            RideType rideType = StartLocation.City == DestinationLocation.City ? RideType.City : RideType.InterCity;
            bool cityRide = rideType == RideType.City;

            bool dayRide = rideDateTime.Hour >= 6 && rideDateTime.Hour <= 22;

            int ridePriceMultiplier = (cityRide ? 1 : 2) * (dayRide ? 1 : 2);
            return taxi.CalculateInitialRidePrice(startLocation, destinationLocation) * ridePriceMultiplier;
        }

        public Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int rideId, int price) : this(startLocation, destinationLocation, rideDateTime, taxi)
        {
            RideId = rideId >= 0 ? rideId : throw new ArgumentException(nameof(rideId));
            Price = price > 0 ? price : throw new ArgumentException(nameof(Price));
        }
    }
}