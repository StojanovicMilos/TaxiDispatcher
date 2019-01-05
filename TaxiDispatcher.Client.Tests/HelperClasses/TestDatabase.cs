using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private List<Ride> Rides = new List<Ride>();

        public void SaveRide(Ride ride)
        {
            ride.Ride_id = GetNewRideId();
            Rides.Add(ride);
        }

        public Taxi[] Taxis { get; } = new Taxi[] {
            new NaxiTaxi { Taxi_driver_id = 1, Taxi_driver_name = "Predrag", Location = 1 },
            new NaxiTaxi { Taxi_driver_id = 2, Taxi_driver_name = "Nenad", Location = 4 },
            new AlfaTaxi { Taxi_driver_id = 3, Taxi_driver_name = "Dragan", Location = 6 },
            new GoldTaxi { Taxi_driver_id = 4, Taxi_driver_name = "Goran", Location = 7 },
        };

        private const int StartingId = 1;
        private int GetNewRideId()
        {
            return Rides.Any() ? Rides.Max(r => r.Ride_id) + 1 : StartingId;
        }

        public Ride GetRide(int id)
        {
            return Rides.Where(r => r.Ride_id == id).First();
        }

        public List<int> GetRide_Ids()
        {
            return Rides.Select(x => x.Ride_id).ToList();
        }
    }
}
