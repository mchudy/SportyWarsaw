using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SportyWarsaw.Domain.Data
{
    public class SportsFacilitiesDownloader : ISportsFacilitiesDownloader
    {
        private const string baseUri = "https://api.bihapi.pl/wfs/warszawa/";

        private readonly string apiUsername = ConfigurationManager.AppSettings["BihapiUsername"];
        private readonly string apiPassword = ConfigurationManager.AppSettings["BihapiPassword"];

        public async Task<IEnumerable<SportsFacility>> GetSportsFacilities()
        {
            var fields = await GetSportFields();
            var pools = await GetSwimmingPools();
            return fields.Concat(pools);
        }

        private async Task<IEnumerable<SportsFacility>> GetSportFields()
        {
            string json = await GetJson("sportFields");
            var facilities = DeserializeSportsFacilities(json);
            foreach (var facility in facilities)
            {
                facility.Type = SportsFacilityType.SportsField;
            }
            return facilities;
        }

        private async Task<IEnumerable<SportsFacility>> GetSwimmingPools()
        {
            string json = await GetJson("swimmingPools");
            var facilities = DeserializeSportsFacilities(json);
            foreach (var facility in facilities)
            {
                facility.Type = SportsFacilityType.SwimmingPool;
            }
            return facilities;
        }

        private static IEnumerable<SportsFacility> DeserializeSportsFacilities(string json)
        {
            string data = JObject.Parse(json)["data"].ToString();
            var facilities = JsonConvert.DeserializeObject<IEnumerable<SportsFacility>>(data);
            return facilities;
        }

        private async Task<string> GetJson(string requestUri)
        {
            using (var handler = new WebRequestHandler())
            {
                handler.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                using (var client = new HttpClient(handler))
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{apiUsername}:{apiPassword}");
                    var header = new AuthenticationHeaderValue(
                               "Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;
                    client.BaseAddress = new Uri(baseUri);
                    var result = await client.GetAsync(requestUri);
                    result.EnsureSuccessStatusCode();
                    return await result.Content.ReadAsStringAsync();
                }
            }
        }
    }
}