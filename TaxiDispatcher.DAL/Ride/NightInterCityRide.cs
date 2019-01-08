namespace TaxiDispatcher.DAL
{
    public class NightInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 4;

        public NightInterCityRide(RideOrder rideOrder, Taxi taxi) : base(rideOrder, taxi) { }
    }
}
