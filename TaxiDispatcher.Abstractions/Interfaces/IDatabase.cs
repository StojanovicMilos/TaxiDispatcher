using System.Collections.Generic;
using TaxiDispatcher.Abstractions.DbDTO;

namespace TaxiDispatcher.Abstractions.Interfaces
{
    public interface IDatabase
    {
        void SaveRide(DbRideDTO ride);
        List<DbTaxiDTO> GetAllTaxis();
        DbTaxiDTO GetTaxi(int id);
        void SaveExistingTaxi(DbTaxiDTO dbTaxi);
    }
}
