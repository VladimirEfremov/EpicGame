using System.Threading;
using System.Web.Mvc;
using System.Web.Security;

using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;
using EpicGameWeb.Models.JSONHelper;

namespace EpicGameWeb.Controllers
{
    public class AuthController : Controller
    {
        public string Login(LoginModel loginModel)
        {
            if (loginModel != null)
            {
                if (ModelState.IsValid)
                {
                    if (RemoteProcedureCallClass
                        .GetUserChannel()
                        .IsRegisteredUser(loginModel.Nickname, loginModel.PasswordHash))
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.Nickname, true);
                        return true.ToJson();
                    }
                    else
                    {
                        ModelState.AddModelError("", "error: user is not registered!");
                        return "";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "error: model not valid!");
                    return "";
                }
            }
            return "";
        }

        public string Registration(RegistrationModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    if (!RemoteProcedureCallClass
                        .GetUserChannel()
                        .IsRegisteredUser(model.Nickname, model.PasswordHash))
                    {
                        if (RemoteProcedureCallClass
                            .GetUserChannel()
                            .RegisterUserToTable(model.FirstName,
                                model.SecondName, model.PasswordHash,
                                model.Nickname, model.Email))
                        {
                            FormsAuthentication.SetAuthCookie(model.Nickname, true);
                            return true.ToJson();
                        }
                        return "";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string GetAccountData()
        {
            AccountData result = new AccountData(User.Identity.Name);
            return result.ToJson();
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}