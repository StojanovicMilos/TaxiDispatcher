using System;
using System.Collections.Generic;
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
            Ride ride = RideFactory.CreateRide(rideOrder, closestTaxi);
            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        private const int MAXIMUMORDERDISTANCE = 15;

        private Taxi FindClosestTaxi(Location startLocation)
        {
            Taxi closestTaxi = _database.Taxis.WithMinimum(t => t.DistanceTo(startLocation));
            if (closestTaxi.DistanceTo(startLocation) > MAXIMUMORDERDISTANCE)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        public void AcceptRide(Ride ride)
        {
            _database.SaveRide(ride);
            Taxi rideTaxi = ride.RideTaxi;
            rideTaxi.CurrentLocation = ride.DestinationLocation;
            Console.WriteLine("Ride accepted, waiting for driver: " + rideTaxi.Taxi_driver_name);
        }

        public List<Ride> GetRideList(int driver_id)
        {
            List<Ride> rides = new List<Ride>();
            List<int> ids = _database.GetRide_Ids();
            foreach (int id in ids)
            {
                Ride ride = _database.GetRide(id);
                if (ride.RideTaxi.Taxi_driver_id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }
    }
}
