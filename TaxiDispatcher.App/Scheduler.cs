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

        public Ride OrderRide(int location_from, int location_to, int ride_type, DateTime time)
        {
            #region FindingTheBestVehicle 

            Taxi min_taxi = _database.Taxis[0];
            int min_distance = Math.Abs(_database.Taxis[0].Location - location_from);

            if (Math.Abs(_database.Taxis[1].Location - location_from) < min_distance)
            {
                min_taxi = _database.Taxis[1];
                min_distance = Math.Abs(_database.Taxis[1].Location - location_from);
            }

            if (Math.Abs(_database.Taxis[2].Location - location_from) < min_distance)
            {
                min_taxi = _database.Taxis[2];
                min_distance = Math.Abs(_database.Taxis[2].Location - location_from);
            }

            if (Math.Abs(_database.Taxis[3].Location - location_from) < min_distance)
            {
                min_taxi = _database.Taxis[3];
                min_distance = Math.Abs(_database.Taxis[3].Location - location_from);
            }

            if (min_distance > 15)
                throw new NoAvailableTaxiVehiclesException();

            #endregion

            #region CreatingRide

            Ride ride = new Ride();
            ride.Taxi_driver_id = min_taxi.Taxi_driver_id;
            ride.Location_from = location_from;
            ride.Location_to = location_to;
            ride.Taxi_driver_name = min_taxi.Taxi_driver_name;

            #endregion

            #region CalculatingPrice

            switch (min_taxi.Taxi_company)
            {
                case "Naxi":
                {
                    ride.Price = 10 * Math.Abs(location_from - location_to);
                    break;
                }
                case "Alfa":
                {
                    ride.Price = 15 * Math.Abs(location_from - location_to);
                    break;
                }
                case "Gold":
                {
                    ride.Price = 13 * Math.Abs(location_from - location_to);
                    break;
                }
                default:
                {
                    throw new Exception("Ilegal company");
                }
            }

            if (ride_type == Constants.InterCity)
            {
                ride.Price *= 2;
            }

            if (time.Hour < 6 || time.Hour > 22)
            {
                ride.Price *= 2;
            }

            #endregion

            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
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
