using System.Collections.Generic;
using System.Linq;
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

        public DbTaxi(int taxiId, string taxiDriverName, DbLocation currentLocation, string taxiCompanyName)
        {
            TaxiId = taxiId;
            DriverName = taxiDriverName;
            CurrentLocation = currentLocation;
            Rides = new List<DbRide>();
            TaxiCompanyName = taxiCompanyName;
        }

        public DbTaxi(Taxi taxi)
        {
            TaxiId = taxi.TaxiId;
            DriverName = taxi.DriverName;
            CurrentLocation = new DbLocation(taxi.CurrentLocation);
            Rides = new List<DbRide>(taxi.Rides.Select(r => new DbRide(r.RideId, r)));
            TaxiCompanyName = taxi.TaxiCompany.Name;
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
