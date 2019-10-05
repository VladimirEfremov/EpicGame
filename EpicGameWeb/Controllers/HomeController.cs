using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EpicGameWeb.Controllers
{
    //[RoutePrefix("api/home")]
    [System.Web.Http.Cors.EnableCors(
        origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        //[Route("index")]
        public void PostIndex([FromBody]object value)
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

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

    }
}
