using CsvHelper.Configuration;
using Rentals.Repositories.Entities;

namespace Rentals.Mappers
{
    public class RentalMap : ClassMap<Rental>
    {
        public RentalMap()
        {
            Map(x => x.Id).Name("id");
            Map(x => x.PostalCode).Name("postalcode");
            Map(x => x.Nb_beds).Name("nb_beds");
            Map(x => x.Price).Name("price");
            Map(x => x.Description).Name("description");
            Map(x => x.City).Name("city");
            Map(x => x.Owner).Name("owner");
            Map(x => x.Nb_baths).Name("nb_baths");
            Map(x => x.Rating).Name("rating");
        }
    }
}
