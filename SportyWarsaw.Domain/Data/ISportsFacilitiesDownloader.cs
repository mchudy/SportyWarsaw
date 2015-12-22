using System.Collections.Generic;
using System.Threading.Tasks;
using SportyWarsaw.Domain.Entities;

namespace SportyWarsaw.Domain.Data
{
    public interface ISportsFacilitiesDownloader
    {
        Task<IEnumerable<SportsFacility>> GetSportsFacilities();
    }
}