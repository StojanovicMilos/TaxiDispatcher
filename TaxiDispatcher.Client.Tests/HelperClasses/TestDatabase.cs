using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private readonly List<DbTaxi> _taxis = new List<DbTaxi> {
            new DbTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"  },
            new DbTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"  },
            new DbTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocation { CoordinateX = 7 }, TaxiCompany = "Gold"  }
        };

        private readonly List<DbRide> _rides = new List<DbRide>();
        public void SaveRide(DbRide ride)
        {
            ride.RideId = GetNewRideId();
            _rides.Add(ride);
        }

        private const int StartingRideId = 1;
        private int GetNewRideId() => _rides.Any() ? _rides.Max(r => r.RideId) + 1 : StartingRideId;

        public List<DbTaxi> GetAllTaxis() => _taxis;

        public DbTaxi GetTaxi(int id) => _taxis.First(t => t.TaxiDriverId == id);

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
