using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace JarvisAPI.Controllers
{
    public class ProvidersController : ApiController
    {
        // GET: api/Providers
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Get()
        {
            JarvisEntities db = new JarvisEntities(); 
            var providers = db.ProviderAccounts.Select(p => new { username = p.Account.AspNetUser.UserName, p.provider }).ToList();

            JArray stoArray = (JArray)JToken.FromObject(providers);
            dynamic obj = new JObject();
            obj.providerInfo = stoArray;

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // GET: api/Providers/nest
        public IHttpActionResult Get(string id)
        {
            JarvisEntities db = new JarvisEntities();
            var providers = db.ProviderAccounts.Select(p => new { username = p.Account.AspNetUser.UserName, p.provider })
                                                    .Where(p=> p.provider == id).ToList();

            JArray stoArray = (JArray)JToken.FromObject(providers);
            dynamic obj = new JObject();
            obj.Data = stoArray;

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // POST: api/Providers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Providers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Providers/5
        public void Delete(int id)
        {
        }
    }
}
