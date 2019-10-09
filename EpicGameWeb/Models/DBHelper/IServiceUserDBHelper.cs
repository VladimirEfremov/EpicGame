﻿using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
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
        void AddUserToFriends(System.Int32 thisId, System.Int32 idToAdd);
        [OperationContract]
        void RemoveUserFromFriends(System.Int32 thisId, System.Int32 idToRemove);
    }
}
