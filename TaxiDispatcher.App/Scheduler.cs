﻿using TaxiDispatcher.App;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Extensions;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.BL
{
    public class Scheduler
    {
        private readonly IDatabase _database;
        private readonly TaxiContext _taxiContext;

        public Scheduler() : this(InMemoryDataBase.Instance) { }

        public Scheduler(IDatabase database)
        {
            _database = database;
            _taxiContext = new TaxiContext(database);
        }

        public Ride OrderRide(RideOrder rideOrder)
        {
            Taxi closestTaxi = FindClosestTaxi(rideOrder.StartLocation);
            return RideFactory.CreateRide(rideOrder, closestTaxi);
        }

        private const int MAXIMUMORDERDISTANCE = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            var allTaxis = _taxiContext.GetAllTaxis();
            Taxi closestTaxi = allTaxis.WithMinimum(t => t.DistanceTo(startLocation));
            if (closestTaxi.DistanceTo(startLocation) > MAXIMUMORDERDISTANCE)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        public void AcceptRide(Ride ride)
        {
            Taxi rideTaxi = ride.RideTaxi;
            rideTaxi.AcceptRide(ride);
            var dbTaxi = rideTaxi.ToDbTaxi();
            _database.SaveExistingTaxi(dbTaxi);
            _database.SaveRide(ride.ToDbRide(dbTaxi));
        }
    }
}
