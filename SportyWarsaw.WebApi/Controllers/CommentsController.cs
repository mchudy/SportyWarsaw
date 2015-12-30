using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Infrastructure;
using SportyWarsaw.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;
using SportyWarsaw.Domain;
using SportyWarsaw.WebApi.Assemblers;

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
        [Route("{meetingid}"), HttpGet]
        public IHttpActionResult GetAll(int meetingid)
        {
            var list_comments = context.Meetings.Find(meetingid).Comments;
            // to do
            return Ok(list_comments);
        }

        [HttpPut]
        public IHttpActionResult Put(CommentModel commentFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Comments.Find(commentFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            oldFacility.Date = commentFacility.Date;
            oldFacility.Id = commentFacility.Id;
            oldFacility.Text = commentFacility.Text;

            context.Comments.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok(commentFacility);
        }

        [HttpPost]
        public IHttpActionResult Post(CommentPlusModel commentFacility)
        {
            if (context.SportsFacilities.Find(commentFacility.Id) == null)
            {
                return BadRequest();
            }
            context.Comments.Add(new Comment()
            {
                User = commentFacility.User,
                Id = commentFacility.Id,
                Date = commentFacility.Date,
                Text = commentFacility.Text
            });
            context.SaveChanges();
            return Ok(commentFacility);
        }

        [HttpDelete]
        public IHttpActionResult Delete(CommentModel commentFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Comments.Find(commentFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }    
            context.Comments.Remove(oldFacility);
            context.SaveChanges();
            return Ok(commentFacility);
        }
    }
}
