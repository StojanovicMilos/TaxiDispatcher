namespace TaxiDispatcher.DAL
{
    public abstract class Ride
    {
        public int Ride_id { get; set; }
        public int Location_from { get; set; }
        public int Location_to { get; set; }
        public Taxi RideTaxi { get; set; }
        public int Price { get; set; }

        protected abstract int RidePriceMultiplier { get; }

        protected Ride(RideOrder rideOrder, Taxi taxi)
        {
            RideTaxi = taxi;
            Location_from = rideOrder.Start;
            Location_to = rideOrder.Destination;
            Price = taxi.CalculateInitialRidePrice(rideOrder) * RidePriceMultiplier;
        }
    }
}
