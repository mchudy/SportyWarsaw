using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportyWarsaw.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SportyWarsaw.Domain.Data
{
    public class DataDownloader
    {
        private const string baseUri = "https://api.bihapi.pl/wfs/warszawa/";

        private readonly string apiUsername = ConfigurationManager.AppSettings["BihapiUsername"];
        private readonly string apiPassword = ConfigurationManager.AppSettings["BihapiPassword"];

        private SportsFacilityConverter converter = new SportsFacilityConverter();

        public async Task<IEnumerable<SportsFacility>> GetSportsFacilities()
        {
            var json = await GetJson("sportFields");
            var data = JObject.Parse(json)["data"].ToString();
            return JsonConvert.DeserializeObject<IEnumerable<SportsFacility>>(data);
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
                    return await result.Content.ReadAsAsync<string>();
                }
            }
        }
    }
}