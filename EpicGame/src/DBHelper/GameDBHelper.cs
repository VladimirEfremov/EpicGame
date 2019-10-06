namespace EpicGame.src.GameDB
{
    using System.Linq;
    using System.Collections.Generic;

    using NLog;
    using EpicGame.src.Models.Game;
    class GameDBHelper : System.IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly GameUnitsEntity m_GameUnitsEntity = new GameUnitsEntity();
        private readonly GameUnitsTypeEntity m_GameUnitsTypeEntity = new GameUnitsTypeEntity();
        private readonly GameBuildingsEntity m_GameBuildingsEntity = new GameBuildingsEntity();
        private readonly GameBuildingsTypeEntity m_GameBuildingsTypeEntity = new GameBuildingsTypeEntity();
        private readonly GameBuildingProductionEntity m_GameBuildingProductionEntity = new GameBuildingProductionEntity();

        public GameDBHelper()
        {
            logger.Info("Create GameDBHelper instance");
        }

        public void Dispose()
        {
            logger.Info("Disposing context.");
            NLog.LogManager.Shutdown();

            m_GameUnitsEntity.Dispose();
            m_GameUnitsTypeEntity.Dispose();
            m_GameBuildingsEntity.Dispose();
            m_GameBuildingsTypeEntity.Dispose();
            m_GameBuildingProductionEntity.Dispose();
        }

        public List<GameUnitsTable> GetAllGameUnitsList()
        {
            return m_GameUnitsEntity.GameUnitsTable.ToList();
        }

        public List<GameUnitTypeTable> GetAllGameUnitsTypeList()
        {
            return m_GameUnitsTypeEntity.GameUnitTypeTable.ToList();
        }

        public List<GameBuildingsTable> GetAllGameBuildingsList()
        {
            return m_GameBuildingsEntity.GameBuildingsTable.ToList();
        }

        public List<GameBuildingsTypeTable> GetAllGameBuildingsTypeList()
        {
            return m_GameBuildingsTypeEntity.GameBuildingsTypeTable.ToList();
        }
        
        public List<GameBuildingProductionTable> GetAllGameBuildingProductionList()
        {
            return m_GameBuildingProductionEntity.GameBuildingProductionTable.ToList();
        }



    }
}
