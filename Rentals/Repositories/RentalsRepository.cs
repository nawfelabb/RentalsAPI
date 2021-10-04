using CsvHelper;
using Rentals.Mappers;
using Rentals.Repositories.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Repositories
{
    public class RentalsRepository : IRentalsRepository
    {
        public async Task<List<Rental>> ReadCSVFile()
        {
            using (var reader = new StreamReader(@"C:\Users\LI2CJ9\Downloads\rentals.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RentalMap>();
                var records = csv.GetRecords<Rental>().ToList();

                return records;
            }
        }
    }
}
