using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.Client.UIDTO
{
    public class UITaxiDTO
    {
        public int TaxiDriverId { get; }
        public List<UIRideDTO> Rides { get; }
        public int TotalEarnings { get; }

        public UITaxiDTO(Taxi taxi)
        {
            if (taxi == null) throw new ArgumentNullException(nameof(taxi));
            TaxiDriverId = taxi.TaxiDriverId;
            if (taxi.Rides == null) throw new ArgumentNullException(nameof(taxi.Rides));
            Rides = taxi.Rides.Select(r => new UIRideDTO(r)).ToList();
            TotalEarnings = taxi.CalculateTotalEarnings();
        }
    }
}
