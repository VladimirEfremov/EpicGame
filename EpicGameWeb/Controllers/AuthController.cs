using System.Web.Mvc;
using System.Web.Security;
using EpicGameCommon;
using EpicGameCommon.Models.Session;
using EpicGameWeb.Models;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    public class AuthController : Controller
    {

        public string GetSessionVariable(string variableName)
        {
            if (HttpContext.Request.Cookies[variableName]?.Value != null)
            {
                return HttpContext.Request.Cookies[variableName].Value;
            }
            return "";
        }

        public void SetSessionVariable(string variableName, string value)
        {
            HttpContext.Response.Cookies[variableName].Value = value;
        }

        public string Login(LoginModel loginModel)
        {
            SetSessionVariable("UserId", "");
            SetSessionVariable("CoreId", "");

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
            SetSessionVariable("UserId", "");
            SetSessionVariable("CoreId", "");

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
                            return model.ToJson();
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
            int userId = RemoteProcedureCallClass.GetUserChannel()
                .GetUserIdByNickname(User.Identity.Name);

            var core = RemoteProcedureCallClass.GetGameChannel()
                .GetCoreByUserId(userId).FromJson<SessionCoresTable>();

            SetSessionVariable("UserId", userId.ToString());
            if (core != null)
            {
                if (core.SessionCoreId != 0) 
                { 
                    SetSessionVariable("CoreId", core.SessionCoreId.ToString());
                }
            }

            AccountData result = new AccountData();
            result.UserId = userId;
            result.CoreId = (core != null) ? core.SessionCoreId : 0;
            result.Nickname = User.Identity.Name;
            return result.ToJson();
        }

        [HttpPost]
        public void AddUserToFriends(TwoUsers users)
        {
            RemoteProcedureCallClass
                .GetUserChannel()
                .AddUserToFriends(users.ToJson());
        }

        [HttpPost]
        public void RemoveUserFromFriends(TwoUsers users)
        {
            RemoteProcedureCallClass
                .GetUserChannel()
                .RemoveUserFromFriends(users.ToJson());
        }

        [HttpGet]
        public string AllUsersInfo()
        {
            var channel = RemoteProcedureCallClass.GetUserChannel();
            var allUsersJson = channel.GetAllUsersInfo();
            return allUsersJson;
        }

        [HttpGet]
        public string UsersFriendsInfo()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var channel = RemoteProcedureCallClass.GetUserChannel();
                var friendsUsersJson = channel.GetUsersFriendsInfo(userId);
                return friendsUsersJson;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string UsersFollowersInfo()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var channel = RemoteProcedureCallClass.GetUserChannel();
                var followersUsersJson = channel.GetUsersFollowersInfo(userId);
                return followersUsersJson;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string UsersFollowingsInfo()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var channel = RemoteProcedureCallClass.GetUserChannel();
                var followingsUsersJson = channel.GetUsersFollowingsInfo(userId);
                return followingsUsersJson;
            }
            else
            {
                return "";
            }
        }

        [HttpPost]
        public void SignOut(string signOut)
        {
            FormsAuthentication.SignOut();
        }

        [HttpGet]
        public string GetAllUserLogData()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                string result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetAllUserLogData(userId);
                return result;
            }
            else
            {
                return "";
            }
        }
        
        [HttpGet]
        public string GetAllUserBattleEvents()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                string result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetAllUserBattleEvents(userId);
                return result;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string GetAllUserProduceEvents()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                string result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetAllUserProduceEvents(userId);
                return result;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string GetAllUserCommunicationEvents()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                string result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetAllUserCommunicationEvents(userId);
                return result;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public string UpdateLogData()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                string result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .UpdateLogData(userId);
                return result;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public int IsLogUpdated()
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                bool result = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .IsLogUpdated(userId);
                return (result ? 1 : 0);
            }
            else
            {
                return -1;
            }
        }

    }
}