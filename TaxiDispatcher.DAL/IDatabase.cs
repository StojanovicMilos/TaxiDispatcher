using System.Collections.Generic;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.DAL
{
    public interface IDatabase
    {
        void SaveRide(DbRide ride);
        List<DbTaxi> GetAllTaxis();
        DbTaxi GetTaxi(int id);
        void SaveExistingTaxi(DbTaxi dbTaxi);
    }
}
