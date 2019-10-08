namespace EpicGame.src.GameDB
{
    using System.Linq;
    using System.Collections.Generic;

    using NLog;
    using EpicGame.src.Models.Game;
    using EpicGame.src.Models.Session;

    class GameDBHelper : System.IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly GameUnitsEntity m_GameUnitsEntity = new GameUnitsEntity();
        private readonly GameUnitsTypeEntity m_GameUnitsTypeEntity = new GameUnitsTypeEntity();
        private readonly GameBuildingsEntity m_GameBuildingsEntity = new GameBuildingsEntity();
        private readonly GameBuildingsTypeEntity m_GameBuildingsTypeEntity = new GameBuildingsTypeEntity();
        private readonly GameBuildingProductionEntity m_GameBuildingProductionEntity = new GameBuildingProductionEntity();

        private static readonly SessionMapEntity m_SessionMapEntity = new SessionMapEntity();
        private static readonly SessionCoresEntity m_SessionCoreEntity = new SessionCoresEntity();

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

            m_SessionMapEntity.Dispose();
            m_SessionCoreEntity.Dispose();
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

        public List<SessionCoresTable> GetAllCores()
        {
            return m_SessionCoreEntity.SessionCoresTable.ToList();
        }

        public SessionMapTable FindCoreMapByMapId(int mapId)
        {
            return m_SessionMapEntity
                .SessionMapTable
                .Where(obj => obj.SessionMapId == mapId)
                .FirstOrDefault();
        }

        private static SessionMapTable m_PrevPosition = new SessionMapTable()
        {
            XCoord = (decimal) 500 * 7_000_000,
            YCoord = (decimal)-500 * 7_000_000
        };
        public static void GenerateCoreForUser(int userId)
        {
            //Map {0, 0} - center of universe
            //1 EGM == 100m
            //7 000 000 EGM radiuse of sun (ROS)
            //1 2 3 4 of universe
            //4 x > 5 * ROS && y < 5 * -ROS 
            SessionMapTable coreCoord = new SessionMapTable()
            {
                XCoord = m_PrevPosition.XCoord,
                YCoord = m_PrevPosition.YCoord
            };
            m_SessionMapEntity.SessionMapTable.Add(coreCoord);
            coreCoord.XCoord += 2000; // 200 km +
            coreCoord.YCoord += 2000;
            m_PrevPosition = coreCoord;
                
            m_SessionMapEntity.SaveChanges();
            logger.Info($"core was placed for user [id: {userId}]");

            var addedCoord = 
                m_SessionMapEntity.SessionMapTable
                .Where(obj => obj.XCoord == coreCoord.XCoord)
                .Where(obj => obj.YCoord == coreCoord.YCoord)
                .FirstOrDefault();
            if (addedCoord != null)
            {
                SessionCoresTable core = new SessionCoresTable()
                {
                    UserId = userId,
                    HPUpgrade = 0,
                    CoreMapId = addedCoord.SessionMapId,
                    AttackUpgrade = 0,
                    DefenceUpgrade = 0,
                    CapacityUpgrade = 0,
                    WarriorsNumber = 0,
                    AttackAircraftNumber = 0,
                    WorkerNumber = 3
                };
                m_SessionCoreEntity.SessionCoresTable.Add(core);

                m_SessionCoreEntity.SaveChanges();
                logger.Info($"core was created for user [id: {userId}]");
            }
            else
            {
                logger.Error("can't find added coord!");
            }
        }

    }
}
