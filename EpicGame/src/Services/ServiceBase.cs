using System.ServiceModel;

using EpicGameCommon;
using EpicGame.src.DBHelper;

namespace EpicGame.src.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ServiceBase : IServiceBase
    {
        UserDBHelper m_UserDBHelper = new UserDBHelper();
        GameDBHelper m_GameDBHelper = new GameDBHelper();
        
        //Game
        public string GetCoreById(int coreId)
        {
            return m_GameDBHelper.GetCoreById(coreId);
        }

        public string GetCoreByUserId(int userId)
        {
            return m_GameDBHelper.GetCoreByUserId(userId);
        }
        
        public int CasernGetNumberOfWarriors(int coreId)
        {
            return m_GameDBHelper.CasernGetNumberOfWarriors(coreId);
        }
        public int CasernGetNumberOfAttackAircraft(int coreId)
        {
            return m_GameDBHelper.CasernGetNumberOfAttackAircraft(coreId);
        }

        public string FindCoreMapByMapId(int mapId)
        {
            return m_GameDBHelper.FindCoreMapByMapIdAsNoTracking(mapId);
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
        public void AddUserToFriends(string users)
        {
            TwoUsers twoUsers = users.FromJson<TwoUsers>();
            m_UserDBHelper.AddUserToFriends(twoUsers.FirstId, twoUsers.SecondId);
        }

        public void RemoveUserFromFriends(string users)
        {
            TwoUsers twoUsers = users.FromJson<TwoUsers>();
            m_UserDBHelper.RemoveUserFromFriends(twoUsers.FirstId, twoUsers.SecondId);
        }

        public bool IsRegisteredUser(string nickname, string passwordHash)
        {
            return m_UserDBHelper.IsRegisteredUser(nickname, passwordHash);
        }

        public int GetUserIdByNickname(string nickname)
        {
            return m_UserDBHelper.GetUserIdByNickname(nickname);
        }

        public bool RegisterUserToTable(string firstName, string secondName, string passswordHash, string nickname, string email)
        {
            return m_UserDBHelper.RegisterUserToTable(firstName, secondName, passswordHash, nickname, email);
        }

        public string GetAllUsersInfo()
        {
            var usersInfo = m_UserDBHelper.GetAllUsersInfo();
            return usersInfo;
        }

        public string GetUsersFriendsInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFriendsInfo(userId);
        }

        public string GetUsersFollowersInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFollowersInfo(userId);
        }

        public string GetUsersFollowingsInfo(int userId)
        {
            return m_UserDBHelper.GetUsersFollowingsInfo(userId);
        }

        public string GetCoreInfoById(int coreId)
        {
            return m_GameDBHelper.GetCoreInfoById(coreId);
        }

        public string DuelBattle(int attackerCoreId, int defenderCoreId)
        {
            var result = m_GameDBHelper
                .DuelBattle(attackerCoreId, defenderCoreId)
                .ToJson();
            return result;
        }

        public string CoreBattle(int attackerCoreId, int defenderCoreId)
        {
            var result = m_GameDBHelper
                .CoreBattle(attackerCoreId, defenderCoreId)
                .ToJson();
            return result;
        }
    }
}
