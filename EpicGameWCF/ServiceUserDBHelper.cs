using System;

namespace EpicGameWCF
{
    public class ServiceUserDBHelper : IServiceUserDBHelper
    {
        public void AddUserToFriends(int thisId, int idToAdd)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisteredUser(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public void RegisterUserToTable(string firstName, string secondName, string passswordHash, string nickname, string email)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromFriends(int thisId, int idToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
