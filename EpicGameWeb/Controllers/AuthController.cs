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
            AccountData result = new AccountData();
            result.Nickname = User.Identity.Name;
            result.FriendsList = new string[] { "fr1" };
            result.FollowersList = new string[] { "fo2" };
            result.FollowingsList = new string[] { "fol3" };
            return result.ToJson();
        }

        [HttpPost]
        public void AddUserToFriends(int thisId, int idToAdd)
        {
            RemoteProcedureCallClass
                .GetUserChannel()
                .AddUserToFriends(thisId, idToAdd);
        }

        [HttpPost]
        public void RemoveUserFromFriends(int thisId, int idToRemove)
        {
            RemoteProcedureCallClass
                .GetUserChannel()
                .RemoveUserFromFriends(thisId, idToRemove);
        }

        [HttpGet]
        public string GetAllUsers()
        {
            var allUsers = RemoteProcedureCallClass
                .GetUserChannel()
                .GetAllUsers();
            return allUsers.ToJson();
        }

        [HttpGet]
        public UserFriendsTable[] GetUsersFriendsTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFriendsTable(userId).ToArray();
        }

        [HttpGet]
        public UserFollowersTable[] GetUsersFollowersTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFollowersTable(userId).ToArray();
        }

        [HttpGet]
        public UserFollowingTable[] GetUsersFollowingsTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFollowingsTable(userId).ToArray();
        }

        [HttpPost]
        public void SignOut(string signOut)
        {
            FormsAuthentication.SignOut();
        }

    }
}