using System.Collections.Generic;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Interfaces
{
    public interface IDatabase
    {
        void SaveNewRide(Ride ride);
        List<Taxi> GetAllTaxis();
        Taxi GetTaxi(int id);
        void SaveExistingTaxi(Taxi dbTaxi);
    }
}
