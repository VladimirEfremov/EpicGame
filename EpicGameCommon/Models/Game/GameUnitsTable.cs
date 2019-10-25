namespace EpicGameCommon.Models.Game
{
    public partial class GameUnitsTable
    {
        public int GameUnitId { get; set; }
        public int GameUnitType { get; set; }
        public string GameUnitName { get; set; }
        public int GameUnitHP { get; set; }
        public int GameUnitAttack { get; set; }
        public int GameUnitDefence { get; set; }
        public int GameUnitGoldIncome { get; set; }
        public int GameUnitGoldOutcome { get; set; }
    }
}
