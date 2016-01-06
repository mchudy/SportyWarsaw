using Microsoft.AspNet.Identity;
using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/Meetings")]
    public class MeetingsController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly IMeetingAssembler assembler;
        private readonly IUserAssembler userAssembler;

        public MeetingsController(SportyWarsawContext context, IMeetingAssembler assembler,
            IUserAssembler userAssembler)
        {
            this.context = context;
            this.assembler = assembler;
            this.userAssembler = userAssembler;
        }

        public IHttpActionResult Get(int id)
        {
            Meeting facility = context.Meetings.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            MeetingModel dto = assembler.ToMeetingModel(facility);
            return Ok(dto);
        }

        [Route("{id}/Details"), HttpGet]
        public IHttpActionResult GetDetails(int id)
        {
            Meeting facility = context.Meetings.Include(m => m.SportsFacility)
                .FirstOrDefault(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            MeetingPlusModel dto = assembler.ToMeetingPlusModel(facility);
            return Ok(dto);
        }

        [Route("{id}/Participants"), HttpGet]
        public IHttpActionResult GetParticipants(int id)
        {
            var participants = context.Meetings.Find(id).Participants.ToList()
                                      .Select(p => userAssembler.ToUserModel(p));
            return Ok(participants);
        }

        [Authorize]
        [Route("MyMeetings"), HttpGet]
        public IHttpActionResult GetMyMeetings()
        {
            var myUsername = User.Identity.GetUserName();
            var mymeetings =
                context.Meetings.Where(m => m.Participants.Any(u => u.UserName == myUsername))
                                .AsEnumerable()
                                .Select(m => assembler.ToMeetingModel(m));
            return Ok(mymeetings);
        }

        [Authorize]
        [Route("NotMyMeetings"), HttpGet]
        public IHttpActionResult GetNotMyMeetings()
        {
            var myUsername = User.Identity.GetUserName();
            var mymeetings =
                context.Meetings.Where(m => m.Participants.All(u => u.UserName != myUsername))
                                .AsEnumerable()
                                .Select(m => assembler.ToMeetingModel(m));
            return Ok(mymeetings);
        }

        [Authorize]
        [Route("Join/{id}"), HttpPost]
        public IHttpActionResult JoinMeeting(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var meeting = context.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            if (meeting.Participants.Any(u => u.UserName == user.UserName))
            {
                return BadRequest();
            }
            meeting.Participants.Add(user);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [Route("Leave/{id}"), HttpPost]
        public IHttpActionResult LeaveMeeting(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var meeting = context.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            if (meeting.Participants.All(u => u.UserName != user.UserName))
            {
                return BadRequest();
            }
            meeting.Participants.Remove(user);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(AddMeetingModel meetingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SportsFacility facility = context.SportsFacilities.Find(meetingModel.SportsFacilityId);
            if (facility == null)
            {
                return BadRequest();
            }
            User organizer = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var newMeeting = new Meeting
            {
                SportsFacility = facility,
                Title = meetingModel.Title,
                Description = meetingModel.Description,
                Cost = meetingModel.Cost,
                EndTime = meetingModel.EndTime,
                MaxParticipants = meetingModel.MaxParticipants,
                Organizer = organizer,
                SportType = meetingModel.SportType,
                StartTime = meetingModel.StartTime,
            };
            newMeeting.Participants.Add(organizer);
            context.Meetings.Add(newMeeting);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(ChangeMeetingModel meetingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var meeting = context.Meetings.Find(meetingModel.Id);
            if (meeting == null || user.UserName != meeting.Organizer.UserName)
            {
                return Unauthorized();
            }
            meeting.Cost = meetingModel.Cost;
            meeting.Title = meetingModel.Title;
            meeting.EndTime = meetingModel.EndTime;
            meeting.StartTime = meetingModel.StartTime;
            meeting.MaxParticipants = meetingModel.MaxParticipants;
            meeting.SportType = meetingModel.SportType;
            meeting.SportsFacility = context.SportsFacilities.Find(meetingModel.SportsFacilityId);

            context.Meetings.AddOrUpdate(meeting);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var meeting = context.Meetings.Find(id);
            if (meeting == null || user.UserName != meeting.Organizer.UserName)
            {
                return Unauthorized();
            }
            context.Meetings.Remove(meeting);
            context.SaveChanges();
            return Ok(meeting);
        }
    }
}
