using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/Facilities")]
    public class SportsFacilitiesController : ApiController
    {
        private SportyWarsawContext context = new SportyWarsawContext();

        public IHttpActionResult Get(int id)
        {
            if (id != 1)
            {
                return NotFound();
            }
            var facility = new SportsFacility
            {
                Id = 1,
                AdministrativeUnit = "dfadfadf"
            };
            return Ok(facility);
        }
    }
}
