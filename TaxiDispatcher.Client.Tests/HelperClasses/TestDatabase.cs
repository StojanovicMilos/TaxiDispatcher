using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private List<DBRide> Rides = new List<DBRide>();

        private List<DBTaxi> Taxis { get; } = new List<DBTaxi> {
            new DBTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DBLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DBTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DBLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"  },
            new DBTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DBLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"  },
            new DBTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DBLocation { CoordinateX = 7 }, TaxiCompany = "Gold"  }
        };

        private const int StartingId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingId;

        public DBRide GetRide(int id) => Rides.Where(r => r.RideId == id).First();

        public void SaveRide(DBRide ride)
        {
            ride.RideId = GetNewRideId();
            Rides.Add(ride);
        }

        public List<DBTaxi> GetAllTaxis() => Taxis;

        public DBTaxi GetTaxi(int id) => Taxis.First(t => t.TaxiDriverId == id);

        public void SaveExistingTaxi(DBTaxi dbTaxi)
        {
            var taxiInDb = GetTaxi(dbTaxi.TaxiDriverId);
            taxiInDb.TaxiDriverName = dbTaxi.TaxiDriverName;
            taxiInDb.CurrentLocation = dbTaxi.CurrentLocation;
            taxiInDb.TaxiCompany = dbTaxi.TaxiCompany;
            taxiInDb.DBRides = new List<DBRide>();
            foreach (var dbRide in dbTaxi.DBRides)
            {
                taxiInDb.DBRides.Add(dbRide);
            }
        }
    }
}
