using System.Collections.Generic;

namespace TaxiDispatcher.DAL
{
    public interface IDatabase
    {
        DbRide GetRide(int id);
        void SaveRide(DbRide ride);
        List<DbTaxi> GetAllTaxis();
        DbTaxi GetTaxi(int id);
        void SaveExistingTaxi(DbTaxi dbTaxi);
    }
}
