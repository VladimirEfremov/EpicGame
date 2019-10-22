using EpicGame.src.Models;
using EpicGame.src.Models.User;
using System.ServiceModel;

namespace EpicGame.src.Services
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceLogin" в коде и файле конфигурации.
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
