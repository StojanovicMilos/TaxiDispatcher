using System;
using System.Collections.Generic;
using TaxiDispatcher.Abstractions.DbDTO;
using TaxiDispatcher.Abstractions.Interfaces;
using TaxiDispatcher.Abstractions.UIDTO;
using TaxiDispatcher.BL.CustomExceptions;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiContext
    {
        private readonly IDatabase _database;

        public TaxiContext(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public UITaxiDTO GetTaxiWithEarningsById(int id)
        {
            Taxi taxi = GetTaxiById(id);
            int totalEarnings = taxi.CalculateTotalEarnings();
            List<UIRideDTO> rides = new List<UIRideDTO>();
            foreach (var ride in taxi.Rides)
            {
                rides.Add(new UIRideDTO { Price = ride.Price });
            }

            return new UITaxiDTO
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

        private static Taxi ConvertToTaxi(DbTaxiDTO dbTaxi)
        {
            if (dbTaxi == null) throw new ArgumentNullException(nameof(dbTaxi));
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
