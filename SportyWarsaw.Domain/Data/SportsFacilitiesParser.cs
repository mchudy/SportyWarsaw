using Newtonsoft.Json.Linq;
using SportyWarsaw.Domain.Entities;
using System.Collections.Generic;

namespace SportyWarsaw.Domain.Data
{
    public class SportsFacilitiesParser
    {
        public IEnumerable<SportsFacility> ParseSportsFacilities(string json)
        {
            var jsonObject = JObject.Parse(json);
            foreach (var obj in jsonObject)
            {
                var sportsFacility = new SportsFacility();


                yield return sportsFacility;
            }
        }
    }
}
