using System.Collections.Generic;

namespace TaxiDispatcher.DTO
{
    public class TaxiDTO
    {
        public int TaxiDriverId { get; set; }
        public List<RideDTO> Rides { get; set; } = new List<RideDTO>();
        public int TotalEarnings { get; set; }
    }
}
