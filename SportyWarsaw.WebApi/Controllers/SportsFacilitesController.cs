using SportyWarsaw.Domain;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportyWarsaw.WebApi.Controllers
{
    public class SportsFacilitiesController : ApiController
    {
        private SportyWarsawContext context = new SportyWarsawContext();

        public async Task<IHttpActionResult> Get(int id)
        {
            string user = ConfigurationManager.AppSettings["BihapiUsername"];
            string password = ConfigurationManager.AppSettings["BihapiPassword"];
            string uri = "https://api.bihapi.pl/wfs/warszawa/";
            using (var handler = new WebRequestHandler())
            {
                handler.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                using (var client = new HttpClient(handler))
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{user}:{password}");
                    var header = new AuthenticationHeaderValue(
                               "Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;
                    client.BaseAddress = new Uri(uri);
                    var result = await client.GetStringAsync("sportFields?maxFeatures=5");
                    return Ok(result);
                }
            }
        }
    }
}
