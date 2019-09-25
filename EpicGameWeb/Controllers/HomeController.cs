using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpicGameWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            //
            //string result = "Вы не авторизированы";
            //if (User.Identity.IsAuthenticated)
            //{
            //    result = $"Ваш логин[email]: {User.Identity.Name}";
            //}
            //
            //return result;
            return View();
        }
    }
}
