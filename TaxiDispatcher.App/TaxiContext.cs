using System.Collections.Generic;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DTO;

namespace TaxiDispatcher.App
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
            Taxi taxi = _database.GetTaxi(id);
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
    }
}
