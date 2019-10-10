using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    [ServiceContract]
    public interface IServiceGameDBHelper
    {
        [OperationContract]
        SessionCoresTable GetCoreById(int coreId);

        [OperationContract]
        int GetNumberOfCoreWarriors(int coreId);

        [OperationContract]
        int GetNumberOfCoreAttackAircraft(int coreId);
    }
}
