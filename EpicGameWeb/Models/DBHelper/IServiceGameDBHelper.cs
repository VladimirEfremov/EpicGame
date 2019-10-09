using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    [ServiceContract]
    public interface IServiceGameDBHelper
    {
        [OperationContract]
        void Gameplay();
    }
}
