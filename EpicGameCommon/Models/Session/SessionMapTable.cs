namespace EpicGameCommon.Models.Session
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionMapTable
    {
        public int SessionMapId { get; set; }
        public decimal XCoord { get; set; }
        public decimal YCoord { get; set; }
    }
}
