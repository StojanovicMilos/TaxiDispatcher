namespace TaxiDispatcher.DAL
{
    public class DayCityRide : Ride
    {
        protected override int RideCostMultiplier => 1;

        public DayCityRide(RideOrder rideOrder, Taxi taxi, int initialRidePrice) : base(rideOrder, taxi, initialRidePrice) { }
    }
}
