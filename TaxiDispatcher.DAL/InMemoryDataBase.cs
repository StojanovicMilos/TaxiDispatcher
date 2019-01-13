using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private List<DbTaxi> _taxis = new List<DbTaxi> {
            new DbTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DbLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DbTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DbLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"},
            new DbTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DbLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"},
            new DbTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DbLocation { CoordinateX = 7 }, TaxiCompany = "Gold"},
        };

        private static List<DbRide> Rides = new List<DbRide>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance { get { return lazy.Value; } }

        public void SaveRide(DbRide ride)
        {
            int max_id = Rides.Count == 0 ? 0 : Rides[0].RideId;
            foreach (DbRide r in Rides)
            {
                if (r.RideId > max_id)
                    max_id = r.RideId;
            }

            ride.RideId = max_id + 1;
            Rides.Add(ride);
        }

        public DbRide GetRide(int id)
        {
            DbRide ride = Rides[0];
            bool found = ride.RideId == id;
            int current = 1;
            while (!found)
            {
                ride = Rides[current];
                found = ride.RideId == id;
                current += 1;
            }

            return ride;
        }

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
