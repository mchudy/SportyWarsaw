using SportyWarsaw.Domain;
using SportyWarsaw.WebApi.Models;
using System.Linq;
using System.Web.Http;
using SportyWarsaw.Domain.Entities;

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

        public IHttpActionResult Add(SportsFacility sportfFacility) // adding new SportFacility
        {
            foreach (var item in context.SportsFacilities)
            {
                if (item.Equals(sportfFacility))
                {
                    return BadRequest("it already exists");
                }
            }
            context.SportsFacilities.Add(sportfFacility);
            context.SaveChanges();
            return Ok(sportfFacility);
        }

        public IHttpActionResult Modify(int id, SportsFacility modifiedSportsFacility) // modify sportfacility
        {
            // jak to zmienic w jedno zapytanie?
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Description = modifiedSportsFacility.Description;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).District = modifiedSportsFacility.District;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Emails = modifiedSportsFacility.Emails;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Number = modifiedSportsFacility.Number;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).PhoneNumber = modifiedSportsFacility.PhoneNumber;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Position = modifiedSportsFacility.Position;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Street = modifiedSportsFacility.Street;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Type = modifiedSportsFacility.Type;
            context.SportsFacilities.FirstOrDefault(f => f.Id == id).Website = modifiedSportsFacility.Website;
            context.SaveChanges();
            return Ok();
        }


    }
}
