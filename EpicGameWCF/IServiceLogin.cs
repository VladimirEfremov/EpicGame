﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EpicGameWCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceLogin" в коде и файле конфигурации.
    [ServiceContract]
    public interface IServiceLogin
    {
        [OperationContract]
        void DoWork();
    }
}
