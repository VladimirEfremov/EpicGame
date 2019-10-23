using System.Collections.Generic;
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

            AccountData result = new AccountData();
            result.UserId = userId;
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
                .AddUserToFriends(users);
        }

        [HttpPost]
        public void RemoveUserFromFriends(TwoUsers users)
        {
            RemoteProcedureCallClass
                .GetUserChannel()
                .RemoveUserFromFriends(users);
        }

        [HttpGet]
        public string GetAllUsersInfo()
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var allUsersJson = channel.GetAllUsersInfo();
            var allUsers = allUsersJson.FromJson<UserInfo[]>();
            return allUsers.ToJson();
        }

        [HttpGet]
        public UserInfo[] GetUsersFriendsInfo(int userId)
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var friendsUsersJson = channel.GetUsersFriendsInfo(userId);
            var friendsUsers = friendsUsersJson.FromJson<UserInfo[]>();
            return friendsUsers;
        }

        [HttpGet]
        public UserInfo[] GetUsersFollowersInfo(int userId)
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var followersUsersJson = channel.GetUsersFollowersInfo(userId);
            var followersUsers = followersUsersJson.FromJson<UserInfo[]>();
            return followersUsers;
        }

        [HttpGet]
        public UserInfo[] GetUsersFollowingsInfo(int userId)
        {
            var channel = RemoteProcedureCallClass
                .GetUserChannel();
            var followingsUsersJson = channel.GetUsersFollowingsInfo(userId);
            var followingsUsers = followingsUsersJson.FromJson<UserInfo[]>();
            return followingsUsers;
        }

        [HttpPost]
        public void SignOut(string signOut)
        {
            FormsAuthentication.SignOut();
        }

    }
}