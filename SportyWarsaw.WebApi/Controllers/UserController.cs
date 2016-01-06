using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

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

        [Route("{username}"), HttpGet]
        public IHttpActionResult Get(string username)
        {
            User user = context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            UserModel dto = assembler.ToUserModel(user);
            return Ok(dto);
        }

        [Route("{username}/Details"), HttpGet]
        public IHttpActionResult GetDetails(string username)
        {
            User user = context.Users
                .Include(e => e.Meetings)
                .FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            UserPlusModel dto = assembler.ToUserPlusModel(user);
            return Ok(dto);
        }

        [Authorize]
        [Route("MyFriends"), HttpGet]
        public IHttpActionResult GetMyFriends()
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myfriends1 = user.FriendshipsInitiated
                                 .Where(f => f.IsConfirmed)
                                 .Select(f => assembler.ToUserModel(f.Friend));
            var myfriends2 = user.FriendshipsRequested
                                 .Where(f => f.IsConfirmed)
                                 .Select(f => assembler.ToUserModel(f.Inviter));
            var myfriends = myfriends1.Concat(myfriends2).ToList();
            return Ok(myfriends);
        }

        [Authorize]
        [Route("MyPendingFriendRequests"), HttpGet]
        public IHttpActionResult GetMyPendingFriendRequests()
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var users = user.FriendshipsRequested
                               .Where(f => f.IsConfirmed == false)
                               .Select(f => assembler.ToUserModel(f.Inviter))
                               .ToList();
            return Ok(users);
        }

        [Authorize]
        [Route("MySentFriendRequests"), HttpGet]
        public IHttpActionResult GetMySentFriendRequests()
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var users = user.FriendshipsInitiated
                               .Where(f => f.IsConfirmed == false)
                               .Select(f => assembler.ToUserModel(f.Friend))
                               .ToList();
            return Ok(users);
        }

        [Authorize]
        [Route("SendFriendRequest/{username}"), HttpPost]
        public IHttpActionResult SendFriendRequest(string username)
        {
            var inviter = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (inviter == null)
            {
                return BadRequest();
            }
            User friend = context.Users.FirstOrDefault(u => u.UserName == username);
            if (friend == null)
            {
                return BadRequest();
            }
            // if already friends
            if (inviter.FriendshipsInitiated.Any(f => f.Friend.UserName == username) ||
                inviter.FriendshipsRequested.Any(f => f.Inviter.UserName == username))
            {
                return BadRequest();
            }
            Friendship friendship = new Friendship()
            {
                CreatedTime = DateTime.Now,
                Inviter = inviter,
                Friend = friend,
                IsConfirmed = false
            };
            inviter.FriendshipsInitiated.Add(friendship);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [Route("AcceptFriendRequest/{username}"), HttpPost]
        public IHttpActionResult AcceptFriendRequest(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var friendship = user.FriendshipsRequested.FirstOrDefault(f => f.Friend.UserName == username);
            if (friendship == null)
            {
                return BadRequest();
            }
            friendship.IsConfirmed = true;
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [Route("RemoveFriend/{username}"), HttpPost]
        public IHttpActionResult RejectFriendRequest(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            Friendship friendship1 = user.FriendshipsRequested.FirstOrDefault(f => f.Inviter.UserName == username);
            if (friendship1 != null)
            {
                user.FriendshipsRequested.Remove(friendship1);
                context.SaveChanges();
                return Ok();
            }
            Friendship friendship2 = user.FriendshipsInitiated.FirstOrDefault(f => f.Friend.UserName == username);
            if (friendship2 == null)
            {
                return BadRequest();
            }
            else
            {
                user.FriendshipsInitiated.Remove(friendship2);
                context.SaveChanges();
                return Ok();
            }
        }

        [Authorize]
        [Route("UpdateProfile"), HttpPost]
        public IHttpActionResult Put(UserPlusModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = context.Users.FirstOrDefault(u => u.UserName == dto.Username);
            if (user == null)
            {
                return BadRequest();
            }
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;
            user.Picture = user.Picture;

            context.Users.AddOrUpdate(user);
            context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult Delete(string username)
        {
            User user = context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return BadRequest();
            }
            context.Users.Remove(user);
            context.SaveChanges();
            return Ok(user);
        }
    }
}
