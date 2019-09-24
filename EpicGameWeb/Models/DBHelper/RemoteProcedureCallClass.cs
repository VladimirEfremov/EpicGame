using System;
using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    public static class RemoteProcedureCallClass
    {
        static BasicHttpBinding s_Binding = new BasicHttpBinding();
        static EndpointAddress s_EndPoint = new EndpointAddress(
            new Uri("http://127.0.0.1:2001/IServiceUserDBHelper"));
        static ChannelFactory<IServiceUserDBHelper> s_Factory = 
            new ChannelFactory<IServiceUserDBHelper>(s_Binding, s_EndPoint);
        static IServiceUserDBHelper s_RemoteChannel = s_Factory.CreateChannel();

        public static IServiceUserDBHelper GetRemoteChannel()
        {
            return s_RemoteChannel;
        }

    }
}