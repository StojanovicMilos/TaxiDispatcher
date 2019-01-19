using System;
using TaxiDispatcher.BL.Interfaces;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiContext
    {
        private readonly IDatabase _database;

        public TaxiContext(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public Taxi GetTaxiWithEarningsById(int id) => _database.GetTaxi(id);
    }
}
