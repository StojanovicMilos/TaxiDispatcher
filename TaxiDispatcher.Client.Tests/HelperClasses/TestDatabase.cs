using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestDatabase : IDatabase
    {
        private readonly List<DbTaxi> _taxis = new List<DbTaxi>
        {
            new DbTaxi(taxiId: 1, taxiDriverName: "Predrag", currentLocation: new DbLocation(1), rides: new List<DbRide>(), taxiCompanyName: "Naxi"),
            new DbTaxi(taxiId: 2, taxiDriverName: "Nenad", currentLocation: new DbLocation(4), rides: new List<DbRide>(), taxiCompanyName: "Naxi"),
            new DbTaxi(taxiId: 3, taxiDriverName: "Dragan", currentLocation: new DbLocation(6), rides: new List<DbRide>(), taxiCompanyName: "Alfa"),
            new DbTaxi(taxiId: 4, taxiDriverName: "Goran", currentLocation: new DbLocation(7), rides: new List<DbRide>(), taxiCompanyName: "Gold")
        };

        private readonly List<DbRide> _rides = new List<DbRide>();

        private const int StartingRideId = 1;
        private int GetNewRideId() => _rides.Any() ? _rides.Max(r => r.RideId) + 1 : StartingRideId;

        public void SaveNewRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            int newId = GetNewRideId();
            _rides.Add(new DbRide(newId, ride));
        }

        public List<Taxi> GetAllTaxis() => _taxis.Select(t => t.ToDomain()).ToList();

        public Taxi GetTaxi(int id)
        {
            if (id < 0) throw new ArgumentException(nameof(id));
            return _taxis.First(t => t.TaxiId == id).ToDomain();
        }

        public void SaveExistingTaxi(Taxi dbTaxi)
        {
            if (dbTaxi == null) throw new ArgumentNullException(nameof(dbTaxi));
            var taxiInDb = _taxis.First(t => t.TaxiId == dbTaxi.TaxiId);
            taxiInDb.DriverName = dbTaxi.DriverName;
            taxiInDb.CurrentLocation = new DbLocation(dbTaxi.CurrentLocation);
            taxiInDb.TaxiCompanyName = dbTaxi.TaxiCompany.Name;
            taxiInDb.Rides = new List<DbRide>(dbTaxi.Rides.Select(r => new DbRide(r.RideId, r)));
        }
    }
}
