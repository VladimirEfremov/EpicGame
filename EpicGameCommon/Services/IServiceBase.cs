using EpicGameCommon.Models;
using System.ServiceModel;

namespace EpicGameCommon
{
    [ServiceContract]
    public interface IServiceBase : IServiceUserDBHelper, IServiceGameDBHelper
    {
        [OperationContract]
        string GetAllUserLogData(int userId);

        [OperationContract]
        string GetAllUserBattleEvents(int userId);

        [OperationContract]
        string GetAllUserProduceEvents(int userId);

        [OperationContract]
        string GetAllUserCommunicationEvents(int userId);

        [OperationContract]
        string UpdateLogData(int userId);

        [OperationContract]
        bool IsLogUpdated(int userId);

        [OperationContract]
        string GetDialogButtonInfo(int userId);
        
        [OperationContract]
        string GetDialogForUser(DialogId id);

        [OperationContract]
        void SendMessage(MessageToAdd toSend);

    }
}
