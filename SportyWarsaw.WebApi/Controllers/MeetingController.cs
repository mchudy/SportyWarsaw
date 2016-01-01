using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
namespace SportyWarsaw.WebApi.Controllers
{
    [RoutePrefix("api/Meetings")]
    public class MeetingController : ApiController
    {
        private readonly SportyWarsawContext context;
        private readonly IMeetingAssembler assembler;

        public MeetingController(SportyWarsawContext context, IMeetingAssembler assembler)
        {
            this.context = context;
            this.assembler = assembler;
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
            Meeting facility = context.Meetings.Find(id);
            if (facility == null)
            {
                return NotFound();
            }
            MeetingPlusModel dto = assembler.ToMeetingPlusModel(facility);
            return Ok(dto); // ok, meeting plus
        }

        [Route("{id}/Participants"), HttpGet]
        public IHttpActionResult GetParticipants(int id)
        {
            var lista = context.Meetings.Find(id).Participants;
            if (lista == null || lista.Count == 0)
            {
                return BadRequest();
            }
            return Ok(lista);
        }

        [Route("{id}/MyMeetings"), HttpGet]
        public IHttpActionResult GetMyMeetings(int id)
        {
            // TO DO
            var mymeetings =
                context.Meetings.Where(f => f.Organizer.Id == User.Identity.ToString())
                    .Select(s => assembler.ToMeetingModel(s)).ToList();
            if (mymeetings.Count == 0)
            {
                return BadRequest();
            }
            return Ok(mymeetings);
        }
        [Route("JoinMeeting"), HttpPost]
        public IHttpActionResult JoinMeeting(MeetingPlusModel meetingFacility)
        {
            if (context.Meetings.Find(meetingFacility.Id) == null)
            {
                return BadRequest();
            }
            User newUser = context.Users.Find(User.Identity);
            context.Meetings.Find(meetingFacility.Id).Participants.Add(newUser);
            context.SaveChanges();
            return Ok(meetingFacility);
        }
        [Route("LeaveMeeting"), HttpPost]
        public IHttpActionResult LeaveMeeting(MeetingPlusModel meetingFacility)
        {
            if (context.Meetings.Find(meetingFacility.Id) == null)
            {
                return BadRequest();
            }
            User newUser = context.Users.Find(User.Identity);
            context.Meetings.Find(meetingFacility.Id).Participants.Remove(newUser);
            context.SaveChanges();
            return Ok(meetingFacility);
        }
        [HttpPost]
        public IHttpActionResult Post(MeetingPlusModel meetingFacility) // new meeting
        {
            if (context.Meetings.Find(meetingFacility.Id) == null)
            {
                return BadRequest();
            }
            context.Meetings.Add(new Meeting()
            {
                SportsFacility = meetingFacility.SportsFacility,
                Id = meetingFacility.Id,
                Title = meetingFacility.Title,
                Description = meetingFacility.Description,
                Cost = meetingFacility.Cost,
                EndTime = meetingFacility.EndTime,
                MaxParticipants = meetingFacility.MaxParticipants,
                Organizer = meetingFacility.Organizer,
                SportType = meetingFacility.SportType,
                StartTime = meetingFacility.StartTime,
            }); // uzupelnic
            context.SaveChanges();
            return Ok(meetingFacility);
        }

        [HttpPut]
        public IHttpActionResult Put(MeetingModel meetingFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Meetings.Find(meetingFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            // poprawki danych
            oldFacility.Cost = meetingFacility.Cost;
            oldFacility.Title = meetingFacility.Title;
            oldFacility.EndTime = meetingFacility.EndTime;
            oldFacility.StartTime = meetingFacility.StartTime;
            oldFacility.Id = meetingFacility.Id;
            oldFacility.MaxParticipants = meetingFacility.MaxParticipants;
            oldFacility.SportType = meetingFacility.SportType;
            context.Meetings.AddOrUpdate(oldFacility);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(MeetingModel meetingFacility)
        {
            // jak to zmienic w jedno zapytanie?
            var oldFacility = context.Meetings.Find(meetingFacility.Id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            context.Meetings.Remove(oldFacility);
            context.SaveChanges();
            return Ok(meetingFacility);
        }
    }
}
