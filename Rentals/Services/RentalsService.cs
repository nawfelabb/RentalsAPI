using Mapster;
using Rentals.Models;
using Rentals.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiErrorHandling.Exceptions;
using System.Linq;

namespace Rentals.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalsRepository _rentalsRepository;

        public RentalsService(IRentalsRepository rentalsRepository)
        {
            _rentalsRepository = rentalsRepository;
        }

        public async Task<RentalDetail> GetAsync(string rentalId)
        {
            var rentalDetail = (await _rentalsRepository.ReadCSVFile()).SingleOrDefault(x => x.Id == rentalId);
            if (rentalDetail != null)
                return rentalDetail.Adapt<RentalDetail>();

            throw new ResourceNotFoundException($"No resource was found with id '{rentalId}'.");
        }

        public async Task<List<RentalInfo>> GetAsync(Entities.SearchCriterias searchCriterias)
        {
            var rentals = await _rentalsRepository.ReadCSVFile();
            if (searchCriterias.MinNbBeds != null)
                rentals = rentals.Where(x => x.Nb_beds >= searchCriterias.MinNbBeds).ToList();
            if(searchCriterias.MinPrice != null)
                rentals = rentals.Where(x => x.Price >= searchCriterias.MinPrice).ToList();
            if (searchCriterias.MaxPrice != null)
                rentals = rentals.Where(x => x.Price <= searchCriterias.MaxPrice).ToList();
            if (!string.IsNullOrEmpty(searchCriterias.PostalCode))
                rentals = PostalCodeCheck(searchCriterias.PostalCode, rentals);

            return rentals.Adapt<List<RentalInfo>>();
        }

        private List<Repositories.Entities.Rental> PostalCodeCheck(string postalCode, List<Repositories.Entities.Rental> rentals)
        {
            var rentalsInfo = new List<Repositories.Entities.Rental>();
            foreach (var rental in rentals)
            {
                string c1, c2, c3, c4, c5, c6;
                string pc1, pc2, pc3, pc4, pc5, pc6;

                if (postalCode.Length == 6)
                {
                    c1 = postalCode.Substring(0, 1);
                    c2 = postalCode.Substring(1, 1);
                    c3 = postalCode.Substring(2, 1);
                    c4 = postalCode.Substring(3, 1);
                    c5 = postalCode.Substring(4, 1);
                    c6 = postalCode.Substring(5, 1);

                    pc1 = rental.PostalCode.Substring(0, 1);
                    pc2 = rental.PostalCode.Substring(1, 1);
                    pc3 = rental.PostalCode.Substring(2, 1);
                    pc4 = rental.PostalCode.Substring(3, 1);
                    pc5 = rental.PostalCode.Substring(4, 1);
                    pc6 = rental.PostalCode.Substring(5, 1);

                    if (CompareCharacter(c1, false, pc1) && CompareCharacter(c2, true, pc2) && CompareCharacter(c3, false, pc3) &&
                        CompareCharacter(c4, true, pc4) && CompareCharacter(c5, false, pc5) && CompareCharacter(c6, true, pc6))
                    {
                        rentalsInfo.Add(rental);
                    }
                }

                else
                {
                    throw new InvalidStateException("the postal code must contain 6 characters");
                }
            }

            return rentalsInfo;
        }

        private static bool CompareCharacter(string searchCriteriasPostalCode, bool CheckNumber, string postalCode)
        {
            int sortie;
            bool condition;

            if (CheckNumber)
                condition = int.TryParse(searchCriteriasPostalCode, out sortie);
            else
                condition = !int.TryParse(searchCriteriasPostalCode, out sortie);

            if (string.IsNullOrWhiteSpace(searchCriteriasPostalCode))
                return true;

            if (condition)
            {
                if(searchCriteriasPostalCode.Equals(postalCode))
                    return true;
            }
            else
                throw new InvalidStateException("It is not a valid postal code");

            return false;
        }
    }
}
