using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Assemblers;
using SportyWarsaw.WebApi.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using SportyWarsaw.Domain.Enums;

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

        [Route("MyMeetings"), HttpGet]
        public IHttpActionResult GetMyMeetings(int id)
        {
            var mymeetings =
                context.Meetings.Where(f => f.Organizer.Id == User.Identity.ToString())
                    .Select(s => assembler.ToMeetingModel(s)).ToList();
            if (mymeetings.Count == 0)
            {
                return BadRequest();
            }
            return Ok(mymeetings);
        }
        [Route("{id}/JoinMeeting"), HttpPost]
        public IHttpActionResult JoinMeeting(int id)
        {
            if (context.Meetings.Find(id) == null)
            {
                return BadRequest();
            }
            User newUser = context.Users.Find(User.Identity);
            if (newUser == null)
            {
                return BadRequest();
            }
            context.Meetings.Find(id).Participants.Add(newUser);
            context.SaveChanges();
            return Ok(id);
        }
        [Route("{id}/LeaveMeeting"), HttpPost]
        public IHttpActionResult LeaveMeeting(int id)
        {
            if (context.Meetings.Find(id) == null)
            {
                return BadRequest();
            }
            User newUser = context.Users.Find(User.Identity);
            context.Meetings.Find(id).Participants.Remove(newUser);
            context.SaveChanges();
            return Ok(id);
        }
        [HttpPost]
        public IHttpActionResult Post(MeetingPlusModel meetingFacility) // new meeting
        {
            SportsFacilitiesAssembler assembler = new SportsFacilitiesAssembler();
            if (context.Meetings.Find(meetingFacility.Id) == null)
            {
                return BadRequest();
            }
            SportsFacility facility = context.SportsFacilities.Find(meetingFacility.SportsFacility.Id);
            if (facility == null)
            {
                return BadRequest();
            }
            context.Meetings.Add(new Meeting()
            {
                SportsFacility = facility,
                Id = meetingFacility.Id,
                Title = meetingFacility.Title,
                Description = meetingFacility.Description,
                Cost = meetingFacility.Cost,
                EndTime = meetingFacility.EndTime,
                MaxParticipants = meetingFacility.MaxParticipants,
                Organizer = context.Meetings.Find(meetingFacility.Id).Organizer,
                SportType = meetingFacility.SportType,
                StartTime = meetingFacility.StartTime,
            });
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
        public IHttpActionResult Delete(int id)
        {
            var oldFacility = context.Meetings.Find(id);
            if (oldFacility == null)
            {
                return BadRequest();
            }
            context.Meetings.Remove(oldFacility);
            context.SaveChanges();
            return Ok(oldFacility);
        }
    }
}
