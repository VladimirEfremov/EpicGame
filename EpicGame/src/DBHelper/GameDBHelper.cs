﻿namespace EpicGame.src.DBHelper
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
    using EpicGameCommon.Models;

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
        private static SessionStatisticEntity m_SessionStatisticEntity = new SessionStatisticEntity();
        //private static Dictionary<int, Renderable[]> m_RenverableCache = new Dictionary<int, Renderable[]>();

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
            var casern = m_SessionCasernsEntity.SessionCasernsTable.AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (casern != null)
            {
                return casern.WarriorsNumber;
            }
            return 0;
        }

        public int CasernGetNumberOfAttackAircraft(int coreId)
        {
            var casern = m_SessionCasernsEntity.SessionCasernsTable.AsNoTracking()
                 .Where(obj => obj.SessionCoreId == coreId)
                 .FirstOrDefault();
            if (casern != null)
            {
                return casern.AttackAircraftNumber;
            }
            return 0;
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
            return m_SessionMapEntity.SessionMapTable.AsNoTracking()
                .Where(obj => obj.SessionMapId == mapId)
                .FirstOrDefault()
                .ToJson();
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
                if (@base.WorkersNumber < (@base.CapacityUpgrade + 1) * 
                        (baseBuilding.Capacity*(@base.CapacityUpgrade + 1)))
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
                var casernBuilding = m_GameBuildingsEntity.GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Casern")
                    .FirstOrDefault(); 

                if ((casern.WarriorsNumber + casern.AttackAircraftNumber) <
                        (casernBuilding.Capacity * (casern.CapacityUpgrade + 1)))
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
            var casernSession =
                m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (casernSession != null)
            {
                var casernBuilding = m_GameBuildingsEntity
                    .GameBuildingsTable.AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "Casern")
                    .FirstOrDefault();

                if ((casernSession.WarriorsNumber + casernSession.AttackAircraftNumber) < 
                        (casernBuilding.Capacity * (casernSession.CapacityUpgrade+1)))
                {
                    while (seconds < 1 * 1)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    ++casernSession.AttackAircraftNumber;
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
                       $"Produce new attack aircraft [{nick}] |{casernSession.AttackAircraftNumber}|");
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
                       $"AttackAircraft capacity limit exceeded for core !!! [nick: {nick}] |{casernSession.AttackAircraftNumber}|");
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
                var goldMiningSession =
                    m_SessionGoldMiningsEntity.SessionGoldMiningsTable
                    .Where(obj => obj.SessionCoreId == coreId)
                    .FirstOrDefault();
                if (goldMiningSession != null)
                {
                    var goldMiningBuilding = m_GameBuildingsEntity
                    .GameBuildingsTable
                    .AsNoTracking()
                    .Where(obj => obj.GameBuildingName == "GoldMining")
                    .FirstOrDefault();
                    if (@base.WorkersNumber > 0)
                    {
                        if (goldMiningSession.WorkersNumber <
                                (goldMiningBuilding.Capacity * (goldMiningSession.CapacityUpgrade + 1)))
                        {
                            --@base.WorkersNumber;
                            while (seconds < 1 * 1)
                            {
                                Thread.Sleep(1000);
                                seconds++;
                            }
                            ++goldMiningSession.WorkersNumber;
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
                                $"Add new worker to gold mining [{nick}] |{goldMiningSession.WorkersNumber}|");
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
                               $"Gold mining capacity exceeded [{nick}] |{goldMiningSession.WorkersNumber}|");
                        }
                    }
                    else
                    {
                        logger.Warn($"@base.WorkersNumber [coreId: {coreId}] < 0");
                    }
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
                                    BaseDefence = (baseSession != null) ?
                                        (baseSession.DefenceUpgrade + 1) * baseGame.GameBuildingDefence :  
                                        baseGame.GameBuildingDefence,
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
                                    DefenceTowerAttack = (defenceTowerSession != null) ? 
                                        (defenceTowerSession.AttackUpgrade+1)*defenceTowerGame.GameBuildingAttack :
                                        defenceTowerGame.GameBuildingAttack,
                                    DefenceTowerDefence = (defenceTowerSession != null) ?
                                        (defenceTowerSession.DefenceUpgrade + 1) * defenceTowerGame.GameBuildingDefence :
                                        defenceTowerGame.GameBuildingDefence,
                                    DefenceTowerType = defenceTowerGame.GameBuildingType,
                                    NumberOfDefenceTower = (defenceTowerSession != null) ? 1 : 0,
                                    
                                    GoldMiningLevel = (goldMiningSession != null) ? goldMiningSession.BuildingLevel : 0,
                                    GoldMiningCapacity = (goldMiningSession != null) ?
                                       (goldMiningGame.Capacity * (goldMiningSession.CapacityUpgrade + 1)) : 
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
                .FirstOrDefault().ToJson();
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

        private void AddStatisticForDuel(int winnerId, int loserId)
        {
            var statsList = m_SessionStatisticEntity.SessionStatisticTable?.ToList();
            if (statsList.Count > 0)
            {
                var winnerStatistic = statsList
                    .Where(obj => obj.SessionCoreId == winnerId)
                    .FirstOrDefault();
                var loserStatistic = statsList
                    .Where(obj => obj.SessionCoreId == loserId)
                    .FirstOrDefault();
                if (winnerStatistic == null)
                {
                    statsList.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = winnerId,
                        Rating = 0,
                        Wins = 1,
                        Defeats = 0,
                        SuccessfullCoreAttacks = 0,
                        NotSuccessfullCoreAttacks = 0,
                        Scores = 30
                    });
                }
                if (loserStatistic == null)
                {
                    statsList.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = loserId,
                        Rating = 0,
                        Wins = 0,
                        Defeats = 1,
                        SuccessfullCoreAttacks = 0,
                        NotSuccessfullCoreAttacks = 0,
                        Scores = 0
                    });
                }

                statsList.OrderBy(obj => obj.Rating);
                for (int i = 0; i < statsList.Count; i++)
                {
                    if (statsList[i].SessionCoreId == winnerId)
                    {
                        statsList[i].Rating = i + 1;
                    }
                    else if (statsList[i].SessionCoreId == loserId)
                    {
                        statsList[i].Rating = i + 1;
                    }
                }
                m_SessionStatisticEntity.SaveChanges();
            }
            else
            {
                m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                {
                    SessionCoreId = winnerId,
                    Rating = 1,
                    Wins = 1,
                    Defeats = 0,
                    SuccessfullCoreAttacks = 0,
                    NotSuccessfullCoreAttacks = 0,
                    Scores = 30
                });
                m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                {
                    SessionCoreId = loserId,
                    Rating = 2,
                    Wins = 0,
                    Defeats = 1,
                    SuccessfullCoreAttacks = 0,
                    NotSuccessfullCoreAttacks = 0,
                    Scores = 0
                });
                m_SessionStatisticEntity.SaveChanges();
            }
        }

        private void AddStatisticForCoreBattle(int winnerId, int loserId, int isWinnerAttacker)
        {
            var stats = m_SessionStatisticEntity.SessionStatisticTable;
            if (stats != null)
            {
                var winnerStatistic = stats
                    .Where(obj => obj.SessionCoreId == winnerId)
                    .FirstOrDefault();
                var loserStatistic = stats
                    .Where(obj => obj.SessionCoreId == loserId)
                    .FirstOrDefault();
                var statsList = stats.ToList();
                if (isWinnerAttacker == 1)
                {
                    if (winnerStatistic == null)
                    {
                        statsList.Add(new SessionStatisticTable()
                        {
                            SessionCoreId = winnerId,
                            Rating = 0,
                            Wins = 1,
                            Defeats = 0,
                            SuccessfullCoreAttacks = 1,
                            NotSuccessfullCoreAttacks = 0,
                            Scores = 45
                        });
                    }
                    if (loserStatistic == null)
                    {
                        statsList.Add(new SessionStatisticTable()
                        {
                            SessionCoreId = loserId,
                            Rating = 0,
                            Wins = 0,
                            Defeats = 1,
                            SuccessfullCoreAttacks = 0,
                            NotSuccessfullCoreAttacks = 0,
                            Scores = 0
                        });
                    }
                }
                else
                {
                    if (winnerStatistic == null)
                    {
                        statsList.Add(new SessionStatisticTable()
                        {
                            SessionCoreId = winnerId,
                            Rating = 0,
                            Wins = 0,
                            Defeats = 1,
                            SuccessfullCoreAttacks = 0,
                            NotSuccessfullCoreAttacks = 0,
                            Scores = 15
                        });
                    }
                    if (loserStatistic == null)
                    {
                        statsList.Add(new SessionStatisticTable()
                        {
                            SessionCoreId = loserId,
                            Rating = 0,
                            Wins = 0,
                            Defeats = 1,
                            SuccessfullCoreAttacks = 0,
                            NotSuccessfullCoreAttacks = 1,
                            Scores = 0
                        });
                    }
                }

                statsList.OrderBy(obj => obj.Rating);
                for (int i = 0; i < statsList.Count; i++)
                {
                    if (statsList[i].SessionCoreId == winnerId)
                    {
                        statsList[i].Rating = i + 1;
                    }
                    else if (statsList[i].SessionCoreId == loserId)
                    {
                        statsList[i].Rating = i + 1;
                    }
                }
                m_SessionStatisticEntity.SaveChanges();
            }
            else
            {
                if (isWinnerAttacker == 1)
                {
                    m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = winnerId,
                        Rating = 1,
                        Wins = 1,
                        Defeats = 0,
                        SuccessfullCoreAttacks = 1,
                        NotSuccessfullCoreAttacks = 0,
                        Scores = 45
                    });
                    m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = winnerId,
                        Rating = 2,
                        Wins = 0,
                        Defeats = 1,
                        SuccessfullCoreAttacks = 0,
                        NotSuccessfullCoreAttacks = 0,
                        Scores = 0
                    });
                }
                else
                {
                    m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = winnerId,
                        Rating = 1,
                        Wins = 1,
                        Defeats = 0,
                        SuccessfullCoreAttacks = 0,
                        NotSuccessfullCoreAttacks = 0,
                        Scores = 15
                    });
                    m_SessionStatisticEntity.SessionStatisticTable.Add(new SessionStatisticTable()
                    {
                        SessionCoreId = winnerId,
                        Rating = 2,
                        Wins = 0,
                        Defeats = 1,
                        SuccessfullCoreAttacks = 0,
                        NotSuccessfullCoreAttacks = 1,
                        Scores = 0
                    });
                }
                m_SessionStatisticEntity.SaveChanges();
            }
        }

        //TODO: у пользователя может и не быть casern
        public BattleResponse DuelBattle(int attackerCoreId, int defenderCoreId)
        {
            var attackerCore = GetCoreByIdAsNoTracking(attackerCoreId)?.FromJson<SessionCoresTable>();
            var defenderCore = GetCoreByIdAsNoTracking(defenderCoreId)?.FromJson<SessionCoresTable>();
            
            List<GameUnitsTable> attackers = GetCoreArmy(attackerCoreId);
            List<GameUnitsTable> defenders = GetCoreArmy(defenderCoreId);

            if (attackerCore != null)
            {
                if (defenderCore != null)
                {
                    if (attackers.Count <= 0 || defenders.Count <= 0)
                    {
                        logger.Warn("Attackers or Defenders have no army!");
                        EventLogger.AddLogForUser(
                            attackerCore.UserId, LogType.BattleFailure,
                            $"You have {attackers.Count} army, your enemy have {defenders.Count} army. " +
                            "So global police can't let you to attack that player!");

                        EventLogger.AddLogForUser(
                            defenderCore.UserId, LogType.BattleFailure,
                            $"Someone trying to attack you but " +
                                ((attackers.Count <= 0) ? "attackers have no army" : "you have no army!") +
                            "So global police can't let them to attack!");

                        return new BattleResponse() { Message = "No battle", WhoWonTheBattle = -1 };
                    }
                    
                    var result = GameEngine.DuelBattle(ref attackers, ref defenders);

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
                            if (attackers[i].GameUnitName.Trim() == "Warrior")
                            {
                                ++attackWarriorCount;
                            }
                            else if (attackers[i].GameUnitName.Trim() == "AttackAircraft")
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
                        EventLogger.AddLogForUser(attackerCoreId, LogType.BattleFailure,
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
                        for (var i = 0; i < defenders.Count; i++)
                        {
                            if (defenders[i].GameUnitName.Trim() == "Warrior")
                            {
                                ++defendersWarriorNumber;
                            }
                            else if (defenders[i].GameUnitName.Trim() == "AttackAircraft")
                            {
                                ++defendersAircraftCount;
                            }
                        }

                        defendersCasern.WarriorsNumber = defendersWarriorNumber;
                        defendersCasern.AttackAircraftNumber = defendersAircraftCount;
                        logger.Info($"Casern [coreId: {attackerCoreId}] warriors & attackaircraft number updated!");

                        if (defenderCore != null)
                        {
                            EventLogger.AddLogForUser(defenderCore.UserId, LogType.BattleFailure,
                                  $"Casern warriors & attackaircraft number updated!");
                        }
                    }
                    else
                    {
                        logger.Error($"Defenders don't have casern!");
                    }

                    m_SessionCasernsEntity.SaveChanges();

                    var attackerUser = m_UserContext.UserTable.AsNoTracking()
                        .Where(obj => obj.UserId == attackerCore.UserId)
                        .FirstOrDefault();
                    var defenderUser = m_UserContext.UserTable.AsNoTracking()
                            .Where(obj => obj.UserId == defenderCore.UserId)
                            .FirstOrDefault();
                    if (attackerUser != null && defenderUser != null)
                    {
                        if (result.WhoWonTheBattle == 0)
                        {
                            EventLogger.AddLogForUser(attackerUser.UserId, LogType.BattleFailure,
                                $"You won the battle, {defenderUser.Nickname}'s army been fully destroyed !");
                            EventLogger.AddLogForUser(defenderUser.UserId, LogType.BattleFailure,
                                $"Your army been fully destroyed by {attackerUser.Nickname}");
                            AddStatisticForDuel(attackerCoreId, defenderCoreId);
                        }
                        else if (result.WhoWonTheBattle == 1)
                        {
                            EventLogger.AddLogForUser(defenderUser.UserId, LogType.BattleFailure,
                                $"You won the battle, {attackerUser.Nickname}'s army been fully destroyed, core was protected !");
                            EventLogger.AddLogForUser(attackerUser.UserId, LogType.BattleFailure,
                                $"Your army been fully destroyed by {defenderUser.Nickname}");
                            AddStatisticForDuel(defenderCoreId, attackerCoreId);
                        }
                    }

                    return result;
                }
                else
                {
                    logger.Error("Defender core == null!");
                    EventLogger.AddLogForUser(defenderCore.UserId,LogType.BattleFailure,
                        $"Defender have no core!");
                }
            }
            else 
            {
                logger.Error("Attackers core == null!");
                EventLogger.AddLogForUser(attackerCore.UserId, LogType.BattleFailure,
                    $"You have no core!");
            }
            return new BattleResponse() { Message = "No Battle", WhoWonTheBattle = -1 };
        }

        public BattleResponse CoreBattle(int attackerCoreId, int defenderCoreId)
        {
            List<GameUnitsTable> attackers = GetCoreArmy(attackerCoreId);
            List<GameUnitsTable> defenders = GetCoreArmy(defenderCoreId);
            List<GameBuildingsTable> defendersBuildings = GetCoreBuildings(defenderCoreId);

            var result =
                GameEngine.CoreBattle(ref attackers, ref defenders, ref defendersBuildings);

            int attackWarriorCount = 0;
            int attackAircraftCount = 0;
            for (var i = 0; i < attackers.Count; i++)
            {
                if (attackers[i].GameUnitName.Trim() == "Warrior")
                {
                    ++attackWarriorCount;
                }
                else if (attackers[i].GameUnitName.Trim() == "AttackAircraft")
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
                if (attackers[i].GameUnitName.Trim() == "Warrior")
                {
                    ++defendersWarriorNumber;
                }
                else if (attackers[i].GameUnitName.Trim() == "AttackAircraft")
                {
                    ++defendersAircraftCount;
                }
            }

            for (var i = 0; i < defenders.Count; i++)
            {
                if (defenders[i].GameUnitName.Trim() == "Warrior")
                {
                    ++defendersWarriorNumber;
                }
                else if (defenders[i].GameUnitName.Trim() == "AttackAircraft")
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

        public void BaseAttackUpgrade(int coreId)
        {
            var core = m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (core != null)
            {
                ++core.AttackUpgrade;
                m_SessionBasesEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId,LogType.ProduceSuccess,
                   $"Upgrade base attack!");

            }
        }

        public void BaseDefenceUpgrade(int coreId)
        {
            var core = m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (core != null)
            {
                ++core.DefenceUpgrade;
                m_SessionBasesEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade base defence!");
            }
        }

        public void BaseCapacityUpgrade(int coreId)
        {
            var core = m_SessionBasesEntity.SessionBasesTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (core != null)
            {
                ++core.CapacityUpgrade;
                m_SessionBasesEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade base capacity!");
            }
        }

        public void CasernCapacityUpgrade(int coreId)
        {
            var casern = m_SessionCasernsEntity.SessionCasernsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (casern != null)
            {
                ++casern.CapacityUpgrade;
                m_SessionCasernsEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade casern capacity!");
            }
        }

        public void GoldMiningCapacityUpgrade(int coreId)
        {
            var goldMining = m_SessionGoldMiningsEntity.SessionGoldMiningsTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (goldMining != null)
            {
                ++goldMining.CapacityUpgrade;
                m_SessionGoldMiningsEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade gold mining capacity!");
            }
        }

        public void DefenceTowerAttackUpgrade(int coreId)
        {
            var defenceTower = m_SessionDefenceTowersEntity.SessionDefenceTowersTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (defenceTower != null)
            {
                ++defenceTower.AttackUpgrade;
                m_SessionDefenceTowersEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade defence tower attack!");
            }
        }

        public void DefenceTowerDefenceUpgrade(int coreId)
        {
            var defenceTower = m_SessionDefenceTowersEntity.SessionDefenceTowersTable
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (defenceTower != null)
            {
                ++defenceTower.DefenceUpgrade;
                m_SessionDefenceTowersEntity.SaveChanges();

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
                EventLogger.AddLogForUser(userId, LogType.ProduceSuccess,
                   $"Upgrade defence tower defence!");
            }
        }
    
        public string GetCoreRenderable(int coreId)
        {
            var core = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (core != null)
            {
                var map = m_SessionMapEntity.SessionMapTable.AsNoTracking()
                    .Where(obj => obj.SessionMapId == core.CoreMapId)
                    .FirstOrDefault();
                if (map != null)
                {
                    Renderable renderable = new Renderable()
                    {
                        SessionMapId = map.SessionMapId,
                        CoreId = coreId,
                        MapX = map.XCoord,
                        MapY = map.YCoord
                    };
                    return renderable.ToJson();
                }
                else
                {
                    logger.Warn($"Map[CoreId: {coreId}] == null");
                }
            }
            else
            {
                logger.Warn($"Core[CoreId: {coreId}] == null");
            }
            return new Renderable().ToJson();
        }

        public string GetOtherRenderable(int coreId)
        {
            var thisCore = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                .Where(obj => obj.SessionCoreId == coreId)
                .FirstOrDefault();
            if (thisCore != null)
            {
                var thisMap = m_SessionMapEntity.SessionMapTable.AsNoTracking()
                    .Where(obj => obj.SessionMapId == thisCore.CoreMapId)
                    .FirstOrDefault();
                if (thisMap != null)
                {
                    decimal left = thisMap.XCoord - 2000;
                    decimal right = thisMap.XCoord + 2000;
                    decimal top = thisMap.YCoord + 2000;
                    decimal bottom = thisMap.YCoord - 2000;

                    var others = 
                        m_SessionMapEntity.SessionMapTable.AsNoTracking()
                        .Where(obj => 
                            obj.XCoord >= left && obj.YCoord <= right &&
                            obj.YCoord >= bottom && obj.YCoord <= top)
                        .ToArray();
                    
                    //if (m_RenverableCache.ContainsKey(coreId))
                    //{
                    //    var cachedRenderables = m_RenverableCache[coreId];
                    //    for (int i = 0; i < cachedRenderables.Length; i++)
                    //    {
                    //        if (cachedRenderables[i].CoreId == others[i].SessionMapId)
                    //        {
                    //
                    //        }
                    //    }
                    //}

                    if (others != null && others.Length >= 0)
                    {
                        int idx = 0;
                        Renderable[] result = new Renderable[others.Length];
                        foreach (var other in others)
                        {
                            var otherCore = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                                .Where(obj => obj.CoreMapId == other.SessionMapId)
                                .FirstOrDefault();
                            var otherUser = m_UserContext.UserTable.AsNoTracking()
                                .Where(obj => obj.UserId == otherCore.UserId)
                                .FirstOrDefault();

                            result[idx++] = new Renderable()
                            {
                                SessionMapId = other.SessionMapId,
                                CoreId = otherCore.SessionCoreId,
                                Nickname = otherUser.Nickname,
                                MapX = other.XCoord,
                                MapY = other.YCoord
                            };
                        }
                        return result.ToJson();
                    }
                    else
                    {
                        logger.Warn($"Others [CoreId: {coreId}] == null or others.count <= 0");
                    }
                }
                else
                {
                    logger.Warn($"Map[CoreId: {coreId}] == null");
                }
            }
            else
            {
                logger.Warn($"Core[CoreId: {coreId}] == null");
            }

            return new Renderable().ToJson();
        }
    }
}
