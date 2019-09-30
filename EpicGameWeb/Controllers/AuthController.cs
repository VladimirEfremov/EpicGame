using System.Web.Http;
using System.Web.Security;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;
using EpicGameWeb.Models.JSONHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/auth")]
    [System.Web.Http.Cors.EnableCors(
       origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        [Route("post")]
        public string PostAuthResponse([FromBody]object value)
        {
            LoginModel model =
                value.ToString().FromJson<LoginModel>(); ;
            if (ModelState.IsValid)
            {
                if (RemoteProcedureCallClass
                    .GetRemoteChannel()
                    .IsRegisteredUser(model.Email, model.PasswordHash))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    
                    return new AuthResponse(true, User.Identity.Name).ToJson();
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


    }
}