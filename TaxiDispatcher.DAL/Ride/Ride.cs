namespace TaxiDispatcher.DAL
{
    public abstract class Ride
    {
        public int Ride_id { get; set; }
        public int Location_from { get; set; }
        public int Location_to { get; set; }
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public int Price { get; set; }

        protected abstract int RidePriceMultiplier { get; }

        protected Ride(RideOrder rideOrder, Taxi taxi)
        {
            Taxi_driver_id = taxi.Taxi_driver_id;
            Location_from = rideOrder.Start;
            Location_to = rideOrder.Destination;
            Taxi_driver_name = taxi.Taxi_driver_name;
            Price = taxi.CalculateInitialRidePrice(rideOrder) * RidePriceMultiplier;
        }
    }
}
