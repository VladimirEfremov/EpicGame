namespace EpicGame.src.Models.Session
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionGoldMiningsTable
    {
        public int SessionGoldMiningId { get; set; }
        public int SessionCoreId { get; set; }
        public int BuildingLevel { get; set; }
        public int WorkersNumber { get; set; }
        public int UniqueUpgrade { get; set; }
        public int CapacityUpgrade { get; set; }
    }
}
