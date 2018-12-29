using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public interface IDatabase
    {
        Ride GetRide(int id);
        List<int> GetRide_Ids();
        void SaveRide(Ride ride);
    }
}
