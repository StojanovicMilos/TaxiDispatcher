using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public static class RideFactory
    {
        public static Ride CreateRide(RideOrder rideOrder, Taxi closestTaxi)
        {
            int initialRidePrice = closestTaxi.CalculateInitialRidePrice(rideOrder);
            bool dayRide = rideOrder.RideDateTime.Hour >= 6 && rideOrder.RideDateTime.Hour <= 22;
            bool cityRide = rideOrder.RideType == Constants.City;
            if (dayRide && cityRide)
                return new DayCityRide(rideOrder, closestTaxi, initialRidePrice);
            if (dayRide && !cityRide)
                return new DayInterCityRide(rideOrder, closestTaxi, initialRidePrice);
            if (!dayRide && cityRide)
                return new NightCityRide(rideOrder, closestTaxi, initialRidePrice);
            return new NightInterCityRide(rideOrder, closestTaxi, initialRidePrice);
        }
    }
}
