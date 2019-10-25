namespace EpicGame.src.Models.Game
{
    public partial class GameBuildingProductionTable
    {
        public int GameBuildingProductionTableId { get; set; }
        public int GameBuildingId { get; set; }
        public int GameUnitId { get; set; }
    }
}
