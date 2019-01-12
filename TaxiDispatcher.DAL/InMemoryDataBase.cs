using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private List<DBTaxi> _taxis = new List<DBTaxi> {
            new DBTaxi { TaxiDriverId = 1, TaxiDriverName = "Predrag", CurrentLocation = new DBLocation { CoordinateX = 1 }, TaxiCompany = "Naxi" },
            new DBTaxi { TaxiDriverId = 2, TaxiDriverName = "Nenad", CurrentLocation = new DBLocation { CoordinateX = 4 }, TaxiCompany = "Naxi"},
            new DBTaxi { TaxiDriverId = 3, TaxiDriverName = "Dragan", CurrentLocation = new DBLocation { CoordinateX = 6 }, TaxiCompany = "Alfa"},
            new DBTaxi { TaxiDriverId = 4, TaxiDriverName = "Goran", CurrentLocation = new DBLocation { CoordinateX = 7 }, TaxiCompany = "Gold"},
        };

        private static List<DBRide> Rides = new List<DBRide>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance { get { return lazy.Value; } }

        public void SaveRide(DBRide ride)
        {
            int max_id = Rides.Count == 0 ? 0 : Rides[0].RideId;
            foreach (DBRide r in Rides)
            {
                if (r.RideId > max_id)
                    max_id = r.RideId;
            }

            ride.RideId = max_id + 1;
            Rides.Add(ride);
        }

        public DBRide GetRide(int id)
        {
            DBRide ride = Rides[0];
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

        public List<DBTaxi> GetAllTaxis() => _taxis;

        public DBTaxi GetTaxi(int id) =>_taxis.First(t => t.TaxiDriverId == id);

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
