using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private readonly List<DbTaxi> _taxis = new List<DbTaxi> {
            new DbTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"},
            new DbTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"},
            new DbTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocation { CoordinateX = 7 }, TaxiCompany = "Gold"}
        };

        private static readonly List<DbRide> Rides = new List<DbRide>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> Lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance => Lazy.Value;

        public void SaveRide(DbRide ride)
        {
            ride.RideId = GetNewRideId();
            Rides.Add(ride);
        }

        private const int StartingRideId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingRideId;

        public List<DbTaxi> GetAllTaxis() => _taxis;

        public DbTaxi GetTaxi(int id) =>_taxis.First(t => t.TaxiDriverId == id);

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
