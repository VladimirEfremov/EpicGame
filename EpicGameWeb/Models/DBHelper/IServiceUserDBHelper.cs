using System.Collections.Generic;
using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    [ServiceContract]
    public interface IServiceUserDBHelper
    {
        [OperationContract]
        bool RegisterUserToTable(string firstName, string secondName,
            string passswordHash, string nickname, string email);
        [OperationContract]
        bool IsRegisteredUser(string email, string passwordHash);

        [OperationContract]
        void AddUserToFriends(int thisId, int idToAdd);

        [OperationContract]
        void RemoveUserFromFriends(int thisId, int idToRemove);

        [OperationContract]
        UserInfo[] GetAllUsersInfo();

        [OperationContract]
        UserInfo[] GetUsersFriendsInfo(int userId);

        [OperationContract]
        UserInfo[] GetUsersFollowersInfo(int userId);

        [OperationContract]
        UserInfo[] GetUsersFollowingsInfo(int userId);
    }
}
