using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [Route("post")]
        public void Index([FromBody]string value)
        {
            //ViewBag.Title = "Home Page";
            //
            //string result = "Вы не авторизированы";
            if (User.Identity.IsAuthenticated)
            {
            //    result = $"Ваш логин[email]: {User.Identity.Name}";
            }
            //
            //return result;
            //return View();
        }
    }
}
