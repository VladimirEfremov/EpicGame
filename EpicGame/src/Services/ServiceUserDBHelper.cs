using System.ServiceModel;

namespace EpicGame.src.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceUserDBHelper : IServiceUserDBHelper
    {
        EpicGame.src.DBHelper.UserDBHelper dbHelperInstance = 
            new EpicGame.src.DBHelper.UserDBHelper();
        public void AddUserToFriends(int thisId, int idToAdd)
        {
            dbHelperInstance.AddUserToFriends(thisId, idToAdd);
        }

        public bool IsRegisteredUser(string email, string passwordHash)
        {
            return dbHelperInstance.IsRegisteredUser(email, passwordHash);
        }

        public void RegisterUserToTable(string firstName, string secondName, string passswordHash, string nickname, string email)
        {
            dbHelperInstance.RegisterUserToTable(firstName, secondName, passswordHash, nickname, email);
        }

        public void RemoveUserFromFriends(int thisId, int idToRemove)
        {
            dbHelperInstance.RemoveUserFromFriends(thisId, idToRemove);
        }
    }
}
