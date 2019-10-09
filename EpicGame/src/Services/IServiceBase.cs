using System.ServiceModel;

namespace EpicGame.src.Services
{
    [ServiceContract]
    public interface IServiceBase : IServiceUserDBHelper, IServiceGameDBHelper
    {
    }
}
