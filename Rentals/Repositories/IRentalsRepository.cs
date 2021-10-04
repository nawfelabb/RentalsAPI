using Rentals.Models;
using Rentals.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentals.Repositories
{
    public interface IRentalsRepository
    {
        Task<List<Rental>> ReadCSVFile();
    }
}
