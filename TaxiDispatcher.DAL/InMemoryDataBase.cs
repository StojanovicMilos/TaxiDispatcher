using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.DAL
{
    public sealed class InMemoryDataBase : IDatabase
    {
        private static readonly List<DbTaxi> Taxis = new List<DbTaxi>
        {
            new DbTaxi(taxiId: 1, taxiDriverName: "Predrag", currentLocation: new DbLocation(1), rides: new List<DbRide>(), taxiCompanyName: "Naxi"),
            new DbTaxi(taxiId: 2, taxiDriverName: "Nenad", currentLocation: new DbLocation(4), rides: new List<DbRide>(), taxiCompanyName: "Naxi"),
            new DbTaxi(taxiId: 3, taxiDriverName: "Dragan", currentLocation: new DbLocation(6), rides: new List<DbRide>(), taxiCompanyName: "Alfa"),
            new DbTaxi(taxiId: 4, taxiDriverName: "Goran", currentLocation: new DbLocation(7), rides: new List<DbRide>(), taxiCompanyName: "Gold")
        };

        private static readonly List<DbRide> Rides = new List<DbRide>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> Lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance => Lazy.Value;

        private const int StartingRideId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingRideId;

        public void SaveNewRide(Ride ride)
        {
            int newId = GetNewRideId();
            Rides.Add(new DbRide(newId, ride));
        }

        public List<Taxi> GetAllTaxis() => Taxis.Select(t => t.ToDomain()).ToList();

        public Taxi GetTaxi(int id) => Taxis.First(t => t.TaxiId == id).ToDomain();

        public void SaveExistingTaxi(Taxi dbTaxi)
        {
            var taxiInDb = Taxis.First(t => t.TaxiId == dbTaxi.TaxiId);
            taxiInDb.DriverName = dbTaxi.DriverName;
            taxiInDb.CurrentLocation = new DbLocation(dbTaxi.CurrentLocation);
            taxiInDb.TaxiCompanyName = dbTaxi.TaxiCompany.Name;
            taxiInDb.Rides = new List<DbRide>(dbTaxi.Rides.Select(r => new DbRide(r.RideId, r)));
        }
    }
}
