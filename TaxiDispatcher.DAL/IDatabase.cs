using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public interface IDatabase
    {
        Ride GetRide(int id);
        void SaveRide(Ride ride);
        Taxi[] GetAllTaxis();
        Taxi GetTaxi(int id);
    }
}
