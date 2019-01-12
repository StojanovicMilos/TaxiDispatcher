using System;
using TaxiDispatcher.App.CustomExceptions;
using TaxiDispatcher.App.Extensions;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        private readonly IDatabase _database;

        public Scheduler() : this(InMemoryDataBase.Instance) { }

        public Scheduler(IDatabase database)
        {
            _database = database;
        }

        public Ride OrderRide(RideOrder rideOrder)
        {
            Taxi closestTaxi = FindClosestTaxi(rideOrder.StartLocation);
            return RideFactory.CreateRide(rideOrder, closestTaxi);
        }

        private const int MAXIMUMORDERDISTANCE = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            var allTaxis = _database.GetAllTaxis();
            Taxi closestTaxi = allTaxis.WithMinimum(t => t.DistanceTo(startLocation));
            if (closestTaxi.DistanceTo(startLocation) > MAXIMUMORDERDISTANCE)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        public void AcceptRide(Ride ride)
        {
            _database.SaveRide(ride);
            Taxi rideTaxi = ride.RideTaxi;
            rideTaxi.AcceptRide(ride);
            rideTaxi.PerformRide(ride);
        }
    }
}
