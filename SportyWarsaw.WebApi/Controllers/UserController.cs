using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/Users")]
    public class UserController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly IUserAssembler assembler;

        public UserController(SportyWarsawContext context, IUserAssembler assembler)
        {
            this.context = context;
            this.assembler = assembler;
        }

        public IHttpActionResult Get(int id)
        {
            User facility = context.Users.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            UserModel dto = assembler.ToUserModel(facility);
            return Ok(dto);
        }

        [Route("{id}/Details"), HttpGet]
        public IHttpActionResult GetDetails(int id)
        {
            User facility = context.Users.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            UserPlusModel dto = assembler.ToUserPlusModel(facility);
            return Ok(dto); // ok, meeting plus
        }

        [Route("MyFriends"), HttpGet]
        public IHttpActionResult GetMyFriends()
        {
            var mymeetings =
                context.Users.Find(User.Identity.GetUserId()).FriendshipsInitiated;
            if (mymeetings == null) // lol
            {
                return BadRequest();
            }
            return Ok(mymeetings);
        }

        [Route("MyPendingFriendRequests"), HttpGet]
        public IHttpActionResult GetMyPendingFriendRequests()
        {
           // to do
            return Ok();
        }

        [Route("SendFriendRequest"), HttpPost]
        public IHttpActionResult SendFriendRequest(int id) // id usera do ktorego wysylamy
        {
            // to do
            return Ok();

        }

        [Route("AcceptFriendRequest"), HttpPost]
        public IHttpActionResult AcceptFriendRequest(int id) // id usera do ktorego wysylamy
        {
            // to do
            return Ok();

        }

        [Route("RejectFriendRequest"), HttpPost]
        public IHttpActionResult RejectFriendRequest(int id) // id usera do ktorego wysylamy
        {
            // to do
            return Ok();

        }

        [HttpPut]
        public IHttpActionResult Put(UserModel userFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Users.Find(userFacility);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            // poprawki danych
            //oldFacility.Description = modifiedSportsFacility.Description;
            //oldFacility.District = modifiedSportsFacility.District;
            //oldFacility.Number = modifiedSportsFacility.Number;
            //oldFacility.Street = modifiedSportsFacility.Street;

            context.Users.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(UserModel userFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Users.Find(userFacility);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            context.Users.Remove(oldFacility);
            context.SaveChanges();
            return Ok(userFacility);
        }
    }
}
