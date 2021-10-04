namespace Rentals.Models
{
    public class RentalDetail
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public int Nb_beds { get; set; }
        public int Nb_baths { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
    }
}
