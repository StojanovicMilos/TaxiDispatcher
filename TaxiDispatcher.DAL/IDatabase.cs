using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public interface IDatabase
    {
        DBRide GetRide(int id);
        void SaveRide(DBRide ride);
        List<DBTaxi> GetAllTaxis();
        DBTaxi GetTaxi(int id);
        void SaveExistingTaxi(DBTaxi dbTaxi);
    }
}
