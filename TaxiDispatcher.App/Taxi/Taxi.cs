using System.Linq;
using System.Collections.Generic;
using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public abstract class Taxi
    {
        public int TaxiDriverId { get; set; }
        public string TaxiDriverName { get; set; }
        public Location CurrentLocation { get; set; }
        public List<Ride> Rides { get; set; } = new List<Ride>();
        protected abstract int PricePerDistance { get; }

        protected Taxi(DBTaxi dbTaxi)
        {
            TaxiDriverId = dbTaxi.TaxiDriverId;
            TaxiDriverName = dbTaxi.TaxiDriverName;
            CurrentLocation = new Location(dbTaxi.CurrentLocation);
            foreach (var dbRide in dbTaxi.DBRides)
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

        public abstract DBTaxi ToDBTaxi();

        protected DBTaxi ToDBTaxiBase()
        {
            var dbTaxi = new DBTaxi
            {
                TaxiDriverId = TaxiDriverId,
                TaxiDriverName = TaxiDriverName,
                CurrentLocation = CurrentLocation.ToDBLocation()
            };
            foreach (var ride in Rides)
            {
                dbTaxi.DBRides.Add(ride.ToDBRide(dbTaxi));
            }
            return dbTaxi;
        }
    }
}
