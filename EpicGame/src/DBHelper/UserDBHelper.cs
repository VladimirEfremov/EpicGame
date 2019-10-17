namespace EpicGame.src.DBHelper
{
    using NLog;
    using System.Linq;
    using EpicGame.src.Models.User;

    class UserDBHelper : System.IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly UserTableEntity m_UserContext = new UserTableEntity();
        private readonly UserFriendsTableEntity  m_UserFriendsContext = new UserFriendsTableEntity();
        private readonly UserFollowersTableEntity m_UserFollowersContext = new UserFollowersTableEntity();
        private readonly UserFollowingTableEntity m_UserFollowingContext = new UserFollowingTableEntity();

        public UserDBHelper()
        {
            logger.Info("Create UserDBHelper instance");
        }

        public void Dispose()
        {
            logger.Info("Disposing context.");
            
            NLog.LogManager.Shutdown();

            m_UserContext.Dispose();
            m_UserFriendsContext.Dispose();
            m_UserFollowersContext.Dispose();
            m_UserFollowingContext.Dispose();
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
                UserContextTrySave();
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

        public void RemoveUserByIdFromTable(System.Int32 id)
        {
            var user = m_UserContext.UserTable.Find(id);
            if (user != null)
            {
                logger.Info($"remove user [id: {user.UserId}] from db");
                m_UserContext.UserTable.Remove(user);
                UserContextTrySave();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(256)]
        private System.Int32 FindUserByEmail(string email)
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
        public System.Int32 FindUserByNickname(string nick)
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
        public void AddUserToFriends(System.Int32 thisId, System.Int32 idToAdd)
        {
            logger.Info($"user [id: {thisId}] add [id: {idToAdd}] to friends");
            var arrayFriends = from   friends in m_UserFriendsContext.UserFriendsTable
                               where  friends.UserId   == thisId
                               where  friends.FriendId == idToAdd
                               select friends;
            if (arrayFriends.Count() != 0)
            {
                //this user already in friend
                return;
            }
            else
            {
                var arrayFollowers = from followers in m_UserFollowersContext.UserFollowersTable
                                     where followers.UserId == thisId
                                     where followers.FollowerId == idToAdd
                                     select followers;
                if (arrayFollowers.Count() != 0)
                {
                    //логика для follower
                    //thisId.Follower.Delete(idToAdd)
                    foreach (var follower in arrayFollowers)
                    {
                        m_UserFollowersContext.UserFollowersTable.Remove(follower);
                    }

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
                    var idToRemoveUsers = from followings in m_UserFollowingContext.UserFollowingTable
                                          where followings.UserId == idToAdd
                                          where followings.FollowingId == thisId
                                          select followings;
                    if (idToRemoveUsers.Count() != 0)
                    {
                        foreach (var following in idToRemoveUsers)
                        {
                            m_UserFollowingContext.UserFollowingTable.Remove(following);
                        }
                    }

                    UserFollowingsContextTrySave();
                    UserFriendsContextTrySave();
                    UserFollowersContextTrySave();
                }
                else
                {
                    var arrayFollowings = from following in m_UserFollowingContext.UserFollowingTable
                                          where following.UserId == thisId
                                          where following.FollowingId == idToAdd
                                          select following;
                    if (arrayFollowings.Count() != 0)
                    {
                        //логика followings
                        return;
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

                        UserFollowingsContextTrySave();
                        UserFollowersContextTrySave();
                    }
                }
            }
        }

        /// <summary>
        /// не тестил
        /// </summary>
        /// <param name="thisId">Кто удаляет из друзей</param>
        /// <param name="idToRemove">Кого удаляют из друзей</param>
        public void RemoveUserFromFriends(System.Int32 thisId, System.Int32 idToRemove)
        {
            logger.Info($"user [id: {thisId}] remove [id: {idToRemove}] from friends");
            var arrayFriends = from friends in m_UserFriendsContext.UserFriendsTable
                               where friends.UserId == thisId
                               where friends.FriendId == idToRemove
                               select friends;
            if (arrayFriends.Count() != 0)
            {
                //friends logic
                //thisId.Friend.Delete(idToRemove);
                foreach (var friend in arrayFriends)
                {
                    m_UserFriendsContext.UserFriendsTable.Remove(friend);
                }

                //thisId.Followers.Add(idToRemove);
                m_UserFollowersContext.UserFollowersTable.Add(new UserFollowersTable()
                {
                    UserId = thisId,
                    FollowerId = idToRemove
                });

                //idToRemove.Friend.Delete(thisId);
                var idToRemoveUsers = from friends in m_UserFriendsContext.UserFriendsTable
                                      where friends.UserId == idToRemove
                                      where friends.FriendId == thisId
                                      select friends;
                if (idToRemoveUsers.Count() != 0)
                {
                    foreach (var friend in idToRemoveUsers)
                    {
                        m_UserFriendsContext.UserFriendsTable.Remove(friend);
                    }
                }

                //idToRemove.Followings.Add(thisId);
                m_UserFollowingContext.UserFollowingTable.Add(new UserFollowingTable()
                {
                    UserId = idToRemove,
                    FollowingId = thisId
                });

                UserFollowingsContextTrySave();
                UserFriendsContextTrySave();
                UserFollowersContextTrySave();
            }
            else
            {
                var arrayFollowers = from followers in m_UserFollowersContext.UserFollowersTable
                                     where followers.UserId == thisId
                                     where followers.FollowerId == idToRemove
                                     select followers;
                if (arrayFollowers.Count() != 0)
                {
                    //логика для follower
                    return;
                }
                else
                {
                    var arrayFollowings = from following in m_UserFollowingContext.UserFollowingTable
                                          where following.UserId == thisId
                                          where following.FollowingId == idToRemove
                                          select following;
                    if (arrayFollowings.Count() != 0)
                    {
                        //логика followings
                        //thisId.Followings.Delete(idToRemove)
                        foreach (var following in arrayFollowings)
                        {
                            m_UserFollowingContext.UserFollowingTable.Remove(following);
                        }

                        //idToRemove.Followers.Delete(thisId)
                        var idToRemoveUsers = from followers in m_UserFollowersContext.UserFollowersTable
                                              where followers.UserId == idToRemove
                                              where followers.FollowerId == thisId
                                              select followers;
                        foreach (var followers in idToRemoveUsers)
                        {
                            m_UserFollowersContext.UserFollowersTable.Remove(followers);
                        }

                        UserFollowingsContextTrySave();
                        UserFollowersContextTrySave();
                    }
                    else
                    {
                        //none logic
                        return;
                    }
                }
            }
        }

        public System.Collections.Generic.List<UserTable> GetAllUsers()
        {
            return m_UserContext.UserTable.ToList();
        }

        public System.Collections.Generic.List<UserFriendsTable> GetUsersFriendsTable(int userId)
        {
            return m_UserFriendsContext
                .UserFriendsTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public System.Collections.Generic.List<UserFollowersTable> GetUsersFollowersTable(int userId)
        {
            return m_UserFollowersContext
                .UserFollowersTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public System.Collections.Generic.List<UserFollowingTable> GetUsersFollowingsTable(int userId)
        {
            return m_UserFollowingContext
                .UserFollowingTable
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToList();
        }

        public void UserContextTrySave()
        {
            try
            {
                m_UserContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Info("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public void UserFriendsContextTrySave()
        {
            try
            {
                m_UserFriendsContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Info("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public void UserFollowersContextTrySave()
        {
            try
            {
                m_UserFollowersContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Info("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public void UserFollowingsContextTrySave()
        {
            try
            {
                m_UserFollowingContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Info("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

    }

}
