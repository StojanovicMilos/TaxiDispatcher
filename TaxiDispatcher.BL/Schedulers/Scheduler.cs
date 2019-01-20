using System;
using TaxiDispatcher.BL.Extensions;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Schedulers
{
    public class Scheduler : IScheduler
    {
        private readonly IDatabase _database;

        public Scheduler(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public RideOrderResult OrderRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            Taxi closestTaxi = FindClosestTaxi(rideOrder.StartLocation);
            if (closestTaxi.DistanceTo(rideOrder.StartLocation) > MaximumOrderDistance)
                return new RideOrderResult("There are no available taxi vehicles!" + Environment.NewLine);
            return new RideOrderResult(new Ride(rideOrder, closestTaxi));
        }

        private const int MaximumOrderDistance = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            var allTaxis = _database.GetAllTaxis();
            return allTaxis.WithMinimum(t => t.DistanceTo(startLocation));
        }

        public Taxi AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            var rideTaxi = ride.RideTaxi;
            rideTaxi.AcceptRide(ride);
            _database.SaveExistingTaxi(rideTaxi);
            _database.SaveNewRide(ride);
            return rideTaxi;
        }
    }
}
