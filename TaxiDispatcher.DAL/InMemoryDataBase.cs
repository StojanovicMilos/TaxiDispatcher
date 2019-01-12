using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private Taxi[] _taxis = new Taxi[] {
            new NaxiTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new Location { CoordinateX = 1 } },
            new NaxiTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new Location { CoordinateX = 4 } },
            new AlfaTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new Location { CoordinateX = 6 } },
            new GoldTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new Location { CoordinateX = 7 } },
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

        public Taxi[] GetAllTaxis() => _taxis;

        public Taxi GetTaxi(int id) =>_taxis.First(t => t.TaxiDriverId == id);
    }
}
