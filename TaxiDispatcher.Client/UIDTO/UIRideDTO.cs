using System;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.Client.UIDTO
{
    public class UIRideDTO
    {
        public int Price { get; }

        public UIRideDTO(Ride r)
        {
            if (r == null) throw new ArgumentNullException(nameof(r));
            Price = r.Price;
        }
    }
}