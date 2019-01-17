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
            new DbTaxi(taxiDriverId: 1, taxiDriverName: "Predrag", currentLocation: new DbLocation(1), taxiCompany: "Naxi"),
            new DbTaxi(taxiDriverId: 2, taxiDriverName: "Nenad", currentLocation: new DbLocation(4), taxiCompany: "Naxi"),
            new DbTaxi(taxiDriverId: 3, taxiDriverName: "Dragan", currentLocation: new DbLocation(6), taxiCompany: "Alfa"),
            new DbTaxi(taxiDriverId: 4, taxiDriverName: "Goran", currentLocation: new DbLocation(7), taxiCompany: "Gold")
        };

        private readonly List<DbRide> _rides = new List<DbRide>();

        private const int StartingRideId = 1;
        private int GetNewRideId() => _rides.Any() ? _rides.Max(r => r.RideId) + 1 : StartingRideId;

        public void SaveRide(Ride ride)
        {
            int newId = GetNewRideId();
            DbTaxi rideTaxi = _taxis.First(t => t.TaxiDriverId == ride.RideTaxi.TaxiDriverId);
            _rides.Add(new DbRide(newId, ride, rideTaxi));
        }

        public List<Taxi> GetAllTaxis() => _taxis.Select(t => t.ToDomain()).ToList();

        public Taxi GetTaxi(int id) => _taxis.First(t => t.TaxiDriverId == id).ToDomain();

        public void SaveExistingTaxi(Taxi dbTaxi)
        {
            var taxiInDb = _taxis.First(t => t.TaxiDriverId == dbTaxi.TaxiDriverId);
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
