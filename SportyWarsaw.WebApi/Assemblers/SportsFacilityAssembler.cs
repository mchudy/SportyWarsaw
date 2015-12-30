using System.Data.Entity.Core.Metadata.Edm;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

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
                Street = entity.Street
            };
        }
        public SportFacilityPlusModel ToSportFacilityPlusModel(SportsFacility entity)
        {
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
                Type = entity.Type
            };
        }
    }
}