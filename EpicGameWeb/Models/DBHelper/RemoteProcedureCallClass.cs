using EpicGameCommon;
using System;
using System.ServiceModel;

namespace EpicGameWeb.Models.DBHelper
{
    public static class RemoteProcedureCallClass
    {
        static BasicHttpBinding s_Binding;
        static EndpointAddress s_EndPoint;
        static ChannelFactory<IServiceBase> s_Factory;
        static IServiceBase s_RemoteChannel;

        static RemoteProcedureCallClass()
        {
            s_Binding = new BasicHttpBinding();
            s_Binding.SendTimeout = new TimeSpan(0, 15, 0);
            s_Binding.ReceiveTimeout = new TimeSpan(0, 15, 0);
            s_EndPoint = new EndpointAddress(
                new Uri("http://127.0.0.1:2001/IServiceBase"));
            s_Factory =
                new ChannelFactory<IServiceBase>(s_Binding, s_EndPoint);
            s_RemoteChannel = s_Factory.CreateChannel();
        }

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