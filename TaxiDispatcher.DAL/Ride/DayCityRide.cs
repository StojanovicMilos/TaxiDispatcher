namespace TaxiDispatcher.DAL
{
    public class DayCityRide : Ride
    {
        protected override int RideCostMultiplier => 1;

        public DayCityRide(RideOrder rideOrder, Taxi taxi) : base(rideOrder, taxi) { }
    }
}
