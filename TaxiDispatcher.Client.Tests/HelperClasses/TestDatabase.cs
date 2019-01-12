using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private List<DbRide> Rides = new List<DbRide>();

        private List<DbTaxi> Taxis { get; } = new List<DbTaxi> {
            new DbTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"  },
            new DbTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"  },
            new DbTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocation { CoordinateX = 7 }, TaxiCompany = "Gold"  }
        };

        private const int StartingId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingId;

        public DbRide GetRide(int id) => Rides.Where(r => r.RideId == id).First();

        public void SaveRide(DbRide ride)
        {
            ride.RideId = GetNewRideId();
            Rides.Add(ride);
        }

        public List<DbTaxi> GetAllTaxis() => Taxis;

        public DbTaxi GetTaxi(int id) => Taxis.First(t => t.TaxiDriverId == id);

        public void SaveExistingTaxi(DbTaxi dbTaxi)
        {
            var taxiInDb = GetTaxi(dbTaxi.TaxiDriverId);
            taxiInDb.TaxiDriverName = dbTaxi.TaxiDriverName;
            taxiInDb.CurrentLocation = dbTaxi.CurrentLocation;
            taxiInDb.TaxiCompany = dbTaxi.TaxiCompany;
            taxiInDb.DbRides = new List<DbRide>();
            foreach (var dbRide in dbTaxi.DbRides)
            {
                taxiInDb.DbRides.Add(dbRide);
            }
        }
    }
}
