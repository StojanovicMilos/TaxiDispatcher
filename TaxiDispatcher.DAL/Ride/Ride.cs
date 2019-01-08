namespace TaxiDispatcher.DAL
{
    public abstract class Ride
    {
        public int Ride_id { get; set; }
        public Location StartLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public Taxi RideTaxi { get; set; }
        public int Price { get; set; }

        protected abstract int RidePriceMultiplier { get; }

        protected Ride(RideOrder rideOrder, Taxi taxi)
        {
            RideTaxi = taxi;
            StartLocation = rideOrder.StartLocation;
            DestinationLocation = rideOrder.DestinationLocation;
            Price = taxi.CalculateInitialRidePrice(rideOrder) * RidePriceMultiplier;
        }
    }
}
