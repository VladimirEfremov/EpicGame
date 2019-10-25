namespace EpicGameCommon.Models.Session
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionCoresTable
    {
        public int SessionCoreId { get; set; }
        public int UserId { get; set; }
        public int CoreMapId { get; set; }
        public int Money { get; set; }
    }
}
