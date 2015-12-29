using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Web.Http;
using SportyWarsaw.Domain.Entities;

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

        public IHttpActionResult GetAll() // returns list of all facilities
        {
            var SportFacilitiesList = new List<SportsFacilityModel>();
            foreach (var item in context.SportsFacilities)
            {
                SportFacilitiesList.Add(new SportsFacilityModel()
                {
                    Id = item.Id,
                    Street = item.Street,
                    Number = item.Number,
                    District = item.District,
                    Description = item.Description
                });
            }
            if (SportFacilitiesList.Count == 0)
            {
                return NotFound();
            }
            return Ok(SportFacilitiesList);
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

        public IHttpActionResult GetAllUsersFromFacility(int id) // list of all users in facility
        {
            // TO DO
            return Ok();
        }
        //


    }
}
