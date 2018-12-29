using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public class InMemoryRideDataBase : IDatabase
    {
        public static List<Ride> Rides = new List<Ride>();

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

    public class Taxi
    {
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public string Taxi_company { get; set; }
        public int Location { get; set; }
    }

    public class Ride
    {
        public int Ride_id { get; set; }
        public int Location_from { get; set; }
        public int Location_to { get; set; }
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public int Price { get; set; }
    }
}
