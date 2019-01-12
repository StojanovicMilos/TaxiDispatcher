using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public class DBTaxi
    {
        public int TaxiDriverId { get; set; }
        public string TaxiDriverName { get; set; }
        public DBLocation CurrentLocation { get; set; }
        public List<DBRide> DBRides { get; set; } = new List<DBRide>();
        public string TaxiCompany { get; set; }
    }
}
