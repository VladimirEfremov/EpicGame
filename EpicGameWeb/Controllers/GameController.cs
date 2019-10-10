using System.Web.Http;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/game")]
    [System.Web.Http.Cors.EnableCors(
       origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        [Route("dosomething")]
        //   /api/game/dosomething
        public void GetLoginResponse()
        {
        }


    }
}