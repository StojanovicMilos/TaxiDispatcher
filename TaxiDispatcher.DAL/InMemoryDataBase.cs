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
            new DbTaxi(taxiDriverId: 1, taxiDriverName: "Predrag", currentLocation: new DbLocation(1), taxiCompany: "Naxi"),
            new DbTaxi(taxiDriverId: 2, taxiDriverName: "Nenad", currentLocation: new DbLocation(4), taxiCompany: "Naxi"),
            new DbTaxi(taxiDriverId: 3, taxiDriverName: "Dragan", currentLocation: new DbLocation(6), taxiCompany: "Alfa"),
            new DbTaxi(taxiDriverId: 4, taxiDriverName: "Goran", currentLocation: new DbLocation(7), taxiCompany: "Gold")
        };

        private static readonly List<DbRide> Rides = new List<DbRide>();

        private InMemoryDataBase() { }

        private static readonly Lazy<InMemoryDataBase> Lazy = new Lazy<InMemoryDataBase>(() => new InMemoryDataBase(), isThreadSafe: true);

        public static InMemoryDataBase Instance => Lazy.Value;

        private const int StartingRideId = 1;
        private int GetNewRideId() => Rides.Any() ? Rides.Max(r => r.RideId) + 1 : StartingRideId;

        public void SaveRide(Ride ride)
        {
            int newId = GetNewRideId();
            DbTaxi rideTaxi = Taxis.First(t => t.TaxiDriverId == ride.RideTaxi.TaxiDriverId);
            Rides.Add(new DbRide(newId, ride, rideTaxi));
        }

        public List<Taxi> GetAllTaxis() => Taxis.Select(t => t.ToDomain()).ToList();

        public Taxi GetTaxi(int id) => Taxis.First(t => t.TaxiDriverId == id).ToDomain();

        public void SaveExistingTaxi(Taxi dbTaxi)
        {
            var taxiInDb = Taxis.First(t => t.TaxiDriverId == dbTaxi.TaxiDriverId);
            taxiInDb.TaxiDriverName = dbTaxi.TaxiDriverName;
            taxiInDb.CurrentLocation = new DbLocation(dbTaxi.CurrentLocation);
            taxiInDb.TaxiCompany = dbTaxi.TaxiCompany;
            taxiInDb.DbRides = new List<DbRide>();
            foreach (var ride in dbTaxi.Rides)
            {
                taxiInDb.DbRides.Add(new DbRide(ride.RideId, ride, taxiInDb));
            }
        }
    }
}
