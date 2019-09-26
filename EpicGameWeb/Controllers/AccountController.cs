using System.Web.Mvc;
using System.Web.Security;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;
using EpicGameWeb.Models.JSONHelper;

namespace EpicGameWeb.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(string stringModel)
        {
            RegistrationModel model =
                    stringModel.FromJson<RegistrationModel>();
            if (ModelState.IsValid)
            {
                if (!RemoteProcedureCallClass.GetRemoteChannel().RegisterUserToTable(
                    model.FirstName, model.SecondName,
                    model.PassswordHash, model.Nickname, model.Email))
                {
                    ModelState.AddModelError("", "На данный email уже зарегестрирован аккаунт!");
                }

                bool userInDB = RemoteProcedureCallClass
                    .GetRemoteChannel()
                    .IsRegisteredUser(model.Email, model.PassswordHash);
                if (userInDB)
                {
                    //true - запомнить меня
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
            }

            if (model != null) { return View(model); }
            else { return View(); }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string stringModel)
        {
            LoginModel model =
                    stringModel.FromJson<LoginModel>();
            if (ModelState.IsValid)
            {
                if (RemoteProcedureCallClass
                    .GetRemoteChannel()
                    .IsRegisteredUser(model.Email, model.PasswordHash))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Такого пользователя не существует!");
                }
            }
            if (model != null) { return View(model); }
            else { return View(); }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}