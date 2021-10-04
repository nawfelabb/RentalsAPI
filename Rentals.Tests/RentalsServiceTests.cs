using Moq;
using Rentals.Repositories;
using Rentals.Repositories.Entities;
using Rentals.Services;
using Rentals.Tests.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rentals.Tests
{
    public class RentalsServiceTests : BaseTestClass<RentalsService>
    {
        private readonly Mock<IRentalsRepository> _rentalsRepository;

        public RentalsServiceTests()
        {
            _rentalsRepository = GetMock<IRentalsRepository>();
        }

        [Fact]
        public async Task Should_Return_One_Rental()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", City = "Montreal", Price = 50},
                new Rental() { Id = "56789", City = "Quebec", Price = 30},
            } );

            //Act
            var result = await Sut.GetAsync("1234");

            //Assert
            Assert.Equal("Montreal", result.City);
            Assert.Equal(50, result.Price);
        }

        [Fact]
        public async Task Should_Return_All_Rentals()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", Nb_beds = 3, Price = 50},
                new Rental() { Id = "56789", Nb_beds = 2, Price = 30},
            });

            //Act
            var result = await Sut.GetAsync(new Services.Entities.SearchCriterias());

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Should_Return_Rentals_With_Min_Nb_Beds()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", Nb_beds = 3, Price = 50},
                new Rental() { Id = "56789", Nb_beds = 2, Price = 30},
            });

            //Act
            var result = await Sut.GetAsync(new Services.Entities.SearchCriterias() { MinNbBeds = 3});

            //Assert
            Assert.Single(result);
            Assert.Equal("1234", result.Single().Id);
        }

        [Fact]
        public async Task Should_Return_Rentals_With_Max_Price()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", Nb_beds = 3, Price = 50},
                new Rental() { Id = "56789", Nb_beds = 2, Price = 30},
            });

            //Act
            var result = await Sut.GetAsync(new Services.Entities.SearchCriterias() { MaxPrice = 45});

            //Assert
            Assert.Single(result);
            Assert.Equal("56789", result.Single().Id);
        }

        [Fact]
        public async Task Should_Return_Rentals_With_Min_Price()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", Nb_beds = 3, Price = 50},
                new Rental() { Id = "56789", Nb_beds = 2, Price = 30},
                new Rental() { Id = "020210710", Nb_beds = 2, Price = 35},
            });

            //Act
            var result = await Sut.GetAsync(new Services.Entities.SearchCriterias() { MinPrice = 35  });

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Should_Return_Rentals_With_PostalCode()
        {
            // Arrange
            _rentalsRepository.Setup(x => x.ReadCSVFile()).ReturnsAsync(new List<Rental>() {
                new Rental() { Id = "1234", Nb_beds = 3, Price = 50, PostalCode = "G1X5G2"},
                new Rental() { Id = "56789", Nb_beds = 2, Price = 30, PostalCode = "G1F3G2"},
                new Rental() { Id = "020210710", Nb_beds = 2, Price = 35, PostalCode = "B1X5G8"},
            });

            //Act
            var result = await Sut.GetAsync(new Services.Entities.SearchCriterias() { PostalCode = "G1X5G2" });

            //Assert
            Assert.Single(result);
            Assert.Equal("G1X5G2", result.Single().PostalCode);
        }
    }
}
