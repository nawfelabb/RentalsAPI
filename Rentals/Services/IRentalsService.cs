using Rentals.Services.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentals.Services
{
    public interface IRentalsService
    {
        Task<Models.RentalDetail> GetAsync(string rentalId);
        Task<List<Models.RentalInfo>> GetAsync(SearchCriterias searchCriterias);
    }
}
