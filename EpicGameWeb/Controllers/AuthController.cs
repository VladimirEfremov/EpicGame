using System.Threading;
using System.Web.Http;
using System.Web.Security;

using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;
using EpicGameWeb.Models.JSONHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/auth")]
    [System.Web.Http.Cors
        .EnableCors(
        origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        [HttpPost, AllowAnonymous, Route("login")]
        public string PostLogin([FromBody]object value)
        {
            LoginModel model =
                value.ToString().FromJson<LoginModel>();
            if (ModelState.IsValid)
            {
                if (RemoteProcedureCallClass
                    .GetUserChannel()
                    .IsRegisteredUser(model.Nickname, model.PasswordHash))
                {
                    FormsAuthentication.SetAuthCookie(model.Nickname, true);
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

        [Route("registration")]
        public string PostRegistrationResponse([FromBody]object value)
        {
            RegistrationModel model =
                value.ToString().FromJson<RegistrationModel>(); ;
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
                    return "error: ";
                }
                else
                {
                    return "error: user already registered!";
                }
            }
            else
            {
                return "error: model not valid!";
            }
        }

        [Route("get_account_data")]
        public string GetAccountData()
        {
            AccountData result = new AccountData(Thread.CurrentPrincipal.Identity.Name);
            return result.ToJson();
        }

        [HttpPost, Route("logout")]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}