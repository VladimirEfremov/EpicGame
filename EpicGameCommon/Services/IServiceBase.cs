using System.ServiceModel;

namespace EpicGameCommon
{
    [ServiceContract]
    public interface IServiceBase : IServiceUserDBHelper, IServiceGameDBHelper
    {
    }
}
