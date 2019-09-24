using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [Authorize]
        public IEnumerable<string> Get()
        {
            bool response =
                RemoteProcedureCallClass.GetRemoteChannel().IsRegisteredUser(
                    "AntonAgranov800@gmail.com", 
                    "123");

            string registered = response ? "TRUE" : "FALSE";

            return new string[] { "isRegistered", registered };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
