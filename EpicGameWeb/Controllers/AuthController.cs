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
        public AccountData GetAccountData()
        {
            AccountData result = new AccountData(User.Identity.Name);
            return result;
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

        [HttpPost]
        public System.Collections.Generic.List<UserTable> GetAllUsers()
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetAllUsers();
        }

        [HttpPost]
        public System.Collections.Generic.List<UserFriendsTable> GetUsersFriendsTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFriendsTable(userId);
        }

        [HttpPost]
        public System.Collections.Generic.List<UserFollowersTable> GetUsersFollowersTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFollowersTable(userId);
        }

        [HttpPost]
        public System.Collections.Generic.List<UserFollowingTable> GetUsersFollowingsTable(int userId)
        {
            return RemoteProcedureCallClass
                .GetUserChannel()
                .GetUsersFollowingsTable(userId);
        }

        [HttpPost]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}