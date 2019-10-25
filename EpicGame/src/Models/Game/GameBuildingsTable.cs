namespace EpicGame.src.Models.Game
{
    public partial class GameBuildingsTable
    {
        public int GameBuildingId { get; set; }
        public int GameBuildingType { get; set; }
        public string GameBuildingName { get; set; }
        public int GameBuildingHP { get; set; }
        public int GameBuildingAttack { get; set; }
        public int GameBuildingDefence { get; set; }
        public int GameBuildingGoldIncome { get; set; }
        public int GameBuildingGoldOutcome { get; set; }
        public int Capacity { get; set; }
    }
}
