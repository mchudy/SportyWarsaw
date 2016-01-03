using System;
using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
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

        //[Route("Page?size=x&index=x"), HttpGet]
        [Route("Page"), HttpGet]
        public IHttpActionResult GetPageSize(int size, int index)
        {
            // to do
            var total_count = context.SportsFacilities.Count();
            var total_pages = Math.Ceiling((double)total_count/size);

            var query = context.SportsFacilities.ToList();
            if (query.Count == 0)
            {
                return NotFound();
            }

            query = query.OrderBy(f => f.Id).ToList();

            var outlist = query.Skip((index - 1) * size)
                            .Take(size)
                            .ToList();

            // jeszcze jakies info o max ilosci stron ??
            return Ok(outlist);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var sportsFacilitesList = context.SportsFacilities
                                             .Select(s => assembler.ToSportsFacilityModel(s))
                                             .ToList();
            return Ok(sportsFacilitesList);
        }

        // powinno brać model, nie encję, do przerobienia na encję trzeba dopisać metodę do assemblera OK
        [HttpPost]
        public IHttpActionResult Post(SportFacilityPlusModel sportsFacility)
        {
            if (context.SportsFacilities.Find(sportsFacility.Id)==null)
            {
                return BadRequest();
            }
            context.SportsFacilities.Add(assembler.ToSportsFacilityFromPlusModel(sportsFacility));
            context.SaveChanges();
            return Ok(sportsFacility);
        }
        [HttpPut]
        public IHttpActionResult Put(SportsFacilityModel modifiedSportsFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.SportsFacilities.Find(modifiedSportsFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            oldFacility.Description = modifiedSportsFacility.Description;
            oldFacility.District = modifiedSportsFacility.District;
            oldFacility.Number = modifiedSportsFacility.Number;
            oldFacility.Street = modifiedSportsFacility.Street;

            context.SportsFacilities.AddOrUpdate(oldFacility);        
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.SportsFacilities.Find(id);
            if (oldFacility == null)
            {
                return BadRequest();
            }       
            context.SportsFacilities.Remove(oldFacility);
            context.SaveChanges();
            return Ok(oldFacility);
        }
    }
}
