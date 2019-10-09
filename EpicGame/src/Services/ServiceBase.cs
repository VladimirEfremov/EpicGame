using System.ServiceModel;

using EpicGame.src.Models.Session;


namespace EpicGame.src.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ServiceBase : IServiceBase
    {
        DBHelper.UserDBHelper m_UserDBHelper = new DBHelper.UserDBHelper();
        DBHelper.GameDBHelper m_GameDBHelper = new DBHelper.GameDBHelper();
        
        public SessionCoresTable GetCoreById(int coreId)
        {
            return m_GameDBHelper.GetCoreById(coreId);
        }

        public int GetNumberOfCoreWarriors(int coreId)
        {
            return m_GameDBHelper.GetNumberOfCoreWarriors(coreId);
        }
        public int GetNumberOfCoreAttackAircraft(int coreId)
        {
            return m_GameDBHelper.GetNumberOfCoreAttackAircraft(coreId);
        }

        public void AddUserToFriends(int thisId, int idToAdd)
        {
            m_UserDBHelper.AddUserToFriends(thisId, idToAdd);
        }

        public bool IsRegisteredUser(string email, string passwordHash)
        {
            return m_UserDBHelper.IsRegisteredUser(email, passwordHash);
        }

        public bool RegisterUserToTable(string firstName, string secondName, string passswordHash, string nickname, string email)
        {
            return m_UserDBHelper.RegisterUserToTable(firstName, secondName, passswordHash, nickname, email);
        }

        public void RemoveUserFromFriends(int thisId, int idToRemove)
        {
            m_UserDBHelper.RemoveUserFromFriends(thisId, idToRemove);
        }
    }
}
