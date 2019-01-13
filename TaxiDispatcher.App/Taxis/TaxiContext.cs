using System.Collections.Generic;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DAL.Entities;
using TaxiDispatcher.DTO;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiContext
    {
        private readonly IDatabase _database;

        public TaxiContext() : this(InMemoryDataBase.Instance) { }

        public TaxiContext(IDatabase database)
        {
            _database = database;
        }

        public TaxiDTO GetTaxiWithEarningsById(int id)
        {
            Taxi taxi = GetTaxiById(id);
            int totalEarnings = taxi.CalculateTotalEarnings();
            List<RideDTO> rides = new List<RideDTO>();
            foreach (var ride in taxi.Rides)
            {
                rides.Add(new RideDTO { Price = ride.Price });
            }

            return new TaxiDTO
            {
                TaxiDriverId = id,
                TotalEarnings = totalEarnings,
                Rides = rides
            };
        }

        private Taxi GetTaxiById(int id)
        {
            var dbTaxi = _database.GetTaxi(id);
            return ConvertToTaxi(dbTaxi);
        }

        private static Taxi ConvertToTaxi(DbTaxi dbTaxi)
        {
            switch (dbTaxi.TaxiCompany)
            {
                case "Alfa":
                    return new AlfaTaxi(dbTaxi);
                case "Gold":
                    return new GoldTaxi(dbTaxi);
                case "Naxi":
                    return new NaxiTaxi(dbTaxi);
            }
            throw new InvalidTaxiCompanyException(dbTaxi.TaxiCompany);
        }

        public List<Taxi> GetAllTaxis()
        {
            var dbTaxis = _database.GetAllTaxis();
            List<Taxi> taxis = new List<Taxi>();
            foreach (var dbTaxi in dbTaxis)
            {
                taxis.Add(ConvertToTaxi(dbTaxi));
            }
            return taxis;
        }
    }
}
