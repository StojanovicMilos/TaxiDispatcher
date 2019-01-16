using System.Collections.Generic;

namespace TaxiDispatcher.Abstractions.DbDTO
{
    public class DbTaxiDTO
    {
        public int TaxiDriverId { get; set; }
        public string TaxiDriverName { get; set; }
        public DbLocationDTO CurrentLocation { get; set; }
        public List<DbRideDTO> DbRides { get; set; } = new List<DbRideDTO>();
        public string TaxiCompany { get; set; }
    }
}
