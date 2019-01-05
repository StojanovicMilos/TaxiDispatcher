using System;
using System.Collections.Generic;
using TaxiDispatcher.App.CustomExceptions;
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

        public Ride OrderRide(int start, int destination, int rideType, DateTime rideDateTime)
        {
            Taxi closestTaxi = FindClosestTaxi(start);
            int ridePrice = CalculateRidePRice(start, destination, rideType, rideDateTime, closestTaxi);
            Ride ride = CreateRide(start, destination, closestTaxi, ridePrice);
            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        private Taxi FindClosestTaxi(int start)
        {
            Taxi closestTaxi = _database.Taxis[0];
            int min_distance = Math.Abs(_database.Taxis[0].Location - start);

            if (Math.Abs(_database.Taxis[1].Location - start) < min_distance)
            {
                closestTaxi = _database.Taxis[1];
                min_distance = Math.Abs(_database.Taxis[1].Location - start);
            }

            if (Math.Abs(_database.Taxis[2].Location - start) < min_distance)
            {
                closestTaxi = _database.Taxis[2];
                min_distance = Math.Abs(_database.Taxis[2].Location - start);
            }

            if (Math.Abs(_database.Taxis[3].Location - start) < min_distance)
            {
                closestTaxi = _database.Taxis[3];
                min_distance = Math.Abs(_database.Taxis[3].Location - start);
            }

            if (min_distance > 15)
                throw new NoAvailableTaxiVehiclesException();
            return closestTaxi;
        }

        private static int CalculateRidePRice(int start, int destination, int rideType, DateTime rideDateTime, Taxi closestTaxi)
        {
            int ridePrice;
            switch (closestTaxi.Taxi_company)
            {
                case "Naxi":
                    {
                        ridePrice = 10 * Math.Abs(start - destination);
                        break;
                    }
                case "Alfa":
                    {
                        ridePrice = 15 * Math.Abs(start - destination);
                        break;
                    }
                case "Gold":
                    {
                        ridePrice = 13 * Math.Abs(start - destination);
                        break;
                    }
                default:
                    {
                        throw new Exception("Ilegal company");
                    }
            }

            if (rideType == Constants.InterCity)
            {
                ridePrice *= 2;
            }

            if (rideDateTime.Hour < 6 || rideDateTime.Hour > 22)
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
