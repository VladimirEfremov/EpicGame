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
        bool IsRegisteredUser(string nickname, string passwordHash);

        [OperationContract]
        void AddUserToFriends(string users);

        [OperationContract]
        void RemoveUserFromFriends(string users);

        [OperationContract]
        int GetUserIdByNickname(string nickname);

        [OperationContract]
        string GetAllUsersInfo();

        [OperationContract]
        string GetUsersFriendsInfo(int userId);

        [OperationContract]
        string GetUsersFollowersInfo(int userId);

        [OperationContract]
        string GetUsersFollowingsInfo(int userId);
    }
}
