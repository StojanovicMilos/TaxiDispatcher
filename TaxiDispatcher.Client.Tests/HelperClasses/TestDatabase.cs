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
            new NaxiTaxi { Taxi_driver_id = 1, Taxi_driver_name = "Predrag", CurrentLocation = new Location{CoordinateX = 1 } },
            new NaxiTaxi { Taxi_driver_id = 2, Taxi_driver_name = "Nenad", CurrentLocation = new Location{CoordinateX = 4 } },
            new AlfaTaxi { Taxi_driver_id = 3, Taxi_driver_name = "Dragan", CurrentLocation = new Location{CoordinateX = 6 } },
            new GoldTaxi { Taxi_driver_id = 4, Taxi_driver_name = "Goran", CurrentLocation = new Location{CoordinateX = 7 } },
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

        public Taxi GetTaxi(int id) => Taxis.First(t => t.Taxi_driver_id == id);
    }
}
