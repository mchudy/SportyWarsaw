using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public interface ISportsFacilitiesAssembler
    {
        SportsFacilityModel ToSportsFacilityModel(SportsFacility entity);
        SportFacilityPlusModel ToSportFacilityPlusModel(SportsFacility entity);
    }
}