namespace EpicGameCommon
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionCasernsTable
    {
        public int SessionCasernId { get; set; }
        public int SessionCoreId { get; set; }
        public int BuildingLevel { get; set; }
        public int WarriorsNumber { get; set; }
        public int AttackAircraftNumber { get; set; }
        public int UniqueUpgrade { get; set; }
        public int CapacityUpgrade { get; set; }
    }
}
