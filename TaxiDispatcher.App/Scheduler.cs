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
            Taxi closestTaxi = FindClosestTaxi(rideOrder.Start);
            int ridePrice = CalculateRidePrice(rideOrder, closestTaxi);
            Ride ride = CreateRide(rideOrder.Start, rideOrder.Destination, closestTaxi, ridePrice);
            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        private const int MAXIMUMORDERDISTANCE = 15;

        private Taxi FindClosestTaxi(int start)
        {
            Taxi closestTaxi = _database.Taxis.WithMinimum(t => t.DistanceTo(start));
            if (closestTaxi.DistanceTo(start) > MAXIMUMORDERDISTANCE)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        private static int CalculateRidePrice(RideOrder rideOrder, Taxi closestTaxi)
        {
            int ridePrice = closestTaxi.CalculateInitialRidePrice(rideOrder);

            if (rideOrder.RideType == Constants.InterCity)
            {
                ridePrice *= 2;
            }

            if (rideOrder.RideDateTime.Hour < 6 || rideOrder.RideDateTime.Hour > 22)
            {
                ridePrice *= 2;
            }

            return ridePrice;
        }

        private static Ride CreateRide(int start, int destination, Taxi closestTaxi, int ridePrice)
        {
            Ride ride = new Ride
            {
                Taxi_driver_id = closestTaxi.Taxi_driver_id,
                Location_from = start,
                Location_to = destination,
                Taxi_driver_name = closestTaxi.Taxi_driver_name,
                Price = ridePrice
            };
            return ride;
        }

        public void AcceptRide(Ride ride)
        {
            _database.SaveRide(ride);

            if (_database.Taxis[0].Taxi_driver_id == ride.Taxi_driver_id)
            {
                _database.Taxis[0].Location = ride.Location_to;
            }

            if (_database.Taxis[1].Taxi_driver_id == ride.Taxi_driver_id)
            {
                _database.Taxis[1].Location = ride.Location_to;
            }

            if (_database.Taxis[2].Taxi_driver_id == ride.Taxi_driver_id)
            {
                _database.Taxis[2].Location = ride.Location_to;
            }

            if (_database.Taxis[3].Taxi_driver_id == ride.Taxi_driver_id)
            {
                _database.Taxis[3].Location = ride.Location_to;
            }

            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi_driver_name);
        }

        public List<Ride> GetRideList(int driver_id)
        {
            List<Ride> rides = new List<Ride>();
            List<int> ids = _database.GetRide_Ids();
            foreach (int id in ids)
            {
                Ride ride = _database.GetRide(id);
                if (ride.Taxi_driver_id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }
    }
}
