using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Taxis
{
    public abstract class Taxi
    {
        public int TaxiDriverId { get; set; }
        public string TaxiDriverName { get; set; }
        public Location CurrentLocation { get; set; }
        public List<Ride> Rides { get; set; } = new List<Ride>();
        protected abstract int PricePerDistance { get; }

        protected Taxi(DbTaxi dbTaxi)
        {
            TaxiDriverId = dbTaxi.TaxiDriverId;
            TaxiDriverName = dbTaxi.TaxiDriverName;
            CurrentLocation = new Location(dbTaxi.CurrentLocation);
            foreach (var dbRide in dbTaxi.DbRides)
            {
                Rides.Add(RideFactory.CreateRide(dbRide, this));
            }
        }

        public int CalculateInitialRidePrice(Location startLocation, Location destinationLocation) => startLocation.DistanceTo(destinationLocation) * PricePerDistance;

        public int DistanceTo(Location startLocation) => startLocation.DistanceTo(CurrentLocation);

        public int CalculateTotalEarnings() => Rides.Sum(r => r.Price);

        public void AcceptRide(Ride ride)
        {
            Rides.Add(ride);
            CurrentLocation = ride.DestinationLocation;
        }

        public abstract DbTaxi ToDbTaxi();

        protected DbTaxi ToDbTaxiBase()
        {
            var dbTaxi = new DbTaxi
            {
                TaxiDriverId = TaxiDriverId,
                TaxiDriverName = TaxiDriverName,
                CurrentLocation = CurrentLocation.ToDbLocation()
            };
            foreach (var ride in Rides)
            {
                dbTaxi.DbRides.Add(ride.ToDbRide(dbTaxi));
            }
            return dbTaxi;
        }
    }
}
