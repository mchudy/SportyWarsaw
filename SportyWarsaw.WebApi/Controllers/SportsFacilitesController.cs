using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    public class SportsFacilitiesController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly ISportsFacilitiesAssembler assembler;

        public SportsFacilitiesController(SportyWarsawContext context, ISportsFacilitiesAssembler assembler)
        {
            this.context = context;
            this.assembler = assembler;
        }

        public IHttpActionResult Get(int id)
        {
            SportsFacility facility = context.SportsFacilities.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            SportsFacilityModel dto = assembler.ToSportsFacilityModel(facility);
            return Ok(dto);
        }
    }
}
