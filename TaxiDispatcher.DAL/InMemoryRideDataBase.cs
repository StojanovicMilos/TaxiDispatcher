using System;
using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryRideDataBase : IDatabase
    {
        private static List<Ride> Rides = new List<Ride>();

        private InMemoryRideDataBase() { }

        private static readonly Lazy<InMemoryRideDataBase> lazy = new Lazy<InMemoryRideDataBase>(() => new InMemoryRideDataBase(), isThreadSafe: true);

        public static InMemoryRideDataBase Instance { get { return lazy.Value; } }

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
