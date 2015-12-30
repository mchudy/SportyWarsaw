﻿using SportyWarsaw.Domain;
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

        [Route("Page?size=x&index=x"), HttpGet]
        public IHttpActionResult GetPageSize()
        {
            // to do
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            //var SportFacilitiesList = new List<SportsFacilityModel>();
            // takie rzeczy robi się LINQ
            var sportsFacilitesList = context.SportsFacilities
                                             .Select(s => assembler.ToSportsFacilityModel(s))
                                             .ToList();
            //foreach (var item in context.SportsFacilities)
            //{
            //    // od tego jest assembler
            //    SportFacilitiesList.Add(new SportsFacilityModel()
            //    {
            //        Id = item.Id,
            //        Street = item.Street,
            //        Number = item.Number,
            //        District = item.District,
            //        Description = item.Description
            //    });
            //}
            // to najlepiej sprawdzić na początku
            //if (SportFacilitiesList.Count == 0)
            //{
            //    return NotFound();
            //}
            return Ok(sportsFacilitesList);
        }

        // powinno brać model, nie encję, do przerobienia na encję trzeba dopisać metodę do assemblera
        [HttpPost]
        public IHttpActionResult Post(SportFacilityPlusModel sportsFacility) // zmienic
        {
            if (context.SportsFacilities.Find(sportsFacility.Id)==null)
            {
                return BadRequest();
            }
            context.SportsFacilities.Add(new SportsFacility()
            {
                Id = sportsFacility.Id,
                Description = sportsFacility.Description,
                District = sportsFacility.District,
                Number = sportsFacility.Number,
                PhoneNumber = sportsFacility.PhoneNumber,
                Position = sportsFacility.Position,
                Street = sportsFacility.Street,
                Type = sportsFacility.Type,
                Website = sportsFacility.Website
            });
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
        public IHttpActionResult Delete(SportsFacilityModel modifiedSportsFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.SportsFacilities.Find(modifiedSportsFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            
            context.SportsFacilities.Remove(oldFacility);
            context.SaveChanges();
            return Ok();
        }
        // BEZ SENSU

        // po co te metody? to raczej do MeetingsController, userzy raczej nie są bezpośrednio powiązani
        // z ośrodkami (ew. można dodać jakieś ulubione, ale tego nie ma w bazie póki co)
        //public IHttpActionResult GetAllUsersFromFacility(int id)
        //{
        //    SportsFacility facility = context.SportsFacilities.Find(id);
        //    if (facility == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(facility.Emails); // returns users emails
        //}

        //public IHttpActionResult AddUserToFacility(int facilityId)
        //{
        //    // TO DO
        //    string new_email = context.Users.Find(User.Identity).Email;
        //    if (new_email == null)
        //    {
        //        return NotFound();
        //    }
        //    EmailAddress emailadress = context.EmailAddresses.First(f => f.Email == new_email);
        //    if (emailadress == null)
        //    {
        //        return NotFound();
        //    }
        //    SportsFacility facility = context.SportsFacilities.Find(facilityId);
        //    if (facility == null)
        //    {
        //        return NotFound();
        //    }

        //    context.SportsFacilities.Find(facilityId).Emails.Add(emailadress);
        //    context.SaveChanges();
        //    return Ok(emailadress); // dto??

        //}

        //public IHttpActionResult RemoveUserFromFacility(int facilityId)
        //{
        //    string new_email = context.Users.Find(User.Identity).Email;
        //    if (new_email == null)
        //    {
        //        return NotFound();
        //    }
        //    EmailAddress emailadress = context.EmailAddresses.First(f => f.Email == new_email);
        //    if (emailadress == null)
        //    {
        //        return NotFound();
        //    }
        //    SportsFacility facility = context.SportsFacilities.Find(facilityId);
        //    if (facility == null)
        //    {
        //        return NotFound();
        //    }
        //    context.SportsFacilities.Find(facilityId).Emails.Remove(emailadress);
        //    context.SaveChanges();
        //    return Ok(emailadress); // dto??

        //}
    }
}
