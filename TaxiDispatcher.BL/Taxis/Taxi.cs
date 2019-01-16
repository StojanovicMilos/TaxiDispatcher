using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.BL.Taxis
{
    public abstract class Taxi
    {
        private Location _currentLocation;
        
        protected abstract int PricePerDistance { get; }

        public int TaxiDriverId { get; }
        public string TaxiDriverName { get; }
        public List<Ride> Rides { get; } = new List<Ride>();

        protected Taxi(DbTaxiDTO dbTaxi)
        {
            if (dbTaxi == null)
                throw new ArgumentNullException(nameof(dbTaxi));
            if (dbTaxi.CurrentLocation == null)
                throw new ArgumentNullException(nameof(dbTaxi.CurrentLocation));

            TaxiDriverId = dbTaxi.TaxiDriverId;
            TaxiDriverName = dbTaxi.TaxiDriverName;
            _currentLocation = new Location(dbTaxi.CurrentLocation);
            foreach (var dbRide in dbTaxi.DbRides)
            {
                if (dbRide == null)
                    throw new ArgumentNullException(nameof(dbRide));
                Rides.Add(RideFactory.CreateRide(dbRide, this));
            }
        }

        public int CalculateInitialRidePrice(Location startLocation, Location destinationLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            if (destinationLocation == null) throw new ArgumentNullException(nameof(destinationLocation));
            return startLocation.DistanceTo(destinationLocation) * PricePerDistance;
        }

        public int DistanceTo(Location startLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            return startLocation.DistanceTo(_currentLocation);
        }

        public int CalculateTotalEarnings() => Rides.Sum(r => r.Price);

        public void AcceptRide(Ride ride)
        {
            if (ride == null)
                throw new ArgumentNullException(nameof(ride));
            Rides.Add(ride);
            _currentLocation = ride.DestinationLocation;
        }

        public abstract DbTaxiDTO ToDbTaxi();

        protected DbTaxiDTO ToDbTaxiBase()
        {
            var dbTaxi = new DbTaxiDTO
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
