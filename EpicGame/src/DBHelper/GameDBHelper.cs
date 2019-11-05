namespace EpicGame.src.DBHelper
{
    using NLog;
    using System.Linq;
    using System.Threading;
    using System.Diagnostics;
    using System.Collections.Generic;

    using EpicGameCommon;
    using EpicGame.src.Models.Game;
    using EpicGame.src.Models.Session;
    using EpicGameCommon.Response;
    using EpicGame.Game;
    using EpicGame.src.Models.User;

    class GameDBHelper : System.IDisposable
    {
        public bool NeedDispose { get; set; } = false;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private UserEntity m_UserContext = new UserEntity();

        private GameUnitsEntity m_GameUnitsEntity = new GameUnitsEntity();
        private GameUnitsTypeEntity m_GameUnitsTypeEntity = new GameUnitsTypeEntity();
        private GameBuildingsEntity m_GameBuildingsEntity = new GameBuildingsEntity();
        private GameBuildingTypeEntity m_GameBuildingsTypeEntity = new GameBuildingTypeEntity();
        private GameBuildingProductionEntity m_GameBuildingProductionEntity = new GameBuildingProductionEntity();

        private static SessionMapEntity m_SessionMapEntity = new SessionMapEntity();
        private static SessionCoresEntity m_SessionCoreEntity = new SessionCoresEntity();
        private static SessionBasesEntity m_SessionBasesEntity = new SessionBasesEntity();
        private static SessionCasernsEntity m_SessionCasernsEntity = new SessionCasernsEntity();
        private static SessionGoldMiningsEntity m_SessionGoldMiningsEntity = new SessionGoldMiningsEntity();
        private static SessionDefenceTowersEntity m_SessionDefenceTowersEntity = new SessionDefenceTowersEntity();


        public GameDBHelper()
        {
            logger.Warn("Create GameDBHelper instance");
        }

        public void Dispose()
        {
            logger.Warn("Dispose call");
            if (NeedDispose)
            {
                logger.Warn("Disposing context.".ToUpper());
                NLog.LogManager.Shutdown();

                m_UserContext.Dispose();

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

        public int CasernGetNumberOfWarriors(int coreId)
        {
            return m_SessionCasernsEntity
                .SessionCasernsTable
                .AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .Select(obj => obj.WarriorsNumber)
                .FirstOrDefault();
        }

        public int CasernGetNumberOfAttackAircraft(int coreId)
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

        public string FindCoreMapByMapIdAsNoTracking(int mapId)
        {
            return m_SessionMapEntity
                .SessionMapTable
                .AsNoTracking()
                .Where(obj => obj.SessionMapId == mapId)
                .FirstOrDefault()
                .ToJson();
        }

        //work in progress
        public bool GenerateCoreForUser(int userId)
        {
            //Map {0, 0} - center of universe
            //1 EGM == 100m
            //7 000 000 EGM radiuse of sun (ROS)
            //1 2 3 4 of universe
            //4 x > 5 * ROS && y < 5 * -ROS 

            var maplist = m_SessionMapEntity.SessionMapTable
                .AsNoTracking().ToList();
            SessionMapTable coreCoord;
            if (maplist.Count == 0)
            {
                coreCoord = new SessionMapTable()
                {
                    XCoord = (decimal) 500 * 7_000_000,
                    YCoord = (decimal)-500 * 7_000_000
                };
                m_SessionMapEntity.SessionMapTable.Add(coreCoord);
            }
            else
            {
                var map = maplist.OrderBy(obj => obj.SessionMapId).LastOrDefault();
                coreCoord = new SessionMapTable()
                {
                    XCoord = map.XCoord + 2000,
                    YCoord = map.YCoord + 2000
                };
                m_SessionMapEntity.SessionMapTable.Add(coreCoord);
            }
                
            m_SessionMapEntity.SaveChanges();
            logger.Info($"core was placed for user [id: {userId}]");

            var addedCoord = 
                m_SessionMapEntity.SessionMapTable.AsNoTracking()
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
                    SessionCoreId = GetCoreByUserId(userId).FromJson<SessionCoresTable>().SessionCoreId,
                    UniqueUpgrade = 0,
                    WorkersNumber = 3,
                    BuildingLevel = 1,
                    AttackUpgrade = 0,
                    DefenceUpgrade = 0
                };
                m_SessionBasesEntity.SessionBasesTable.Add(@base);
                m_SessionBasesEntity.SaveChanges();
                logger.Info($"base was created for user [id: {userId}]");
                return true;
            }
            else
            {
                logger.Error("can't find added coord!");
                return false;
            }
        }

        public void CoreBuildCasern(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base != null)
            {
                if (@base.WorkersNumber > 0)
                {
                    if (m_SessionCasernsEntity
                        .SessionCasernsTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault() == null)
                    {
                        --@base.WorkersNumber;
                        m_SessionBasesEntity.SaveChanges();
                        while (seconds < 1 * 1)
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

                        logger.Info("[call] CoreBuildCasern");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceSuccess,
                            $"Build casern for core [{nick}]");
                    }
                    else
                    {
                        logger.Warn($"Casern already build for core [coreId: {coreId}]");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceFailure,
                           $"Casern already build for core [{nick}]");
                    }
                }
            }
            else
            {
                logger.Warn("base == null");
            }
        }

        public void CoreBuildGoldMining(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base != null)
            {
                if (@base.WorkersNumber > 0)
                {
                    if (m_SessionGoldMiningsEntity
                        .SessionGoldMiningsTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault() == null)
                    {
                        --@base.WorkersNumber;
                        m_SessionBasesEntity.SaveChanges();
                        while (seconds < 1 * 1)
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

                        logger.Info("[call] CoreBuildGoldMining");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceSuccess,
                            $"Build gold mining for core [{nick}]");
                    }
                    else
                    {
                        logger.Warn($"GoldMining already build for core [coreId: {coreId}]");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceFailure,
                           $"Gold mining already build for core [{nick}]");
                    }
                }
            }
            else
            {
                logger.Warn("base == null");
            }
        }

        public void CoreBuildDefenceTower(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base != null)
            {
                if (@base.WorkersNumber > 0)
                {
                    if (m_SessionDefenceTowersEntity
                        .SessionDefenceTowersTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .ToArray().Length == 0)
                    {
                        --@base.WorkersNumber;
                        m_SessionBasesEntity.SaveChanges();
                        while (seconds < 1 * 1)
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

                        logger.Info("[call] CoreBuildDefenceTower");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceSuccess,
                            $"Build defence tower for core [{nick}]");
                    }
                    else
                    {
                        logger.Warn($"DefenceTower already build for core [coreId: {coreId}]");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                           LogType.ProduceFailure,
                           $"Defence tower already build for core [{nick}]");
                    }
                }
            }
            else
            {
                logger.Warn("base == null");
            }
        }

        public void BaseProduceWorker(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base != null)
            {
                var baseBuilding = m_GameBuildingsEntity
                    .GameBuildingsTable
                    .AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Base")
                    .FirstOrDefault();
                if (@base.WorkersNumber < (@base.CapacityUpgrade + 1) * baseBuilding.Capacity)
                {
                    while (seconds < 1 * 1)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    ++@base.WorkersNumber;
                    m_SessionBasesEntity.SaveChanges();

                    logger.Info("[call] BaseProduceWorker");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceSuccess,
                        $"Produce new worker [{nick}] |{@base.WorkersNumber}|");
                }
                else
                {
                    logger.Warn($"Workers capacity limit exceeded for core !!! [coreId: {coreId}]");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceFailure,
                        $"Workers capacity limit exceeded for core !!! [nick: {nick}, coreId: {coreId}]");
                }
            }
            else
            {
                logger.Warn($"core [id:{coreId}] is not exist");
            }
        }
        
        public void CasernProduceWarrior(int coreId)
        {
            int seconds = 0;
            var casern =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (casern != null)
            {
                var casernBuilding = m_GameBuildingsEntity
                .GameBuildingsTable
                .AsNoTracking()
                .Where(obj => obj.GameBuildingName == "Casern")
                .FirstOrDefault();
                if ((casern.WarriorsNumber + casern.AttackAircraftNumber) < casernBuilding.Capacity)
                {
                    while (seconds < 1 * 1)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    ++casern.WarriorsNumber;
                    m_SessionCasernsEntity.SaveChanges();

                    logger.Info("[call] CasernProduceWarrior");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceSuccess,
                        $"Produce new warrior [{nick}] |{casern.WarriorsNumber}|");
                }
                else
                {
                    logger.Warn($"Warriors capacity limit exceeded for core !!! [coreId: {coreId}]");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceFailure,
                        $"Warriors capacity limit exceeded for core !!! [nick: {nick}, coreId: {coreId}]");
                }
            }
            else
            {
                logger.Warn($"core [id:{coreId}] is not exist");
            }
        }

        public void CasernProduceAttackAircraft(int coreId)
        {
            int seconds = 0;
            var casern =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (casern != null)
            {
                var casernBuilding = m_GameBuildingsEntity
                    .GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Casern")
                    .FirstOrDefault();
                if ((casern.WarriorsNumber + casern.AttackAircraftNumber) < casernBuilding.Capacity)
                {
                    while (seconds < 1 * 1)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    ++casern.AttackAircraftNumber;
                    m_SessionCasernsEntity.SaveChanges();

                    logger.Info("[call] CasernProduceAttackAircraft");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceSuccess,
                       $"Produce new attack aircraft [{nick}] |{casern.AttackAircraftNumber}|");
                }
                else
                {
                    logger.Warn($"AttackAircraft capacity limit exceeded for core !!! [coreId: {coreId}]");

                    int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                    string nick = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == userId)
                        .FirstOrDefault()?.Nickname;
                    if (userId <= 0)
                    {
                        logger.Error($"UserId [{userId}] <= 0");
                    }
                    else
                    {
                        logger.Info($"UserId [{userId}]");
                    }
                    EventLogger.AddLogForUser(userId,
                        LogType.ProduceFailure,
                       $"AttackAircraft capacity limit exceeded for core !!! [nick: {nick}] |{casern.AttackAircraftNumber}|");
                }
            }
            else
            {
                logger.Warn($"core [id:{coreId}] is not exist");
            }
        }

        public void GoldMiningAddWorker(int coreId)
        {
            int seconds = 0;
            var @base =
                m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (@base != null)
            {
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
                        while (seconds < 1 * 1)
                        {
                            Thread.Sleep(1000);
                            seconds++;
                        }
                        ++goldMining.WorkersNumber;
                        m_SessionGoldMiningsEntity.SaveChanges();
                        m_SessionBasesEntity.SaveChanges();

                        logger.Info("[call] GoldMiningAddWorker");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;

                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }

                        EventLogger.AddLogForUser(userId,
                            LogType.ProduceSuccess,
                            $"Add new worker to gold mining [{nick}] |{goldMining.WorkersNumber}|");
                    }
                    else
                    {
                        logger.Warn($"goldMining.WorkersNumber [coreId: {coreId}] < goldMiningBuilding.Capacity");

                        int userId = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                        .Where(obj => obj.SessionCoreId == coreId)
                        .FirstOrDefault().UserId;
                        string nick = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == userId)
                            .FirstOrDefault()?.Nickname;
                        if (userId <= 0)
                        {
                            logger.Error($"UserId [{userId}] <= 0");
                        }
                        else
                        {
                            logger.Info($"UserId [{userId}]");
                        }
                        EventLogger.AddLogForUser(userId,
                           LogType.ProduceFailure,
                           $"Gold mining capacity exceeded [{nick}] |{goldMining.WorkersNumber}|");
                    }
                }
                else
                {
                    logger.Warn($"@base.WorkersNumber [coreId: {coreId}] < 0");
                }
            }
            else
            {
                logger.Warn($"core [id:{coreId}] is not exist");
            }
        }

        public string GetCoreInfoById(int coreId)
        {
            var coreSession = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (coreSession != null)
            {
                var baseSession = m_SessionBasesEntity.SessionBasesTable.AsNoTracking()
                    .Where(obj => obj.SessionCoreId == coreId)
                    .FirstOrDefault();
                var baseGame = m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Base")
                    .FirstOrDefault();

                var casernSession = m_SessionCasernsEntity.SessionCasernsTable.AsNoTracking()
                    .Where(obj => obj.SessionCoreId == coreId)
                    .FirstOrDefault();
                var casernGame = m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Casern")
                    .FirstOrDefault();

                var goldMiningSession = m_SessionGoldMiningsEntity.SessionGoldMiningsTable.AsNoTracking()
                    .Where(obj => obj.SessionCoreId == coreId)
                    .FirstOrDefault();
                var goldMiningGame = m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "GoldMining")
                    .FirstOrDefault();

                var defenceTowerSession = m_SessionDefenceTowersEntity.SessionDefenceTowersTable.AsNoTracking()
                    .Where(obj => obj.SessionCoreId == coreId)
                    .FirstOrDefault();
                var defenceTowerGame = m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "DefenceTower")
                    .FirstOrDefault();

                if (baseGame != null)
                {
                    if (casernGame != null)
                    {
                        if (goldMiningGame != null)
                        {
                            if (defenceTowerGame != null)
                            {
                                CoreInfo coreInfo = new CoreInfo()
                                {
                                    //already checked
                                    Money = coreSession.Money, 

                                    CoreId = coreId,
                                    CoreMapId = coreSession.CoreMapId,
                                    
                                    BaseHp = baseGame.GameBuildingHP,
                                    BaseLevel = (baseSession != null) ? baseSession.BuildingLevel : 0,
                                    BaseCapacity = (baseSession != null) ?
                                       baseGame.Capacity * (baseSession.CapacityUpgrade + 1) :
                                       baseGame.Capacity,
                                    BaseAttack = (baseSession != null) ?
                                       baseGame.GameBuildingAttack * (baseSession.AttackUpgrade + 1) :
                                       baseGame.GameBuildingAttack,
                                    BaseDefence = baseGame.GameBuildingDefence,
                                    BaseWorkersCount = (baseSession != null) ? baseSession.WorkersNumber : 0,
                                    BaseType = baseGame.GameBuildingType,
                                    BaseIncome = baseGame.GameBuildingGoldIncome,
                                    BaseOutcome = baseGame.GameBuildingGoldOutcome,
                                    NumberOfWorkersInBase = (baseSession != null) ?
                                       baseSession.WorkersNumber :
                                       0,
                                    
                                    Casern = (casernSession != null) ? 1 : 0,
                                    GoldMining = (goldMiningSession != null) ? 1 : 0,
                                    DefenceTower = (defenceTowerSession != null) ? 1 : 0,
                                    
                                    CasernLevel = (casernSession != null) ?
                                       casernSession.BuildingLevel :
                                       0,
                                    CasernCapacity = (casernSession != null) ?
                                       (casernGame.Capacity * (casernSession.CapacityUpgrade + 1)) :
                                       casernGame.Capacity,
                                    CasernHp = casernGame.GameBuildingHP,
                                    CasernAttack = casernGame.GameBuildingAttack,
                                    CasernDefence = casernGame.GameBuildingDefence,
                                    CasernWarriorsCount = (casernSession != null) ? casernSession.WarriorsNumber : 0,
                                    CasernAttackAircraftsCount = (casernSession != null) ? casernSession.AttackAircraftNumber : 0,
                                    CasernType = casernGame.GameBuildingType,
                                    CasernIncome = casernGame.GameBuildingGoldIncome,
                                    CasernOutcome = casernGame.GameBuildingGoldOutcome,
                                    
                                    DefenceTowerLevel = (defenceTowerSession != null) ?
                                        defenceTowerSession.BuildingLevel : 0,
                                    DefenceTowerCapacity = (defenceTowerSession != null) ?
                                       defenceTowerGame.Capacity * (defenceTowerSession.DefenceUpgrade + 1) :
                                       defenceTowerGame.Capacity,
                                    DefenceTowerHp = defenceTowerGame.GameBuildingHP,
                                    DefenceTowerAttack = defenceTowerGame.GameBuildingAttack,
                                    DefenceTowerDefence = defenceTowerGame.GameBuildingDefence,
                                    DefenceTowerType = defenceTowerGame.GameBuildingType,
                                    NumberOfDefenceTower = (defenceTowerSession != null) ? 1 : 0,
                                    
                                    GoldMiningLevel = (goldMiningSession != null) ? goldMiningSession.BuildingLevel : 0,
                                    GoldMiningCapacity = (goldMiningSession != null) ?
                                       (goldMiningGame.Capacity * (casernSession.CapacityUpgrade + 1)) : 
                                       goldMiningGame.Capacity,
                                    GoldMiningHp = goldMiningGame.GameBuildingHP,
                                    GoldMiningAttack = goldMiningGame.GameBuildingAttack,
                                    GoldMiningDefence = goldMiningGame.GameBuildingDefence,
                                    GoldMiningType = goldMiningGame.GameBuildingType,
                                    GoldMiningIncome = goldMiningGame.GameBuildingGoldIncome,
                                    GoldMiningOutcome = goldMiningGame.GameBuildingGoldOutcome,
                                    GoldMiningNumberOfWorkers = (goldMiningSession != null) ? goldMiningSession.WorkersNumber : 0
                                };
                                string coreInfoJson = coreInfo.ToJson();
                                logger.Info($"CORE INFO SEND: {coreInfoJson}");
                                return coreInfoJson;
                            }
                            else
                            {
                                logger.Error($"defenceTowerGame with core id: {coreId} doesnt exist");
                                return new CoreInfo().ToJson();
                            }
                        }
                        else
                        {
                            logger.Error($"goldMiningGame with core id: {coreId} doesnt exist");
                            return new CoreInfo().ToJson();
                        }
                    }
                    else
                    {
                        logger.Error($"casernGame with core id: {coreId} doesnt exist");
                        return new CoreInfo().ToJson();
                    }
                }
                else
                {
                    logger.Error($"baseGame with core id: {coreId} doesnt exist");
                    return new CoreInfo().ToJson();
                }
            }
            else
            {
                logger.Error($"CoreSession with id: {coreId} doesnt exist");
                return new CoreInfo().ToJson();
            }
        }

        public string GetAllCores()
        {
            logger.Info("[call] GetAllCores");
            return m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                .ToList().ToJson();
        }

        public string GetCoreByUserId(int userId)
        {
            logger.Info("[call] GetCoreByUserId");
            return m_SessionCoreEntity.SessionCoresTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .FirstOrDefault()
                .ToJson();
        }

        public string GetCoreById(int coreId)
        {
            logger.Info("[call] GetCoreById");
            return m_SessionCoreEntity
                .SessionCoresTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault()
                .ToJson();
        }

        public string GetCoreByIdAsNoTracking(int coreId)
        {
            logger.Info("[call] GetCoreById");
            return m_SessionCoreEntity
                .SessionCoresTable.AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault()
                .ToJson();
        }

        private List<GameUnitsTable> GetCoreArmy(int coreId)
        {
            var result = new List<GameUnitsTable>();
            var warriorUnit = GetUnitProperty("Warrior");
            var attackAircraftUnit = GetUnitProperty("AttackAircraft");

            var numberWarriors = CasernGetNumberOfWarriors(coreId);
            var numberAttackAircraft = CasernGetNumberOfAttackAircraft(coreId);

            for (int i = 0; i < numberWarriors; i++)
            {
                result.Add(
                    new GameUnitsTable()
                    {
                        GameUnitId = warriorUnit.GameUnitId,
                        GameUnitType = warriorUnit.GameUnitType,
                        GameUnitName = warriorUnit.GameUnitName,
                        GameUnitHP = warriorUnit.GameUnitHP,
                        GameUnitAttack = warriorUnit.GameUnitAttack,
                        GameUnitDefence = warriorUnit.GameUnitDefence,
                        GameUnitGoldIncome = warriorUnit.GameUnitGoldIncome,
                        GameUnitGoldOutcome = warriorUnit.GameUnitGoldOutcome
                    });
            }
            for (int i = 0; i < numberAttackAircraft; i++)
            {
                result.Add(new GameUnitsTable()
                {
                    GameUnitId = attackAircraftUnit.GameUnitId,
                    GameUnitType = attackAircraftUnit.GameUnitType,
                    GameUnitName = attackAircraftUnit.GameUnitName,
                    GameUnitHP = attackAircraftUnit.GameUnitHP,
                    GameUnitAttack = attackAircraftUnit.GameUnitAttack,
                    GameUnitDefence = attackAircraftUnit.GameUnitDefence,
                    GameUnitGoldIncome = attackAircraftUnit.GameUnitGoldIncome,
                    GameUnitGoldOutcome = attackAircraftUnit.GameUnitGoldOutcome
                });
            }

            return result;
        }

        private List<GameBuildingsTable> GetCoreBuildings(int coreId)
        {
            var result = new List<GameBuildingsTable>();
            var defenceTowerBuilding =
                m_GameBuildingsEntity.GameBuildingsTable
                .Where(obj => obj.GameBuildingName == "DefenceTower")
                .FirstOrDefault();
            var defenceTowerCount =
                m_SessionDefenceTowersEntity.SessionDefenceTowersTable
                .Where(obj => obj.SessionCoreId == coreId)
                .ToList().Count;

            var baseBuilding =
                m_GameBuildingsEntity.GameBuildingsTable
                .Where(obj => obj.GameBuildingName == "Base")
                .FirstOrDefault();

            var warriorUnit = GetUnitProperty("Warrior");
            var attackAircraftUnit = GetUnitProperty("AttackAircraft");

            var numberWarriors = CasernGetNumberOfWarriors(coreId);
            var numberAttackAircraft = CasernGetNumberOfAttackAircraft(coreId);

            for (int i = 0; i < defenceTowerCount; i++)
            {
                result.Add(
                    new GameBuildingsTable()
                    {
                        GameBuildingId = defenceTowerBuilding.GameBuildingId,
                        GameBuildingType = defenceTowerBuilding.GameBuildingType,
                        GameBuildingName = "DefenceTower",
                        GameBuildingHP = defenceTowerBuilding.GameBuildingHP,
                        GameBuildingAttack = defenceTowerBuilding.GameBuildingAttack,
                        GameBuildingDefence = defenceTowerBuilding.GameBuildingDefence,
                        GameBuildingGoldIncome = defenceTowerBuilding.GameBuildingGoldIncome,
                        GameBuildingGoldOutcome = defenceTowerBuilding.GameBuildingGoldOutcome,
                        Capacity = defenceTowerBuilding.Capacity
                    });
            }

            for (int i = 0; i < numberAttackAircraft; i++)
            {
                result.Add(new GameBuildingsTable()
                {
                    GameBuildingId = baseBuilding.GameBuildingId,
                    GameBuildingType = baseBuilding.GameBuildingType,
                    GameBuildingName = "Base",
                    GameBuildingHP = baseBuilding.GameBuildingHP,
                    GameBuildingAttack = baseBuilding.GameBuildingAttack,
                    GameBuildingDefence = baseBuilding.GameBuildingDefence,
                    GameBuildingGoldIncome = baseBuilding.GameBuildingGoldIncome,
                    GameBuildingGoldOutcome = baseBuilding.GameBuildingGoldOutcome,
                    Capacity = baseBuilding.Capacity
                });
            }

            return result;
        }

        //TODO: у пользователя может и не быть casern
        public BattleResponse DuelBattle(int attackerCoreId, int defenderCoreId)
        {
            var attackers = GetCoreArmy(attackerCoreId);
            var defenders = GetCoreArmy(defenderCoreId);

            SessionCoresTable attackerCore = null;
            SessionCoresTable defenderCore = null;

            if (attackers.Count <= 0 ||
                defenders.Count <= 0)
            {
                logger.Warn("Attackers or Defenders have no army!");

                var attackerCoreJson = GetCoreByIdAsNoTracking(attackerCoreId);
                if (attackerCoreJson != null)
                {
                    attackerCore = attackerCoreJson.FromJson<SessionCoresTable>();
                    if (attackerCore != null)
                    {
                        EventLogger.AddLogForUser(
                            attackerCore.UserId, 
                            LogType.BattleFailure, 
                            $"You have {attackers.Count} army, your enemy have {defenders.Count} army. " +
                            "So global police can't let you to attack that player!");
                    }
                    else
                    {
                        logger.Error("Attackers core == null!");
                        EventLogger.AddLogForUser(
                            attackerCore.UserId,
                            LogType.BattleFailure,
                            $"You have no core!");
                    }
                }
                else
                {
                    logger.Error(
                        $"{attackerCoreId} doesnt exists!");
                }

                var defenderCoreJson = GetCoreByIdAsNoTracking(defenderCoreId);
                if (defenderCoreJson != "null")
                {
                    defenderCore = defenderCoreJson.FromJson<SessionCoresTable>();
                    if (defenderCore != null)
                    {
                        EventLogger.AddLogForUser(
                            defenderCore.UserId,
                            LogType.BattleFailure,
                            $"Someone trying to attack you but " +
                                ((attackers.Count <= 0) ? "attackers have no army" : "you have no army!") +
                            "So global police can't let them to attack!");
                    }
                    else
                    {
                        logger.Error("Defender core == null!");
                        EventLogger.AddLogForUser(
                            defenderCore.UserId,
                            LogType.BattleFailure,
                            $"Defender have no core!");
                    }
                }
                else
                {
                    logger.Error(
                        $"{defenderCoreId} doesnt exists!");
                }
            }

            var result = 
                GameEngine.DuelBattle(ref attackers, ref defenders);

            var attackersCasern = 
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == attackerCoreId)
                .FirstOrDefault();
            if (attackersCasern != null)
            {
                int attackWarriorCount = 0;
                int attackAircraftCount = 0;
                for (var i = 0; i < attackers.Count; i++)
                {
                    if (attackers[i].GameUnitName == "Warrior")
                    {
                        ++attackWarriorCount;
                    }
                    else if (attackers[i].GameUnitName == "AttackAircraft")
                    {
                        ++attackAircraftCount;
                    }
                }

                attackersCasern.WarriorsNumber = attackWarriorCount;
                attackersCasern.AttackAircraftNumber = attackAircraftCount;
                logger.Info($"Casern [coreId: {attackerCoreId}] warriors & attackaircraft number updated!");

                if (attackerCore != null)
                {
                    EventLogger.AddLogForUser(
                          attackerCore.UserId, LogType.BattleFailure,
                          $"Casern warriors & attackaircraft number updated!");
                }
            }
            else
            {
                logger.Error($"Attackers don't have casern!");

                EventLogger.AddLogForUser(
                            attackerCoreId,
                            LogType.BattleFailure,
                            $"You have no core!");
            }

            var defendersCasern =
               m_SessionCasernsEntity.SessionCasernsTable
               .Where(obj => obj.SessionCoreId == defenderCoreId)
               .FirstOrDefault();
            if (defendersCasern != null)
            {
                int defendersWarriorNumber = 0;
                int defendersAircraftCount = 0;
                for (var i = 0; i < attackers.Count; i++)
                {
                    if (attackers[i].GameUnitName == "Warrior")
                    {
                        ++defendersWarriorNumber;
                    }
                    else if (attackers[i].GameUnitName == "AttackAircraft")
                    {
                        ++defendersAircraftCount;
                    }
                }

                defendersCasern.WarriorsNumber = defendersWarriorNumber;
                defendersCasern.AttackAircraftNumber = defendersAircraftCount;
                logger.Info($"Casern [coreId: {attackerCoreId}] warriors & attackaircraft number updated!");

                if (defenderCore != null)
                {
                    EventLogger.AddLogForUser(
                          defenderCore.UserId, LogType.BattleFailure,
                          $"Casern warriors & attackaircraft number updated!");
                }
            }
            else
            {
                logger.Error($"Defenders don't have casern!");
            }

            m_SessionCasernsEntity.SaveChanges();

            return result;
        }

        public BattleResponse CoreBattle(int attackerCoreId, int defenderCoreId)
        {
            var attackers = GetCoreArmy(attackerCoreId);
            var defenders = GetCoreArmy(defenderCoreId);
            var defendersBuildings = GetCoreBuildings(defenderCoreId);

            var result =
                GameEngine.CoreBattle(ref attackers, ref defenders, ref defendersBuildings);

            int attackWarriorCount = 0;
            int attackAircraftCount = 0;
            for (var i = 0; i < attackers.Count; i++)
            {
                if (attackers[i].GameUnitName == "Warrior")
                {
                    ++attackWarriorCount;
                }
                else if (attackers[i].GameUnitName == "AttackAircraft")
                {
                    ++attackAircraftCount;
                }
            }

            var attackersCasern =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == attackerCoreId)
                .FirstOrDefault();
            attackersCasern.WarriorsNumber = attackWarriorCount;
            attackersCasern.AttackAircraftNumber = attackAircraftCount;
            logger.Info($"Casern [coreId: {attackerCoreId}] warriors & attackaircraft number updated!");

            int defendersWarriorNumber = 0;
            int defendersAircraftCount = 0;
            for (var i = 0; i < attackers.Count; i++)
            {
                if (attackers[i].GameUnitName == "Warrior")
                {
                    ++defendersWarriorNumber;
                }
                else if (attackers[i].GameUnitName == "AttackAircraft")
                {
                    ++defendersAircraftCount;
                }
            }

            for (var i = 0; i < defenders.Count; i++)
            {
                if (defenders[i].GameUnitName == "Warrior")
                {
                    ++defendersWarriorNumber;
                }
                else if (defenders[i].GameUnitName == "AttackAircraft")
                {
                    ++defendersAircraftCount;
                }
            }
            var defendersCasern =
               m_SessionCasernsEntity.SessionCasernsTable
               .Where(obj => obj.SessionCoreId == defenderCoreId)
               .FirstOrDefault();
            defendersCasern.WarriorsNumber = defendersWarriorNumber;
            defendersCasern.AttackAircraftNumber = defendersAircraftCount;
            logger.Info($"Casern [coreId: {attackerCoreId}] warriors & attackaircraft number updated!");

            m_SessionCasernsEntity.SaveChanges();

            int defenderDefenceTowerCount = 0; 
            for (var i = 0; i < defendersBuildings.Count; i++)
            {
                if (defendersBuildings[i].GameBuildingName == "DefenceTower")
                {
                    ++defenderDefenceTowerCount;
                }
            }
            var listOfTowerDefence = 
                m_SessionDefenceTowersEntity.SessionDefenceTowersTable
                .Where(obj => obj.SessionCoreId == defenderCoreId)
                .ToList();
            while (listOfTowerDefence.Count > defenderDefenceTowerCount)
            {
                listOfTowerDefence.Remove(listOfTowerDefence[0]);
            }
            m_SessionDefenceTowersEntity.SaveChanges();

            return result;
        }
    }
}
