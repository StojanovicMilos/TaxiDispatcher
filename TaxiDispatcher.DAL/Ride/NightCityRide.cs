namespace TaxiDispatcher.DAL
{
    public class NightCityRide : Ride
    {
        protected override int RideCostMultiplier => 2;

        public NightCityRide(RideOrder rideOrder, Taxi taxi) : base(rideOrder, taxi) { }
    }
}
