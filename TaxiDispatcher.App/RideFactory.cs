using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public static class RideFactory
    {
        public static Ride CreateRide(RideOrder rideOrder, Taxi closestTaxi)
        {
            bool dayRide = rideOrder.RideDateTime.Hour >= 6 && rideOrder.RideDateTime.Hour <= 22;
            bool cityRide = rideOrder.RideType == Constants.City;
            if (dayRide && cityRide)
                return new DayCityRide(rideOrder, closestTaxi);
            if (dayRide && !cityRide)
                return new DayInterCityRide(rideOrder, closestTaxi);
            if (!dayRide && cityRide)
                return new NightCityRide(rideOrder, closestTaxi);
            return new NightInterCityRide(rideOrder, closestTaxi);
        }
    }
}
