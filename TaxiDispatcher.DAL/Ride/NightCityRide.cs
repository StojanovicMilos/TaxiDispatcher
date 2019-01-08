namespace TaxiDispatcher.DAL
{
    public class NightCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public NightCityRide(RideOrder rideOrder, Taxi taxi) : base(rideOrder, taxi) { }
    }
}
