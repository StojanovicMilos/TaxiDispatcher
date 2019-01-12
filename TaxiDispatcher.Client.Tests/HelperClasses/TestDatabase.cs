using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private List<Ride> Rides = new List<Ride>();

        private Taxi[] Taxis { get; } = new Taxi[] {
            new NaxiTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new Location{CoordinateX = 1 } },
            new NaxiTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new Location{CoordinateX = 4 } },
            new AlfaTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new Location{CoordinateX = 6 } },
            new GoldTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new Location{CoordinateX = 7 } },
        };

        private const int StartingId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.Ride_id) + 1 : StartingId;

        public Ride GetRide(int id) => Rides.Where(r => r.Ride_id == id).First();

        public void SaveRide(Ride ride)
        {
            ride.Ride_id = GetNewRideId();
            Rides.Add(ride);
        }

        public Taxi[] GetAllTaxis() => Taxis;

        public Taxi GetTaxi(int id) => Taxis.First(t => t.TaxiDriverId == id);
    }
}
