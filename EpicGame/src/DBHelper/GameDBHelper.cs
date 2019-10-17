namespace EpicGame.src.DBHelper
{
    using System.Linq;
    using System.Collections.Generic;

    using NLog;
    using EpicGame.src.Models.Game;
    using EpicGame.src.Models.Session;
    using System.Threading;
    using System.Diagnostics;

    class GameDBHelper : System.IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly GameUnitsEntity m_GameUnitsEntity = new GameUnitsEntity();
        private readonly GameUnitsTypeEntity m_GameUnitsTypeEntity = new GameUnitsTypeEntity();
        private readonly GameBuildingsEntity m_GameBuildingsEntity = new GameBuildingsEntity();
        private readonly GameBuildingTypeEntity m_GameBuildingsTypeEntity = new GameBuildingTypeEntity();
        private readonly GameBuildingProductionEntity m_GameBuildingProductionEntity = new GameBuildingProductionEntity();

        private static readonly SessionMapEntity m_SessionMapEntity = new SessionMapEntity();
        private static readonly SessionCoresEntity m_SessionCoreEntity = new SessionCoresEntity();
        private static readonly SessionBasesEntity m_SessionBasesEntity = new SessionBasesEntity();
        private static readonly SessionCasernsEntity m_SessionCasernsEntity = new SessionCasernsEntity();
        private static readonly SessionGoldMiningsEntity m_SessionGoldMiningsEntity = new SessionGoldMiningsEntity();
        private static readonly SessionDefenceTowersEntity m_SessionDefenceTowersEntity = new SessionDefenceTowersEntity();


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
            m_SessionBasesEntity.Dispose();
            m_SessionCasernsEntity.Dispose();
            m_SessionGoldMiningsEntity.Dispose();
            m_SessionDefenceTowersEntity.Dispose();
        }

        private List<GameUnitsTable> GetAllGameUnitsList()
        {
            return m_GameUnitsEntity.GameUnitsTable.AsNoTracking().ToList();
        }

        private List<GameUnitTypeTable> GetAllGameUnitsTypeList()
        {
            return m_GameUnitsTypeEntity.GameUnitTypeTable.AsNoTracking().ToList();
        }

        private List<GameBuildingsTable> GetAllGameBuildingsList()
        {
            return m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking().ToList();
        }

        private List<GameBuildingTypeTable> GetAllGameBuildingsTypeList()
        {
            return m_GameBuildingsTypeEntity.GameBuildingTypeTable.AsNoTracking().ToList();
        }

        private List<GameBuildingProductionTable> GetAllGameBuildingProductionList()
        {
            return m_GameBuildingProductionEntity.GameBuildingProductionTable.AsNoTracking().ToList();
        }

        public List<SessionCoresTable> GetAllCores()
        {
            return m_SessionCoreEntity.SessionCoresTable.AsNoTracking().ToList();
        }

        private static SessionCoresTable GetCoreByUserId(int userId)
        {
            return m_SessionCoreEntity.SessionCoresTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .FirstOrDefault();
        }

        public SessionCoresTable GetCoreById(int coreId)
        {
            return m_SessionCoreEntity
                .SessionCoresTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
        }

        public int GetNumberOfCoreWarriors(int coreId)
        {
            return m_SessionCasernsEntity
                .SessionCasernsTable
                .AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .Select(obj => obj.WarriorsNumber)
                .FirstOrDefault();
        }
        public int GetNumberOfCoreAttackAircraft(int coreId)
        {
            return m_SessionCasernsEntity
                .SessionCasernsTable
                .AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .Select(obj => obj.AttackAircraftNumber)
                .FirstOrDefault();
        }

        public GameUnitsTable GetUnitProperty(string unitName)
        {
            return m_GameUnitsEntity
                .GameUnitsTable
                .AsNoTracking()
                .Where(obj => obj.GameUnitName.Equals(unitName))
                .FirstOrDefault();
        }

        public SessionMapTable FindCoreMapByMapId(int mapId)
        {
            return m_SessionMapEntity
                .SessionMapTable
                .AsNoTracking()
                .Where(obj => obj.SessionMapId == mapId)
                .FirstOrDefault();
        }

        private static SessionMapTable m_PrevPosition = new SessionMapTable()
        {
            XCoord = (decimal) 500 * 7_000_000,
            YCoord = (decimal)-500 * 7_000_000
        };

        //work in progress
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
                    CoreMapId = addedCoord.SessionMapId,
                    Money = 1000
                };
                m_SessionCoreEntity.SessionCoresTable.Add(core);
                m_SessionCoreEntity.SaveChanges();
                logger.Info($"core was created for user [id: {userId}]");

                SessionBasesTable @base = new SessionBasesTable()
                {
                    SessionCoreId = GetCoreByUserId(userId).SessionCoreId,
                    UniqueUpgrade = 0,
                    WorkersNumber = 3,
                    BuildingLevel = 1,
                    AttackUpgrade = 0,
                    DefenceUpgrade = 0
                };
                m_SessionBasesEntity.SessionBasesTable.Add(@base);
                m_SessionBasesEntity.SaveChanges();
                logger.Info($"base was created for user [id: {userId}]");
            }
            else
            {
                logger.Error("can't find added coord!");
            }
        }

        public void CoreBuildCasern(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base.WorkersNumber > 0)
            {
                --@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
                while (seconds < 1 * 60)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                m_SessionCasernsEntity.SessionCasernsTable.Add(
                    new SessionCasernsTable()
                    {
                        SessionCoreId = coreId,
                        WarriorsNumber = 0,
                        BuildingLevel = 1,
                        AttackAircraftNumber = 0,
                        CapacityUpgrade = 0,
                        UniqueUpgrade = 0
                    });
                m_SessionCasernsEntity.SaveChanges();
                ++@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
            }
        }

        public void CoreBuildGoldMining(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base.WorkersNumber > 0)
            {
                --@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
                while (seconds < 1 * 60)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                m_SessionGoldMiningsEntity
                    .SessionGoldMiningsTable.Add(
                    new SessionGoldMiningsTable()
                    {
                        SessionCoreId = coreId,
                        WorkersNumber = 0,
                        BuildingLevel = 1,
                        CapacityUpgrade = 0,
                        UniqueUpgrade = 0
                    });
                m_SessionGoldMiningsEntity.SaveChanges();
                ++@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
            }
        }

        public void CoreBuildDefenceTower(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base.WorkersNumber > 0)
            {
                --@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
                while (seconds < 1 * 60)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                m_SessionDefenceTowersEntity
                    .SessionDefenceTowersTable.Add(
                    new SessionDefenceTowersTable()
                    {
                        SessionCoreId = coreId,
                        BuildingLevel = 1,
                        AttackUpgrade = 0,
                        DefenceUpgrade = 0,
                        UniqueUpgrade = 0
                    });
                m_SessionDefenceTowersEntity.SaveChanges();
                ++@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
            }
        }

        public void BaseProduceWorker(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            var baseBuilding = m_GameBuildingsEntity
                .GameBuildingsTable
                .AsNoTracking()
                .Where(obj => obj.GameBuildingName == "Base")
                .FirstOrDefault();
            if (@base.WorkersNumber < (@base.CapacityUpgrade + 1) * baseBuilding.Capacity)
            {
                while (seconds < 1 * 15)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                ++@base.WorkersNumber;
                m_SessionBasesEntity.SaveChanges();
            }
        }
        
        public void CasernProduceWarrior(int coreId)
        {
            int seconds = 0;
            var casern =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            var casernBuilding = m_GameBuildingsEntity
                .GameBuildingsTable
                .AsNoTracking()
                .Where(obj => obj.GameBuildingName == "Casern")
                .FirstOrDefault();
            if ((casern.WarriorsNumber + casern.AttackAircraftNumber) < casernBuilding.Capacity)
            {
                while (seconds < 1 * 15)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                ++casern.WarriorsNumber;
                m_SessionCasernsEntity.SaveChanges();
            }
        }

        public void CasernProduceAttackAircraft(int coreId)
        {
            int seconds = 0;
            var casern =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            var casernBuilding = m_GameBuildingsEntity
                .GameBuildingsTable
                .AsNoTracking()
                .Where(obj => obj.GameBuildingName == "Casern")
                .FirstOrDefault();
            if ((casern.WarriorsNumber + casern.AttackAircraftNumber) < casernBuilding.Capacity)
            {
                while (seconds < 1 * 15)
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                ++casern.AttackAircraftNumber;
                m_SessionCasernsEntity.SaveChanges();
            }
        }

        public void GoldMiningAddWorker(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            var goldMining =
                m_SessionGoldMiningsEntity.SessionGoldMiningsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            var goldMiningBuilding = m_GameBuildingsEntity
                .GameBuildingsTable
                .AsNoTracking()
                .Where(obj => obj.GameBuildingName == "GoldMining")
                .FirstOrDefault();
            if (@base.WorkersNumber > 0)
            {
                if (goldMining.WorkersNumber < goldMiningBuilding.Capacity)
                {
                    --@base.WorkersNumber;
                    while (seconds < 1 * 15)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    ++goldMining.WorkersNumber;
                    m_SessionGoldMiningsEntity.SaveChanges();
                    m_SessionBasesEntity.SaveChanges();
                }
                else
                {
                    logger.Warn($"goldMining.WorkersNumber [coreId: {coreId}] < goldMiningBuilding.Capacity");
                }
            }
            else
            {
                logger.Warn($"@base.WorkersNumber [coreId: {coreId}] < 0");
            }
        }

    }
}
