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
    }
}