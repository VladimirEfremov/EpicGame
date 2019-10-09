using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    [ServiceContract]
    public interface IServiceBase : IServiceUserDBHelper, IServiceGameDBHelper
    {
    }
}
