using SportyWarsaw.Domain;
using SportyWarsaw.WebApi.Models;
using System.Linq;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    public class SportsFacilitiesController : ApiController
    {
        private readonly SportyWarsawContext context;

        public SportsFacilitiesController(SportyWarsawContext context)
        {
            this.context = context;
        }

        public IHttpActionResult Get(int id)
        {
            var models = context.SportsFacilities.Where(f => f.Id == id).Select(f => new SportsFacilityModel
            {
                Id = f.Id,
                Description = f.Description,
                District = f.District,
                Number = f.Number,
                Street = f.Street
            });
            return Ok(models);
        }
    }
}
