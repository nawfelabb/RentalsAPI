
namespace Rentals.Models
{
    public class SearchCriterias
    {
        public int? MinNbBeds { get; set; }
        public string PostalCode { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
