using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.DAL.Entities
{
    public class DbTaxi
    {
        public int TaxiDriverId { get; }
        public string TaxiDriverName { get; set; }
        public DbLocation CurrentLocation { get; set; }
        public List<DbRide> DbRides { get; set; }
        public string TaxiCompany { get; set; }

        public DbTaxi(int taxiDriverId, string taxiDriverName, DbLocation currentLocation, string taxiCompany)
        {
            TaxiDriverId = taxiDriverId;
            TaxiDriverName = taxiDriverName;
            CurrentLocation = currentLocation;
            DbRides = new List<DbRide>();
            TaxiCompany = taxiCompany;
        }

        public DbTaxi(Taxi taxi)
        {
            TaxiDriverId = taxi.TaxiDriverId;
            TaxiDriverName = taxi.TaxiDriverName;
            CurrentLocation = new DbLocation(taxi.CurrentLocation);
            DbRides = new List<DbRide>(taxi.Rides.Select(r => new DbRide(r.RideId, r, this)));
            TaxiCompany = taxi.TaxiCompany;
        }

        public Taxi ToDomain()
        {
            Taxi taxi;
            switch (TaxiCompany)
            {
                case "Alfa":
                    taxi = new AlfaTaxi(TaxiDriverId, TaxiDriverName, CurrentLocation.ToDomain(), new List<Ride>());
                    break;
                case "Gold":
                    taxi = new GoldTaxi(TaxiDriverId, TaxiDriverName, CurrentLocation.ToDomain(), new List<Ride>());
                    break;
                case "Naxi":
                    taxi = new NaxiTaxi(TaxiDriverId, TaxiDriverName, CurrentLocation.ToDomain(), new List<Ride>());
                    break;
                default:
                    throw new InvalidTaxiCompanyException(TaxiCompany);
            }

            foreach (var dbRide in DbRides)
            {
                taxi.Rides.Add(dbRide.ToDomain(taxi));
            }

            return taxi;
        }
    }
}
