using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Collections.Generic;
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
            //TODO
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var SportFacilitiesList = new List<SportsFacilityModel>();
            // takie rzeczy robi się LINQ
            var sportsFacilitesList = context.SportsFacilities
                                             .Select(s => assembler.ToSportsFacilityModel(s))
                                             .ToList();
            foreach (var item in context.SportsFacilities)
            {
                // od tego jest assembler
                SportFacilitiesList.Add(new SportsFacilityModel()
                {
                    Id = item.Id,
                    Street = item.Street,
                    Number = item.Number,
                    District = item.District,
                    Description = item.Description
                });
            }
            // to najlepiej sprawdzić na początku
            if (SportFacilitiesList.Count == 0)
            {
                return NotFound();
            }
            return Ok(SportFacilitiesList);
        }

        // powinno brać model, nie encję, do przerobienia na encję trzeba dopisać metodę do assemblera
        [HttpPost]
        public IHttpActionResult Post(SportsFacility sportsFacility)
        {
            // iterowanie po całej tabeli, żeby coś znaleźć, serio? Find(id)
            foreach (var item in context.SportsFacilities)
            {
                if (item.Equals(sportsFacility))
                {
                    return BadRequest();
                }
            }
            context.SportsFacilities.Add(sportsFacility);
            context.SaveChanges();
            return Ok(sportsFacility);
        }

        // powinno brać model nie encję, id nie jest potrzebne
        [HttpPut]
        public IHttpActionResult Put(int id, SportsFacility modifiedSportsFacility)
        {
            // jak to zmienic w jedno zapytanie?
            // var oldFacility = context.SportsFacilities.Find(modifiedSportsFacility.Id)
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

        // po co te metody? to raczej do MeetingsController, userzy raczej nie są bezpośrednio powiązani
        // z ośrodkami (ew. można dodać jakieś ulubione, ale tego nie ma w bazie póki co)
        public IHttpActionResult GetAllUsersFromFacility(int id)
        {
            // TO DO
            return Ok();
        }

        public IHttpActionResult AddUserToFacility(int facilityId)
        {
            // TO DO
            return Ok();
        }

        public IHttpActionResult RemoveUserFromFacility(int facilityId)
        {
            return Ok();
        }
    }
}
