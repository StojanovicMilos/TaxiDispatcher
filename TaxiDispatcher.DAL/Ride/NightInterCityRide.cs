namespace TaxiDispatcher.DAL
{
    public class NightInterCityRide : Ride
    {
        protected override int RideCostMultiplier => 4;

        public NightInterCityRide(RideOrder rideOrder, Taxi taxi, int initialRidePrice) : base(rideOrder, taxi, initialRidePrice) { }
    }
}
