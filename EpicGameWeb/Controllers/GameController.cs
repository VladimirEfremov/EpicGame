using System.Web.Http;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/game")]
    [System.Web.Http.Cors.EnableCors(
       origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        [Route("dosomething")]
        public void PostLoginResponse([FromBody]object value)
        {
        }
    }
}