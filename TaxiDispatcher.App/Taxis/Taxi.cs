using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Taxis
{
    public abstract class Taxi
    {
        private Location _currentLocation;
        
        protected abstract int PricePerDistance { get; }

        public int TaxiDriverId { get; }
        public string TaxiDriverName { get; }
        public List<Ride> Rides { get; } = new List<Ride>();

        protected Taxi(DbTaxi dbTaxi)
        {
            TaxiDriverId = dbTaxi.TaxiDriverId;
            TaxiDriverName = dbTaxi.TaxiDriverName;
            _currentLocation = new Location(dbTaxi.CurrentLocation);
            foreach (var dbRide in dbTaxi.DbRides)
            {
                Rides.Add(RideFactory.CreateRide(dbRide, this));
            }
        }

        public int CalculateInitialRidePrice(Location startLocation, Location destinationLocation) => startLocation.DistanceTo(destinationLocation) * PricePerDistance;

        public int DistanceTo(Location startLocation) => startLocation.DistanceTo(_currentLocation);

        public int CalculateTotalEarnings() => Rides.Sum(r => r.Price);

        public void AcceptRide(Ride ride)
        {
            Rides.Add(ride);
            _currentLocation = ride.DestinationLocation;
        }

        public abstract DbTaxi ToDbTaxi();

        protected DbTaxi ToDbTaxiBase()
        {
            var dbTaxi = new DbTaxi
            {
                TaxiDriverId = TaxiDriverId,
                TaxiDriverName = TaxiDriverName,
                CurrentLocation = _currentLocation.ToDbLocation()
            };
            foreach (var ride in Rides)
            {
                dbTaxi.DbRides.Add(ride.ToDbRide(dbTaxi));
            }
            return dbTaxi;
        }
    }
}
