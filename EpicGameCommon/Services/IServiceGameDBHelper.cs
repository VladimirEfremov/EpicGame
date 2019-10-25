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

    }
}
