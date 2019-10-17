using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    [ServiceContract]
    public interface IServiceGameDBHelper
    {
        [OperationContract]
        SessionCoresTable GetCoreById(int coreId);

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

    }
}
