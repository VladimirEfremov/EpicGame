﻿using System.ServiceModel;

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
        void AddUserToFriends(System.Int32 thisId, System.Int32 idToAdd);
        [OperationContract]
        void RemoveUserFromFriends(System.Int32 thisId, System.Int32 idToRemove);

        [OperationContract]
        System.Collections.Generic.List<UserTable> GetAllUsers();

        [OperationContract]
        System.Collections.Generic.List<UserFriendsTable> GetUsersFriendsTable(int userId);

        [OperationContract]
        System.Collections.Generic.List<UserFollowersTable> GetUsersFollowersTable(int userId);

        [OperationContract]
        System.Collections.Generic.List<UserFollowingTable> GetUsersFollowingsTable(int userId);
    }
}
