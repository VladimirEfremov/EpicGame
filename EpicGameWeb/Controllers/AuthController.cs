using System.Web.Http;
using System.Web.Security;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;
using EpicGameWeb.Models.Game;
using EpicGameWeb.Models.JSONHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/auth")]
    [System.Web.Http.Cors.EnableCors(
       origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        [Route("login")]
        public string PostLoginResponse([FromBody]object value)
        {
            LoginModel model =
                value.ToString().FromJson<LoginModel>();
            if (ModelState.IsValid)
            {
                if (RemoteProcedureCallClass
                    .GetRemoteChannel()
                    .IsRegisteredUser(model.Nickname, model.PasswordHash))
                {
                    FormsAuthentication.SetAuthCookie(model.Nickname, true);
                    
                    return new AuthResponse(true).ToJson();
                }
                else
                {
                    return "error: user is not registered!";
                }
            }
            else
            {
                return "error: model not valid!";
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
                    .GetRemoteChannel()
                    .IsRegisteredUser(model.Nickname, model.PasswordHash))
                {
                    if (RemoteProcedureCallClass
                        .GetRemoteChannel()
                        .RegisterUserToTable(model.FirstName, 
                            model.SecondName, model.PasswordHash, 
                            model.Nickname, model.Email))
                    {
                        FormsAuthentication.SetAuthCookie(model.Nickname, true);
                        return new AuthResponse(true).ToJson();
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

        [Route("isauth")]
        public string IsAuthenticated()
        {
            AccountData accountData;
            accountData.IsAuthorized = User.Identity.IsAuthenticated;
            accountData.Nickname = User.Identity.Name;
            accountData.FriendList = new string[0];
            string result = accountData.ToJson();
            return result;
        }

        [Route("logout")]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}