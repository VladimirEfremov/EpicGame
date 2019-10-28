using System.Web.Mvc;
using System.Web.Security;
using EpicGameCommon;
using EpicGameCommon.Models.Session;
using EpicGameWeb.Models.Account;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    public class AuthController : Controller
    {
        public static int UserId;
        public static int CoreId;

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
            UserId = userId;

            var core = RemoteProcedureCallClass.GetGameChannel()
                .GetCoreByUserId(UserId).FromJson<SessionCoresTable>();
            
            if (core?.SessionCoreId != 0) { CoreId = core.SessionCoreId; }

            AccountData result = new AccountData();
            result.UserId = userId;
            result.CoreId = (core != null) ? core.SessionCoreId : 0;
            result.Nickname = User.Identity.Name;
            result.FriendsList = new string[] { "fr1" };
            result.FollowersList = new string[] { "fo2" };
            result.FollowingsList = new string[] { "fol3" };
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
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var allUsersJson = channel.GetAllUsersInfo();
            return allUsersJson;
        }

        [HttpGet]
        public string UsersFriendsInfo()
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var friendsUsersJson = channel.GetUsersFriendsInfo(UserId);
            return friendsUsersJson;
        }

        [HttpGet]
        public string UsersFollowersInfo()
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var followersUsersJson = channel.GetUsersFollowersInfo(UserId);
            return followersUsersJson;
        }

        [HttpGet]
        public string UsersFollowingsInfo()
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var followingsUsersJson = channel.GetUsersFollowingsInfo(UserId);
            return followingsUsersJson;
        }

        [HttpPost]
        public void SignOut(string signOut)
        {
            FormsAuthentication.SignOut();
        }

    }
}