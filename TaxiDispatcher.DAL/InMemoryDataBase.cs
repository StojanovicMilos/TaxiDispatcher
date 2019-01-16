using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.Abstractions.Interfaces;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private readonly List<DbTaxiDTO> _taxis = new List<DbTaxiDTO> {
            new DbTaxiDTO { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocationDTO { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxiDTO { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocationDTO { CoordinateX = 4 }, TaxiCompany = "Naxi"},
            new DbTaxiDTO { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocationDTO { CoordinateX = 6 }, TaxiCompany = "Alfa"},
            new DbTaxiDTO { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocationDTO { CoordinateX = 7 }, TaxiCompany = "Gold"}
        };

        private static readonly List<DbRideDTO> Rides = new List<DbRideDTO>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> Lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance => Lazy.Value;

        public void SaveRide(DbRideDTO ride)
        {
            ride.RideId = GetNewRideId();
            Rides.Add(ride);
        }

        private const int StartingRideId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingRideId;

        public List<DbTaxiDTO> GetAllTaxis() => _taxis;

        public DbTaxiDTO GetTaxi(int id) =>_taxis.First(t => t.TaxiDriverId == id);

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
