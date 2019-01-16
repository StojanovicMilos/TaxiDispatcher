using System;
using TaxiDispatcher.Abstractions.Interfaces;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Extensions;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.BL
{
    public class Scheduler
    {
        private readonly IDatabase _database;
        private readonly TaxiContext _taxiContext;

        public Scheduler(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _taxiContext = new TaxiContext(database);
        }

        public Ride OrderRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            Taxi closestTaxi = FindClosestTaxi(rideOrder.StartLocation);
            return RideFactory.CreateRide(rideOrder, closestTaxi);
        }

        private const int MaximumOrderDistance = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            var allTaxis = _taxiContext.GetAllTaxis();
            Taxi closestTaxi = allTaxis.WithMinimum(t => t.DistanceTo(startLocation));
            if (closestTaxi.DistanceTo(startLocation) > MaximumOrderDistance)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        public void AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            Taxi rideTaxi = ride.RideTaxi;
            rideTaxi.AcceptRide(ride);
            var dbTaxi = rideTaxi.ToDbTaxi();
            _database.SaveExistingTaxi(dbTaxi);
            _database.SaveRide(ride.ToDbRide(dbTaxi));
        }
    }
}
