using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.Abstractions.Interfaces;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private readonly List<DbTaxiDTO> _taxis = new List<DbTaxiDTO> {
            new DbTaxiDTO { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocationDTO { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxiDTO { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocationDTO { CoordinateX = 4 }, TaxiCompany = "Naxi"  },
            new DbTaxiDTO { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocationDTO { CoordinateX = 6 }, TaxiCompany = "Alfa"  },
            new DbTaxiDTO { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocationDTO { CoordinateX = 7 }, TaxiCompany = "Gold"  }
        };

        private readonly List<DbRideDTO> _rides = new List<DbRideDTO>();
        public void SaveRide(DbRideDTO ride)
        {
            ride.RideId = GetNewRideId();
            _rides.Add(ride);
        }

        private const int StartingRideId = 1;
        private int GetNewRideId() => _rides.Any() ? _rides.Max(r => r.RideId) + 1 : StartingRideId;

        public List<DbTaxiDTO> GetAllTaxis() => _taxis;

        public DbTaxiDTO GetTaxi(int id) => _taxis.First(t => t.TaxiDriverId == id);

        public void SaveExistingTaxi(DbTaxiDTO dbTaxi)
        {
            var taxiInDb = GetTaxi(dbTaxi.TaxiDriverId);
            taxiInDb.TaxiDriverName = dbTaxi.TaxiDriverName;
            taxiInDb.CurrentLocation = dbTaxi.CurrentLocation;
            taxiInDb.TaxiCompany = dbTaxi.TaxiCompany;
            taxiInDb.DbRides = new List<DbRideDTO>();
            foreach (var dbRide in dbTaxi.DbRides)
            {
                taxiInDb.DbRides.Add(dbRide);
            }
        }
    }
}
