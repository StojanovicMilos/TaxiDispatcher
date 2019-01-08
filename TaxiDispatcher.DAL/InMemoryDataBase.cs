using System;
using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {

        Taxi[] IDatabase.Taxis { get { return _taxis; }}

        private Taxi[] _taxis = new Taxi[] {
            new NaxiTaxi { Taxi_driver_id = 1, Taxi_driver_name = "Predrag", CurrentLocation = new Location { CoordinateX = 1 } },
            new NaxiTaxi { Taxi_driver_id = 2, Taxi_driver_name = "Nenad", CurrentLocation = new Location { CoordinateX = 4 } },
            new AlfaTaxi { Taxi_driver_id = 3, Taxi_driver_name = "Dragan", CurrentLocation = new Location { CoordinateX = 6 } },
            new GoldTaxi { Taxi_driver_id = 4, Taxi_driver_name = "Goran", CurrentLocation = new Location { CoordinateX = 7 } },
        };

        private static List<Ride> Rides = new List<Ride>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance { get { return lazy.Value; } }

        public void SaveRide(Ride ride)
        {
            int max_id = Rides.Count == 0 ? 0 : Rides[0].Ride_id;
            foreach (Ride r in Rides)
            {
                if (r.Ride_id > max_id)
                    max_id = r.Ride_id;
            }

            ride.Ride_id = max_id + 1;
            Rides.Add(ride);
        }

        public Ride GetRide(int id)
        {
            Ride ride = Rides[0];
            bool found = ride.Ride_id == id;
            int current = 1;
            while (!found)
            {
                ride = Rides[current];
                found = ride.Ride_id == id;
                current += 1;
            }

            return ride;
        }

        public List<int> GetRide_Ids()
        {
            List<int> ids = new List<int>();
            foreach (Ride ride in Rides)
            {
                ids.Add(ride.Ride_id);
            }

            return ids;
        }
    }
}
