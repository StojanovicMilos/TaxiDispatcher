using System.Collections.Generic;

namespace TaxiDispatcher.DAL.Entities
{
    public class DbTaxi
    {
        public int TaxiDriverId { get; set; }
        public string TaxiDriverName { get; set; }
        public DbLocation CurrentLocation { get; set; }
        public List<DbRide> DbRides { get; set; } = new List<DbRide>();
        public string TaxiCompany { get; set; }
    }
}
