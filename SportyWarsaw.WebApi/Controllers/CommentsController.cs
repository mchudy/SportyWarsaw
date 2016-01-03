using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Infrastructure;
using SportyWarsaw.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
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
            var list_comments = context.Meetings.Find(meetingid).Comments.ToList();
            //przerobie na dto
            var outlist = new List<CommentModel>();
            foreach (var item in list_comments)
            {
                outlist.Add(assembler.ToCommentModel(item));
            }
            // to do
            return Ok(outlist);
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
        public IHttpActionResult Post(CommentModel commentFacility)
        {
            if (context.SportsFacilities.Find(commentFacility.Id) == null)
            {
                return BadRequest();
            }
            User currentUser = context.Users.Find(commentFacility.UserId);
            if (currentUser == null)
            {
                return BadRequest();
            }
            context.Comments.Add(new Comment()
            {
                User = currentUser,
                Id = commentFacility.Id,
                Date = DateTime.Now,
                Text = commentFacility.Text
            });
            context.SaveChanges();
            return Ok(commentFacility);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Comments.Find(id);
            if (oldFacility == null)
            {
                return BadRequest();
            }    
            context.Comments.Remove(oldFacility);
            context.SaveChanges();
            return Ok(oldFacility);
        }
    }
}
