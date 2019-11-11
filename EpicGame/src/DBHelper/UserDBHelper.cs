namespace EpicGame.src.DBHelper
{
    using NLog;
    using System.Linq;
    using System.Collections.Generic;
    
    using EpicGameCommon;
    using EpicGame.src.Models.User;
    using EpicGame.src.Models.Session;
    using EpicGame.Game;

    class UserDBHelper : System.IDisposable
    {
        public bool NeedDispose { get; set; } = false;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly UserEntity m_UserContext = new UserEntity();
        private readonly UserFriendsTableEntity  m_UserFriendsContext = new UserFriendsTableEntity();
        private readonly UserFollowersTableEntity m_UserFollowersContext = new UserFollowersTableEntity();
        private readonly UserFollowingTableEntity m_UserFollowingContext = new UserFollowingTableEntity();

        private static readonly SessionCoresEntity m_SessionCoreEntity = new SessionCoresEntity();
        private static readonly SessionBasesEntity m_SessionBasesEntity = new SessionBasesEntity();
        private static readonly SessionCasernsEntity m_SessionCasernsEntity = new SessionCasernsEntity();
        private static readonly SessionDefenceTowersEntity m_SessionDefenceTowersEntity = new SessionDefenceTowersEntity();
        private static readonly SessionGoldMiningsEntity m_SessionGoldMiningsEntity = new SessionGoldMiningsEntity();
        private static readonly SessionMapEntity m_SessionMapEntity = new SessionMapEntity();

        public UserDBHelper()
        {
            logger.Warn("Create UserDBHelper instance");
        }

        public void Dispose()
        {
            logger.Warn("Dispose call");
            if (NeedDispose)
            {
                logger.Warn("Disposing context.".ToUpper());
            
                NLog.LogManager.Shutdown();

                m_UserContext.Dispose();
                m_UserFriendsContext.Dispose();
                m_UserFollowersContext.Dispose();
                m_UserFollowingContext.Dispose();
            }
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
                    XCoord = (decimal)500 * 7_000_000,
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

                core = m_SessionCoreEntity.SessionCoresTable
                    .AsNoTracking()
                    .Where(obj => obj.UserId == userId)
                    .FirstOrDefault();
                if (core != null)
                {
                    SessionBasesTable @base = new SessionBasesTable()
                    {
                        SessionCoreId = core.SessionCoreId,
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
                    logger.Info($"Can't create core for UserdId: {userId}");
                    return false;
                }
            }
            else
            {
                logger.Error("can't find added coord!");
                return false;
            }
        }

        private bool RegisterUserToTable(UserTable user)
        {
            logger.Info($"Register user to table: " +
                $"{user.UserId} {user.FirstName} {user.SecondName}" +
                $"{user.PasswordHash} {user.Nickname} {user.Email}");
            var userIndex = FindUserByNickname(user.Nickname);
            if (userIndex == -1)
            {
                m_UserContext.UserTable.Add(user);
                m_UserContext.SaveChanges();
                GenerateCoreForUser(user.UserId);
                return true;
            }
            else
            {
                logger.Warn($"This user's nickname already exists in db! [id = {userIndex + 1}]");
                return false;
            }
        }

        public bool RegisterUserToTable(string firstName, string secondName,
            string passswordHash, string nickname, string email)
        {
            return RegisterUserToTable(new UserTable()
            {
                FirstName = firstName,
                SecondName = secondName,
                FullName = $"{firstName} {secondName}",
                PasswordHash = passswordHash,
                Nickname = nickname,
                Email = email
            });
        }

        public bool IsRegisteredUser(string nickname, string passwordHash)
        {
            string tnickname = nickname.Trim(' ');
            string tpasswordHash = passwordHash.Trim(' ');
            logger.Info($"check if user registered: {tnickname} {passwordHash}");
            var array = m_UserContext.UserTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Nickname.Trim(' ').Equals(tnickname) && 
                    array[i].PasswordHash.Trim(' ').Equals(tpasswordHash))
                {
                    logger.Info($"user {nickname} is found");
                    return true;
                }
            }
            logger.Info($"user {nickname} isn't found");
            return false;
        }

        public int GetUserIdByNickname(string nickname)
        {
            return m_UserContext.UserTable.AsNoTracking()
                .Where(obj => obj.Nickname == nickname)
                .FirstOrDefault().UserId;
        }

        public void RemoveUserByIdFromTable(int userId)
        {
            var user = m_UserContext.UserTable
                .Where(obj => obj.UserId == userId)
                .FirstOrDefault();
            if (user != null)
            {
                var core = m_SessionCoreEntity.SessionCoresTable
                    .Where(obj => obj.UserId == userId)
                    .FirstOrDefault();
                if (core != null)
                {
                    var baseSession = m_SessionBasesEntity.SessionBasesTable
                        .Where(obj => obj.SessionCoreId == core.SessionCoreId)
                        .FirstOrDefault();
                    if (baseSession != null)
                    {
                        m_SessionBasesEntity.SessionBasesTable.Remove(baseSession);
                        m_SessionBasesEntity.SaveChanges();
                        logger.Info($"remove core [id: {core.SessionCoreId}] from session bases table");
                    }

                    var casernSession = m_SessionCasernsEntity.SessionCasernsTable
                        .Where(obj => obj.SessionCoreId == core.SessionCoreId)
                        .FirstOrDefault();
                    if (casernSession != null)
                    {
                        m_SessionCasernsEntity.SessionCasernsTable.Remove(casernSession);
                        m_SessionCasernsEntity.SaveChanges();
                        logger.Info($"remove core [id: {core.SessionCoreId}] from session casern table");
                    }

                    var defenceTowerSession = m_SessionDefenceTowersEntity.SessionDefenceTowersTable
                        .Where(obj => obj.SessionCoreId == core.SessionCoreId)
                        .FirstOrDefault();
                    if (defenceTowerSession != null)
                    {
                        m_SessionDefenceTowersEntity.SessionDefenceTowersTable.Remove(defenceTowerSession);
                        m_SessionDefenceTowersEntity.SaveChanges();
                        logger.Info($"remove core [id: {core.SessionCoreId}] from session def tower table");
                    }

                    var goldMiningSession = m_SessionGoldMiningsEntity.SessionGoldMiningsTable
                        .Where(obj => obj.SessionCoreId == core.SessionCoreId)
                        .FirstOrDefault();
                    if (goldMiningSession != null)
                    {
                        m_SessionGoldMiningsEntity.SessionGoldMiningsTable.Remove(goldMiningSession);
                        m_SessionGoldMiningsEntity.SaveChanges();
                        logger.Info($"remove core [id: {core.SessionCoreId}] from gold mining table");
                    }

                    m_SessionCoreEntity.SessionCoresTable.Remove(core);
                    m_SessionCoreEntity.SaveChanges();
                    logger.Info($"remove core [id: {core.SessionCoreId}] from session core table");
                    var mapSession = m_SessionMapEntity.SessionMapTable
                        .Where(obj => obj.SessionMapId == core.CoreMapId)
                        .FirstOrDefault();
                    if (mapSession != null)
                    {
                        m_SessionMapEntity.SessionMapTable.Remove(mapSession);
                        m_SessionMapEntity.SaveChanges();
                        logger.Info($"remove core [id: {core.SessionCoreId}] from map table");
                    }

                }
                else
                {
                    logger.Warn($"can't remove core [id:{core?.SessionCoreId}] from table");
                }

                var friends = m_UserFriendsContext.UserFriendsTable
                     .Where(obj => obj.UserId == userId || obj.FriendId == userId)
                     .FirstOrDefault();
                if (friends != null)
                {
                    m_UserFriendsContext.UserFriendsTable.Remove(friends);
                    m_UserFriendsContext.SaveChanges();
                    logger.Info($"remove user [id: {user.UserId}] from friends table");
                }

                var followers = m_UserFollowersContext.UserFollowersTable
                    .Where(obj => obj.UserId == userId || obj.FollowerId == userId)
                    .FirstOrDefault();
                if (followers != null)
                {
                    m_UserFollowersContext.UserFollowersTable.Remove(followers);
                    m_UserFollowersContext.SaveChanges();
                    logger.Info($"remove user [id: {user.UserId}] from folllowers table");
                }

                var followings = m_UserFollowingContext.UserFollowingTable
                    .Where(obj => obj.UserId == userId || obj.FollowingId == userId)
                    .FirstOrDefault();
                if (followings != null)
                {
                    m_UserFollowingContext.UserFollowingTable.Remove(followings);
                    m_UserFollowingContext.SaveChanges();
                    logger.Info($"remove user [id: {user.UserId}] from folllowings table");
                }
                
                m_UserContext.UserTable.Remove(user);
                m_UserContext.SaveChanges();
                logger.Info($"remove user [id: {user.UserId}] from user table");
            }
            else
            {
                logger.Warn($"can't remove user [id:{userId}] from table");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(256)]
        private int FindUserByEmail(string email)
        {
            var array = m_UserContext.UserTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Email.Equals(email))
                {
                    logger.Info($"user [email: {email}] was found");
                    return array[i].UserId;
                }
            }
            return -1;
        }

        [System.Runtime.CompilerServices.MethodImpl(256)]
        public int FindUserByNickname(string nick)
        {
            var array = m_UserContext.UserTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Nickname.Equals(nick))
                {
                    logger.Info($"user [nick: {nick}] was found!");
                    return array[i].UserId;
                }
            }
            logger.Info($"user [nick: {nick}] not found found!");
            return -1;
        }

        /// <summary>
        /// Используется при инвайте в друзья [от thisId для idToAdd]
        /// </summary>
        /// <param name="thisId">Кто добавляет в друзья</param>
        /// <param name="idToAdd">Кого добавляют в друзья (кидают инвайт)</param>
        public void AddUserToFriends(int thisId, int idToAdd)
        {
            if (thisId != idToAdd)
            {
                logger.Info($"user [id: {thisId}] add [id: {idToAdd}] to friends");

                var thisUser = m_UserContext.UserTable.AsNoTracking()
                                 .Where(obj => obj.UserId == thisId).FirstOrDefault();

                var userToAdd = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == idToAdd)
                    .FirstOrDefault();

                var friend = m_UserFriendsContext.UserFriendsTable.AsNoTracking()
                        .Where(obj => obj.UserId == thisId)
                        .Where(obj => obj.FriendId == idToAdd)
                        .FirstOrDefault();
                if (friend != null)
                {
                    //this user already in friend
                    EventLogger.AddLogForUser(thisId, LogType.Communication,
                        $"User [{userToAdd?.Nickname}] already in your friend list");
                }
                else
                {
                    var follower = m_UserFollowersContext.UserFollowersTable
                        .Where(obj => obj.UserId == thisId)
                        .Where(obj => obj.FollowerId == idToAdd)
                        .FirstOrDefault();

                    if (follower != null)
                    {
                        //логика для follower
                        //thisId.Follower.Delete(idToAdd)
                        m_UserFollowersContext.UserFollowersTable.Remove(follower);

                        //thisId.Friends.Add(idToAdd)
                        m_UserFriendsContext.UserFriendsTable.Add(new UserFriendsTable()
                        {
                            UserId = thisId,
                            FriendId = idToAdd
                        });
                        //idToAdd.Friend.Add(thisId)
                        m_UserFriendsContext.UserFriendsTable.Add(new UserFriendsTable()
                        {
                            UserId = idToAdd,
                            FriendId = thisId
                        });
                        //idToAdd.Following.Delete(thisId)
                        var following = 
                            m_UserFollowingContext.UserFollowingTable
                            .Where(obj => obj.UserId == idToAdd)
                            .Where(obj => obj.FollowingId == thisId)
                            .FirstOrDefault();

                        if (following != null)
                        {
                            m_UserFollowingContext.UserFollowingTable.Remove(following);
                        }

                        m_UserFollowingContext.SaveChanges();
                        m_UserFriendsContext.SaveChanges();
                        m_UserFollowersContext.SaveChanges();

                        EventLogger.AddLogForUser(thisId,
                            LogType.Communication,
                            $"You add user [{userToAdd?.Nickname}] to friends list");
                        EventLogger.AddLogForUser(idToAdd, LogType.Communication,
                            $"User [{thisUser?.Nickname}] accept request for friendship");
                    }
                    else
                    {
                        var following = m_UserFollowingContext.UserFollowingTable.AsNoTracking()
                            .Where(obj => obj.UserId == thisId)
                            .Where(obj => obj.FollowingId == idToAdd)
                            .FirstOrDefault();

                        if (following != null)
                        {
                            //логика followings
                            EventLogger.AddLogForUser(thisId, LogType.Communication,
                                $"You already following user [{userToAdd?.Nickname}]");
                        }
                        else
                        {
                            //логика none
                            //thisId.Following.Add(idToAdd);
                            m_UserFollowingContext.UserFollowingTable.Add(new UserFollowingTable()
                            {
                                UserId = thisId,
                                FollowingId = idToAdd
                            });
                            //idToAdd.Followers.Add(thisId);
                            m_UserFollowersContext.UserFollowersTable.Add(new UserFollowersTable()
                            {
                                UserId = idToAdd,
                                FollowerId = thisId
                            });

                            m_UserFollowingContext.SaveChanges();
                            m_UserFollowersContext.SaveChanges();

                            EventLogger.AddLogForUser(thisId, LogType.Communication,
                                $"You are following user [{userToAdd?.Nickname}]");
                            EventLogger.AddLogForUser(idToAdd, LogType.Communication,
                                 $"User [{thisUser?.Nickname}] now your follower");
                        }
                    }
                }
            }
            else
            {
                logger.Warn("User trying to add himself in a friends list");
                EventLogger.AddLogForUser(thisId, LogType.Communication,
                    "You can't add yourself to a friends list!");
            }
        }

        /// <summary>
        /// не тестил
        /// </summary>
        /// <param name="thisId">Кто удаляет из друзей</param>
        /// <param name="idToRemove">Кого удаляют из друзей</param>
        public void RemoveUserFromFriends(int thisId, int idToRemove)
        {
            if (thisId != idToRemove)
            {
                logger.Info($"user [id: {thisId}] remove [id: {idToRemove}] from friends");
                var thisUserNickname = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == thisId)
                    .FirstOrDefault()?.Nickname;

                var userToRemoveNicname = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == idToRemove)
                    .FirstOrDefault()?.Nickname;
                
                var friend = m_UserFriendsContext.UserFriendsTable
                    .Where(obj => obj.UserId == thisId)
                    .Where(obj => obj.FriendId == idToRemove)
                    .FirstOrDefault();

                if (friend != null)
                {
                    //friends logic
                    //thisId.Friend.Delete(idToRemove);
                    m_UserFriendsContext.UserFriendsTable.Remove(friend);

                    //thisId.Followers.Add(idToRemove);
                    m_UserFollowersContext.UserFollowersTable.Add(new UserFollowersTable()
                    {
                        UserId = thisId,
                        FollowerId = idToRemove
                    });

                    //idToRemove.Friend.Delete(thisId);
                    var userToRemove = m_UserFriendsContext.UserFriendsTable
                        .Where(obj => obj.UserId == idToRemove)
                        .Where(obj => obj.FriendId == thisId)
                        .FirstOrDefault();

                    if (userToRemove != null)
                    {
                        m_UserFriendsContext.UserFriendsTable.Remove(userToRemove);
                    }

                    //idToRemove.Followings.Add(thisId);
                    m_UserFollowingContext.UserFollowingTable.Add(new UserFollowingTable()
                    {
                        UserId = idToRemove,
                        FollowingId = thisId
                    });

                    m_UserFollowingContext.SaveChanges();
                    m_UserFriendsContext.SaveChanges();
                    m_UserFollowersContext.SaveChanges();

                    EventLogger.AddLogForUser(thisId, LogType.Communication,
                        $"You remove user [{userToRemoveNicname}] from friends list");
                    EventLogger.AddLogForUser(idToRemove, LogType.Communication,
                        $"User {thisUserNickname} delete you from friends list");
                }
                else
                {
                    var follower = m_UserFollowersContext.UserFollowersTable.AsNoTracking()
                       .Where(obj => obj.UserId == thisId)
                       .Where(obj => obj.FollowerId == idToRemove)
                       .FirstOrDefault();

                    if (follower != null)
                    {
                        //логика для follower
                        EventLogger.AddLogForUser(thisId, LogType.Communication,
                            $"User [{userToRemoveNicname}] is your follower, you can't remove him from that list");
                        return;
                    }
                    else
                    {
                        var following = m_UserFollowingContext.UserFollowingTable
                           .Where(obj => obj.UserId == thisId)
                           .Where(obj => obj.FollowingId == idToRemove)
                           .FirstOrDefault();
                        if (following != null)
                        {
                            //логика followings
                            //thisId.Followings.Delete(idToRemove)
                            m_UserFollowingContext.UserFollowingTable.Remove(following);

                            //idToRemove.Followers.Delete(thisId)
                            var deleteThisFromFolowerList = m_UserFollowersContext.UserFollowersTable
                               .Where(obj => obj.UserId == idToRemove)
                               .Where(obj => obj.FollowerId == thisId)
                               .FirstOrDefault();

                            m_UserFollowersContext.UserFollowersTable.Remove(deleteThisFromFolowerList);

                            m_UserFollowersContext.SaveChanges();
                            m_UserFollowersContext.SaveChanges();

                            EventLogger.AddLogForUser(thisId, LogType.Communication,
                                $"You stop following user [{userToRemoveNicname}]");

                            EventLogger.AddLogForUser(idToRemove, LogType.Communication,
                                $"User {thisUserNickname} stop following you");

                        }
                        else
                        {
                            //none logic
                            return;
                        }
                    }
                }
            }
            else
            {
                logger.Warn("User trying to remove himself from a friends list");
                EventLogger.AddLogForUser(thisId, LogType.Communication,
                    "You can't remove yourself from a friends list!");
            }
        }

        public static int GetCoreIdByUserId(int userId)
        {
            var core = m_SessionCoreEntity.SessionCoresTable.AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .FirstOrDefault();
            if (core != null)
            {
                return core.SessionCoreId;
            }
            else
            {
                return -1;
            }
        }

        public List<UserTable> GetAllUsers()
        {
            return m_UserContext.UserTable.AsNoTracking().ToList();
        }

        public List<UserFriendsTable> GetUsersFriendsTable(int userId)
        {
            return m_UserFriendsContext
                .UserFriendsTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public List<UserFollowersTable> GetUsersFollowersTable(int userId)
        {
            return m_UserFollowersContext
                .UserFollowersTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public List<UserFollowingTable> GetUsersFollowingsTable(int userId)
        {
            return m_UserFollowingContext
                .UserFollowingTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public string GetAllUsersInfo()
        {
            var allUsers = GetAllUsers();
            var result = new List<UserInfo>();
            foreach(var user in allUsers)
            {
                var core = m_SessionCoreEntity.SessionCoresTable
                            .AsNoTracking()
                            .Where(obj => obj.UserId == user.UserId)
                            .FirstOrDefault();
                result.Add(new UserInfo() {
                    Nickname = user.Nickname.Trim(),
                    UserId = user.UserId,
                    CoreId = (core == null) ? 0 : core.SessionCoreId
                });
            }
            return result.ToArray().ToJson();
        }

        public string GetUsersFriendsInfo(int userId)
        {
            List<UserFriendsTable> friendsList = m_UserFriendsContext
               .UserFriendsTable
               .AsNoTracking()
               .Where(obj => obj.FriendId == userId)
               .ToList();
            var result = new List<UserInfo>();
            foreach (var friend in friendsList)
            {
                var userTableInfo = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == friend.UserId)
                    .FirstOrDefault();
                var core = m_SessionCoreEntity.SessionCoresTable
                            .AsNoTracking()
                            .Where(obj => obj.UserId == friend.UserId)
                            .FirstOrDefault();
                result.Add(new UserInfo()
                {
                    Nickname = userTableInfo.Nickname.Trim(),
                    UserId = userTableInfo.UserId,
                    CoreId = (core == null) ? 0 : core.SessionCoreId
                });
            }
            return result.ToArray().ToJson();
        }

        public string GetUsersFollowersInfo(int userId)
        {
            var followersList = m_UserFollowingContext
               .UserFollowingTable
               .AsNoTracking()
               .Where(obj => obj.FollowingId == userId)
               .ToList();
            var result = new List<UserInfo>();
            foreach (var user in followersList)
            {
                var userTableInfo = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == user.UserId)
                    .FirstOrDefault();
                var core = m_SessionCoreEntity.SessionCoresTable
                            .AsNoTracking()
                            .Where(obj => obj.UserId == user.UserId)
                            .FirstOrDefault();
                result.Add(new UserInfo()
                {
                    Nickname = userTableInfo.Nickname.Trim(),
                    UserId = userTableInfo.UserId,
                    CoreId = (core == null) ? 0 : core.SessionCoreId
                });
            }
            return result.ToArray().ToJson();
        }

        public string GetUsersFollowingsInfo(int userId)
        {
            var followingsList = m_UserFollowersContext
               .UserFollowersTable
               .AsNoTracking()
               .Where(obj => obj.FollowerId == userId)
               .ToList();
            var result = new List<UserInfo>();
            foreach (var user in followingsList)
            {
                var userTableInfo = m_UserContext.UserTable.AsNoTracking()
                    .Where(obj => obj.UserId == user.UserId)
                    .FirstOrDefault();
                var core = m_SessionCoreEntity.SessionCoresTable
                            .AsNoTracking()
                            .Where(obj => obj.UserId == user.UserId)
                            .FirstOrDefault();
                result.Add(new UserInfo()
                {
                    Nickname = userTableInfo?.Nickname?.Trim(),
                    UserId = (userTableInfo == null) ? -1 : userTableInfo.UserId,
                    CoreId = (core == null) ? -1 : core.SessionCoreId
                });
            }
            return result.ToArray().ToJson();
        }

        
    }

}
