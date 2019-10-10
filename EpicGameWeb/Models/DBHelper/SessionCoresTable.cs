namespace EpicGameWeb.Models.DBHelper
{
    public class SessionCoresTable
    {
        public int SessionCoreId { get; set; }
        public int UserId { get; set; }
        public int CoreMapId { get; set; }
        public int Money { get; set; }
        public int HPUpgrade { get; set; }
        public int AttackUpgrade { get; set; }
        public int DefenceUpgrade { get; set; }
        public int CapacityUpgrade { get; set; }
        public int WarriorsNumber { get; set; }
        public int AttackAircraftNumber { get; set; }
        public int WorkerNumber { get; set; }
        public int Casern { get; set; }
        public int DefenceTower { get; set; }
        public int GoldMining { get; set; }
    }
}