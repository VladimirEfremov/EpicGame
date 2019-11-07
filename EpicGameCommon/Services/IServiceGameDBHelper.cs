using EpicGameCommon.Response;
using System.ServiceModel;

namespace EpicGameCommon
{
    [ServiceContract]
    public interface IServiceGameDBHelper
    {
        [OperationContract]
        string GetCoreById(int coreId);

        [OperationContract]
        string GetCoreByUserId(int userId);

        [OperationContract]
        int CasernGetNumberOfWarriors(int coreId);

        [OperationContract]
        int CasernGetNumberOfAttackAircraft(int coreId);

        [OperationContract]
        void CoreBuildCasern(int coreId);

        [OperationContract]
        void CoreBuildGoldMining(int coreId);

        [OperationContract]
        void CoreBuildDefenceTower(int coreId);

        [OperationContract]
        void BaseProduceWorker(int coreId);

        [OperationContract]
        void CasernProduceWarrior(int coreId);

        [OperationContract]
        void CasernProduceAttackAircraft(int coreId);

        [OperationContract]
        void GoldMiningAddWorker(int coreId);

        [OperationContract]
        string GetCoreInfoById(int coreId);

        [OperationContract]
        string DuelBattle(int attackerCoreId, int defenderCoreId);

        [OperationContract]
        string CoreBattle(int attackerCoreId, int defenderCoreId);

        [OperationContract]
        void BaseAttackUpgrade(int coreId);

        [OperationContract]
        void BaseDefenceUpgrade(int coreId);

        [OperationContract]
        void BaseCapacityUpgrade(int coreId);

        [OperationContract]
        void CasernCapacityUpgrade(int coreId);

        [OperationContract]
        void GoldMiningCapacityUpgrade(int coreId);

        [OperationContract]
        void DefenceTowerAttackUpgrade(int coreId);

        [OperationContract]
        void DefenceTowerDefenceUpgrade(int coreId);
    }
}
