using System.Collections.Generic;
using TaxiDispatcher.App.CustomExceptions;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DTO;

namespace TaxiDispatcher.App.Taxis
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

        public Taxi GetTaxiById(int id)
        {
            var dbTaxi = _database.GetTaxi(id);
            return ConvertToToTaxi(dbTaxi);
        }

        private static Taxi ConvertToToTaxi(DbTaxi dbTaxi)
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
                taxis.Add(ConvertToToTaxi(dbTaxi));
            }
            return taxis;
        }
    }
}
