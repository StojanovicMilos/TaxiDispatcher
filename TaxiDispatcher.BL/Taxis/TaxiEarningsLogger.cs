using System;
using TaxiDispatcher.BL.Interfaces;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiEarningsLogger
    {
        private readonly IDatabase _database;
        private readonly ILogger _logger;

        public TaxiEarningsLogger(IDatabase database, ILogger logger)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogTotalEarningsForTaxiWithId(int taxiId)
        {
            Taxi taxi = _database.GetTaxi(taxiId);

            _logger.WriteLine($"Driver with ID = {taxi.TaxiId} earned today:");
            foreach (var ride in taxi.Rides)
            {
                _logger.WriteLine("Price: " + ride.Price);
            }

            _logger.WriteLine("Total: " + taxi.CalculateTotalEarnings());
        }
    }
}
