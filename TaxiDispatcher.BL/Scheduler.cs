using System;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Extensions;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL
{
    public class Scheduler
    {
        private readonly IDatabase _database;

        public Scheduler(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public Ride OrderRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            Taxi closestTaxi = FindClosestTaxi(rideOrder.StartLocation);
            return new Ride(rideOrder, closestTaxi);
        }

        private const int MaximumOrderDistance = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            var allTaxis = _database.GetAllTaxis();
            var closestTaxi = allTaxis.WithMinimum(t => t.DistanceTo(startLocation));
            if (closestTaxi.DistanceTo(startLocation) > MaximumOrderDistance)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        public Taxi AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            var rideTaxi = ride.RideTaxi;
            rideTaxi.AcceptRide(ride);
            _database.SaveExistingTaxi(rideTaxi);
            _database.SaveRide(ride);
            return rideTaxi;
        }
    }
}
