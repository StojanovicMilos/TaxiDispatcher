using System;
using System.Collections.Generic;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.DAL.Entities
{
    public class DbTaxi
    {
        public int TaxiId { get; }
        public string DriverName { get; set; }
        public DbLocation CurrentLocation { get; set; }
        public List<DbRide> Rides { get; set; }
        public string TaxiCompanyName { get; set; }

        public DbTaxi(int taxiId, string taxiDriverName, DbLocation currentLocation, List<DbRide> rides, string taxiCompanyName)
        {
            TaxiId = taxiId >= 0 ? taxiId : throw new ArgumentException(nameof(taxiId));
            DriverName = taxiDriverName ?? throw new ArgumentNullException(nameof(taxiDriverName));
            CurrentLocation = currentLocation ?? throw new ArgumentNullException(nameof(currentLocation));
            Rides = rides ?? throw new ArgumentNullException(nameof(rides));
            TaxiCompanyName = taxiCompanyName ?? throw new ArgumentNullException(nameof(taxiCompanyName));
        }

        public Taxi ToDomain()
        {
            Taxi taxi = new Taxi(TaxiId, DriverName, CurrentLocation.ToDomain(), new List<Ride>(), TaxiCompanyName);

            foreach (var dbRide in Rides)
            {
                taxi.Rides.Add(dbRide.ToDomain(taxi));
            }

            return taxi;
        }
    }
}
