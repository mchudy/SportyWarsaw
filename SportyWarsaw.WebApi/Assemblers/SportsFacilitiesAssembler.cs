using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;
using System.Linq;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class SportsFacilitiesAssembler : ISportsFacilitiesAssembler
    {
        public SportsFacilityModel ToSportsFacilityModel(SportsFacility entity)
        {
            return new SportsFacilityModel
            {
                Id = entity.Id,
                Description = entity.Description,
                District = entity.District,
                Number = entity.Number,
                Street = entity.Street,
                Type = entity.Type
            };
        }

        public SportFacilityPlusModel ToSportFacilityPlusModel(SportsFacility entity)
        {
            if (entity == null) return null;
            return new SportFacilityPlusModel()
            {
                Id = entity.Id,
                Number = entity.Number,
                Street = entity.Street,
                District = entity.District,
                Description = entity.Description,
                PhoneNumber = entity.PhoneNumber,
                Position = entity.Position,
                Website = entity.Website,
                Type = entity.Type,
                Emails = entity.Emails.Select(e => e.Email).ToList()
            };
        }

        public SportsFacility ToSportsFacilityFromModel(SportsFacilityModel model)
        {
            return new SportsFacility()
            {
                Id = model.Id,
                Description = model.Description,
                District = model.District,
                Number = model.Number,
                Street = model.Street,
            };
        }

        public SportsFacility ToSportsFacilityFromPlusModel(SportFacilityPlusModel model)
        {
            return new SportsFacility()
            {
                Id = model.Id,
                Description = model.Description,
                District = model.District,
                Number = model.Number,
                PhoneNumber = model.PhoneNumber,
                Street = model.Street,
                Website = model.Website,
                Position = model.Position,
                Type = model.Type
            };
        }
    }
}