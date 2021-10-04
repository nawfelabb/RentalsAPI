namespace Rentals.Repositories.Entities
{
    public class Rental
    {
        public string Id { get; set; }
        public string PostalCode { get; set; }
        public int Nb_beds { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public int Nb_baths { get; set; }
        public int Rating { get; set; }
    }
}
