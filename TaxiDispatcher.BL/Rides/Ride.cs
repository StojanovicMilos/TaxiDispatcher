using System;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Rides
{
    public class Ride
    {
        public int RideId { get; }
        public Location StartLocation { get; }
        public Location DestinationLocation { get; }
        public DateTime RideDateTime { get; }
        public Taxi RideTaxi { get; }
        public int Price { get; }

        public Ride(RideOrder rideOrder, Taxi taxi) : this(rideOrder.StartLocation, rideOrder.DestinationLocation, rideOrder.RideDateTime, taxi)
        {
            RideId = 0;
            Price = taxi.CalculateInitialRidePrice(rideOrder.StartLocation, rideOrder.DestinationLocation) * CalculateRidePriceMultiplier(rideOrder.RideDateTime);
        }

        private Ride(Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi)
        {
            StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            DestinationLocation = destinationLocation ?? throw new ArgumentNullException(nameof(destinationLocation));
            RideDateTime = rideDateTime;
            RideTaxi = taxi ?? throw new ArgumentNullException(nameof(taxi));
        }

        private int CalculateRidePriceMultiplier(DateTime rideDateTime)
        {
            //Client shouldn't know and/or choose weather a ride is city or intercity. That's BL.
            bool cityRide = StartLocation.City == DestinationLocation.City;

            bool dayRide = rideDateTime.Hour >= 6 && rideDateTime.Hour <= 22;

            return (cityRide ? 1 : 2) * (dayRide ? 1 : 2);
        }

        public Ride(int rideId, Location startLocation, Location destinationLocation, DateTime rideDateTime, Taxi taxi, int price) : this(startLocation, destinationLocation, rideDateTime, taxi)
        {
            RideId = rideId >= 0 ? rideId : throw new ArgumentException(nameof(rideId));
            Price = price > 0 ? price : throw new ArgumentException(nameof(Price));
        }
    }
}