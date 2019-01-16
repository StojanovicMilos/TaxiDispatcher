using System.Collections.Generic;

namespace TaxiDispatcher.Abstractions.UIDTO
{
    public class UITaxiDTO
    {
        public int TaxiDriverId { get; set; }
        public List<UIRideDTO> Rides { get; set; } = new List<UIRideDTO>();
        public int TotalEarnings { get; set; }
    }
}
