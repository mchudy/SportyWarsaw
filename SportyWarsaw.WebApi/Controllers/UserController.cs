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
            // poprawic !!
            var myfriends1 =
                context.Users.Find(User.Identity.GetUserId()).FriendshipsInitiated.Where(f => f.IsConfirmed).ToList();
            var myfriends2 = context.Users.Find(User.Identity.GetUserId())
                .FriendshipsRequested.Where(f => f.IsConfirmed).ToList();
            
            var myfriends = new List<Friendship>();
            foreach (var item in myfriends1)
            {
                myfriends.Add(item);
            }
            foreach (var item in myfriends2)
            {
                myfriends.Add(item);
            }
            if (myfriends.Count == 0)
            {
                return NotFound();
            }
            return Ok(myfriends);
        }

        [Route("MyPendingFriendRequests"), HttpGet]
        public IHttpActionResult GetMyPendingFriendRequests()
        {
            var friendrequests =
                context.Users.Find(User.Identity.GetUserId()).FriendshipsRequested.Where(f => f.IsConfirmed==false).ToList();
            if (friendrequests.Count == 0)
            {
                return NotFound();
            }
            return Ok(friendrequests);
        }

        [Route("MySentFriendRequests"), HttpGet]
        public IHttpActionResult GetMySentFriendRequests()
        {
            var sentfriendrequests =
                context.Users.Find(User.Identity.GetUserId()).FriendshipsInitiated.Where(f => f.IsConfirmed == false).ToList();
            if (sentfriendrequests.Count == 0)
            {
                return NotFound();
            }
            return Ok(sentfriendrequests);
        }

        [Route("SendFriendRequest/{id}"), HttpPost]
        public IHttpActionResult SendFriendRequest(int id) // id usera do ktorego wysylamy
        {
            User inviter = context.Users.Find(User.Identity.GetUserId());
            if (inviter == null)
            {
                return BadRequest();
            }
            User friend = context.Users.Find(id);
            if (friend == null)
            {
                return BadRequest();
            }
            Friendship nowa = new Friendship()
            {
                CreatedTime = DateTime.Now,
                Inviter = inviter,
                InviterId = inviter.Id,
                Friend = friend,
                FriendId = friend.Id,
                IsConfirmed = false
            };
            context.Users.Find(id).FriendshipsRequested.Add(nowa);
            context.Users.Find(User.Identity.GetUserId()).FriendshipsInitiated.Add(nowa);
            return Ok();
        }

        [Route("AcceptFriendRequest/{id}"), HttpPost]
        public IHttpActionResult AcceptFriendRequest(int id) // id usera od ktorego dostajemy
        {
            context.Users.Find(User.Identity.GetUserId()).FriendshipsRequested.First(f => f.FriendId == id.ToString()).IsConfirmed
                = true;
            context.Users.Find(id).FriendshipsInitiated.First(f => f.FriendId == User.Identity.GetUserId()).IsConfirmed
                = true;
            return Ok();
        }

        [Route("RejectFriendRequest/{id}"), HttpPost]
        public IHttpActionResult RejectFriendRequest(int id) // id usera do ktorego wysylamy
        {
            Friendship to_delete1 = context.Users.Find(User.Identity.GetUserId()).FriendshipsRequested.First(f => f.FriendId == id.ToString());
            if (to_delete1 == null)
            {
                return NotFound();
            }
            Friendship to_delete2 =
                context.Users.Find(id).FriendshipsInitiated.First(f => f.FriendId == User.Identity.GetUserId());
            if (to_delete2 == null)
            {
                return NotFound();
            }

            context.Users.Find(id).FriendshipsInitiated.Remove(to_delete2);
            context.Users.Find(User.Identity.GetUserId()).FriendshipsRequested.Remove(to_delete1);
            context.SaveChanges();
            return Ok();

        }

        [HttpPut]
        public IHttpActionResult Put(UserModel userFacility)
        {
            var oldFacility = context.Users.Find(userFacility);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            // poprawki danych
            oldFacility.FirstName = userFacility.FirstName;
            oldFacility.LastName = userFacility.LastName;
            context.Users.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var oldFacility = context.Users.Find(id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            context.Users.Remove(oldFacility);
            context.SaveChanges();
            return Ok(oldFacility);
        }
    }
}
