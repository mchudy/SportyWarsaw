using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly ICommentAssembler assembler;

        public CommentsController(SportyWarsawContext context, ICommentAssembler assembler)
        {
            this.context = context;
            this.assembler = assembler;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Comment facility = context.Comments.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            CommentModel dto = assembler.ToCommentModel(facility);
            return Ok(dto);
        }

        [Route("meeting/{meetingId}"), HttpGet]
        public IHttpActionResult GetAll(int meetingId)
        {
            var comments = context.Meetings.Find(meetingId).Comments
                                  .OrderByDescending(c => c.Date)
                                  .AsEnumerable()
                                  .Select(c => assembler.ToCommentModel(c));
            return Ok(comments);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(CommentModel commentFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldFacility = context.Comments.Find(commentFacility.Id);
            if (oldFacility == null)
            {
                return NotFound();
            }
            if (oldFacility.User.UserName != User.Identity.Name)
            {
                return Unauthorized();
            }
            oldFacility.Text = commentFacility.Text;

            context.Comments.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok(commentFacility);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(AddCommentModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var meeting = context.Meetings.Find(commentModel.MeetingId);
            if (meeting == null)
            {
                return BadRequest();
            }
            var currentUser = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            context.Comments.Add(new Comment()
            {
                User = currentUser,
                Date = DateTime.Now,
                Text = commentModel.Text,
                Meeting = meeting
            });
            context.SaveChanges();
            return Ok(commentModel);
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var comment = context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (comment.User.UserName != User.Identity.Name)
            {
                return Unauthorized();
            }
            context.Comments.Remove(comment);
            context.SaveChanges();
            return Ok(comment);
        }
    }
}
