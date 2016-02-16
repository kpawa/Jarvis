using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JarvisAPI.Controllers
{
    public class AccountsController : ApiController
    {
        // GET: api/Accounts
        public IHttpActionResult Get()
        {
            JarvisEntities db = new JarvisEntities();

            var users = db.AspNetUsers.Select(a => new { Username = a.UserName, Email = a.Email }).ToList();
            JArray stoArray = (JArray)JToken.FromObject(users);
            dynamic obj = new JObject();
            obj.users = stoArray;

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // GET: api/Accounts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Accounts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Accounts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Accounts/5
        public void Delete(int id)
        {
        }
    }
}
