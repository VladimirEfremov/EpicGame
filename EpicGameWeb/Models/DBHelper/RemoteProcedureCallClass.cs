using System;
using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    public static class RemoteProcedureCallClass
    {
        static BasicHttpBinding s_Binding = new BasicHttpBinding();
        static EndpointAddress s_EndPoint = new EndpointAddress(
            new Uri("http://127.0.0.1:2001/IServiceBase"));
        static ChannelFactory<IServiceBase> s_Factory = 
            new ChannelFactory<IServiceBase>(s_Binding, s_EndPoint);
        static IServiceBase s_RemoteChannel = s_Factory.CreateChannel();

        public static IServiceBase GetBaseChannel()
        {
            return s_RemoteChannel;
        }

        public static IServiceUserDBHelper GetUserChannel()
        {
            return s_RemoteChannel;
        }

        public static IServiceGameDBHelper GetGameChannel()
        {
            return s_RemoteChannel;
        }

    }
}