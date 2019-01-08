namespace TaxiDispatcher.DAL
{
    public class DayInterCityRide : Ride
    {
        protected override int RidePriceMultiplier => 2;

        public DayInterCityRide(RideOrder rideOrder, Taxi taxi) : base(rideOrder, taxi) { }
    }
}
