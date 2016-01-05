using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/sportsFacilities")]
    public class SportsFacilitiesController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly ISportsFacilitiesAssembler assembler;

        public SportsFacilitiesController(SportyWarsawContext context, ISportsFacilitiesAssembler assembler)
        {
            this.context = context;
            this.assembler = assembler;
        }

        [HttpGet]
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

        [Route("{id}/Details"), HttpGet]
        public IHttpActionResult GetDetails(int id)
        {
            SportsFacility facility = context.SportsFacilities.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            SportFacilityPlusModel dto = assembler.ToSportFacilityPlusModel(facility);
            return Ok(dto);
        }

        [Route("Page"), HttpGet]
        public IHttpActionResult GetPageSize(int size, int index, string nameFilter = "")
        {
            IQueryable<SportsFacility> query = context.SportsFacilities
                .OrderBy(f => f.Id);
            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(f => f.Description.StartsWith(nameFilter));
            }
            query = query.Skip((index - 1) * size)
                         .Take(size);
            var outlist = query.AsEnumerable()
                               .Select(f => assembler.ToSportsFacilityModel(f))
                               .ToList();
            return Ok(outlist);
        }

        [Route("All"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var sportsFacilitesList = context.SportsFacilities
                                             .AsEnumerable()
                                             .Select(s => assembler.ToSportsFacilityModel(s))
                                             .ToList();
            return Ok(sportsFacilitesList);
        }

        [HttpPost]
        public IHttpActionResult Post(SportFacilityPlusModel sportsFacility)
        {
            if (context.SportsFacilities.Find(sportsFacility.Id) != null)
            {
                return BadRequest();
            }
            context.SportsFacilities.Add(assembler.ToSportsFacilityFromPlusModel(sportsFacility));
            context.SaveChanges();
            return Ok(sportsFacility);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult Put(SportFacilityPlusModel modifiedSportsFacility)
        {
            var oldFacility = context.SportsFacilities.Find(modifiedSportsFacility.Id);
            if (oldFacility == null)
            {
                return NotFound();
            }
            oldFacility.Description = modifiedSportsFacility.Description;
            oldFacility.District = modifiedSportsFacility.District;
            oldFacility.Number = modifiedSportsFacility.Number;
            oldFacility.Street = modifiedSportsFacility.Street;
            oldFacility.PhoneNumber = modifiedSportsFacility.PhoneNumber;
            oldFacility.Website = modifiedSportsFacility.Website;
            oldFacility.Position = modifiedSportsFacility.Position;
            //TODO: handle email addresses
            context.SportsFacilities.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var oldFacility = context.SportsFacilities.Find(id);
            if (oldFacility == null)
            {
                return NotFound();
            }
            context.SportsFacilities.Remove(oldFacility);
            context.SaveChanges();
            return Ok();
        }
    }
}
