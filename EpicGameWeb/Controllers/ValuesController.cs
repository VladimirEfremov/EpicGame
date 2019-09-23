using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.ServiceModel;

namespace EpicGameWeb.Controllers
{
    public class ValuesController : ApiController
    {
        static BasicHttpBinding binding = new BasicHttpBinding();
        static EndpointAddress endpoint = new EndpointAddress(new Uri("http://127.0.0.1:2001/IServiceUserDBHelper"));
        static ChannelFactory<IServiceUserDBHelper> factory = new ChannelFactory<IServiceUserDBHelper>(binding, endpoint);
        static IServiceUserDBHelper channel = factory.CreateChannel();

        // GET api/values
        public IEnumerable<string> Get()
        {
            bool response =
                channel.IsRegisteredUser(
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
