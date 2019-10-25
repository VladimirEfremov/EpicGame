namespace EpicGame.src.Models.Session
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionDefenceTowersTable
    {
        public int SessionDefenceTowerId { get; set; }
        public int SessionCoreId { get; set; }
        public int BuildingLevel { get; set; }
        public int UniqueUpgrade { get; set; }
        public int AttackUpgrade { get; set; }
        public int DefenceUpgrade { get; set; }
    }
}
