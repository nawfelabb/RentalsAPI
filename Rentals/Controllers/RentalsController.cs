using Mapster;
using Microsoft.AspNetCore.Mvc;
using Rentals.Models;
using Rentals.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentals.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("rentals")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _rentalsService;
        public RentalsController(IRentalsService rentalsService)
        {
            _rentalsService = rentalsService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Get a specific rental", typeof(RentalDetail))]
        public async Task<ActionResult> Get(string id)
        {
            return Ok(await _rentalsService.GetAsync(id));
        }

        [HttpGet]
        [SwaggerResponse(200, "Get a list of rentals", typeof(List<RentalInfo>))]
        public async Task<ActionResult> Get([FromQuery] SearchCriterias searchCriterias)
        {
            return Ok(await _rentalsService.GetAsync(searchCriterias.Adapt<Services.Entities.SearchCriterias>()));
        }
    }
}
