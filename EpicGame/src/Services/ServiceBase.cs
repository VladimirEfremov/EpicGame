using System.Collections.Generic;
using System.ServiceModel;
using EpicGame.src.Models;
using EpicGame.src.Models.Session;
using EpicGame.src.Models.User;

namespace EpicGame.src.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ServiceBase : IServiceBase
    {
        DBHelper.UserDBHelper m_UserDBHelper = new DBHelper.UserDBHelper();
        DBHelper.GameDBHelper m_GameDBHelper = new DBHelper.GameDBHelper();
        
        //Game
        public SessionCoresTable GetCoreById(int coreId)
        {
            return m_GameDBHelper.GetCoreById(coreId);
        }

        public int CasernGetNumberOfWarriors(int coreId)
        {
            return m_GameDBHelper.CasernGetNumberOfWarriors(coreId);
        }
        public int CasernGetNumberOfAttackAircraft(int coreId)
        {
            return m_GameDBHelper.CasernGetNumberOfAttackAircraft(coreId);
        }

        public SessionMapTable FindCoreMapByMapId(int mapId)
        {
            return m_GameDBHelper.FindCoreMapByMapId(mapId);
        }

        public void CoreBuildCasern(int coreId)
        {
            m_GameDBHelper.CoreBuildCasern(coreId);
        }

        public void CoreBuildGoldMining(int coreId)
        {
            m_GameDBHelper.CoreBuildGoldMining(coreId);
        }

        public void CoreBuildDefenceTower(int coreId)
        {
            m_GameDBHelper.CoreBuildDefenceTower(coreId);
        }

        public void BaseProduceWorker(int coreId)
        {
            m_GameDBHelper.BaseProduceWorker(coreId);
        }

        public void CasernProduceWarrior(int coreId)
        {
            m_GameDBHelper.CasernProduceWarrior(coreId);
        }

        public void CasernProduceAttackAircraft(int coreId)
        {
            m_GameDBHelper.CasernProduceAttackAircraft(coreId);
        }

        public void GoldMiningAddWorker(int coreId)
        {
            m_GameDBHelper.GoldMiningAddWorker(coreId);
        }

        //USER
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

        public UserInfo[] GetAllUsersInfo()
        {
            var usersInfo = m_UserDBHelper.GetAllUsersInfo();
            return usersInfo;
        }

        public UserInfo[] GetUsersFriendsInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFriendsInfo(userId);
        }

        public UserInfo[] GetUsersFollowersInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFollowersInfo(userId);
        }

        public UserInfo[] GetUsersFollowingsInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFollowingsInfo(userId);
        }
    }
}
