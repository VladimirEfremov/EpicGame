using EpicGame.src.Models.Session;
using System.ServiceModel;

namespace EpicGame.src.Services
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
