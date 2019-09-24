using System.Web.Mvc;
using System.Web.Security;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;

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
        public ActionResult Registration(RegistrationModel model)
        {
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
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
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
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}