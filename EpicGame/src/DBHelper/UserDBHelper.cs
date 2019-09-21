namespace EpicGame.src.DBHelper
{
    using System.Linq;
    using EpicGame.src.Models;

    class UserDBHelper : System.IDisposable
    {
        private readonly UserTableEntity m_UserContext = new UserTableEntity();
        private readonly UserRelationTableEntity m_UserRelationContext = new UserRelationTableEntity();
        public UserDBHelper()
        {
        }

        public void Dispose()
        {
            m_UserContext.Dispose();
            m_UserRelationContext.Dispose();
        }

        public void RegisterUserToTable(UserTable user)
        {
            var userIndex = FindUserByEmail(user.Nickname);
            if (userIndex == -1)
            {
                var sb = new System.Text.StringBuilder();
                {
                    var usersToAdd = m_UserContext.UserTable.ToList();
                    for (var i = 0; i < usersToAdd.Count; i++)
                    {
                        sb.Append($"{usersToAdd[i].Id} ");
                    }
                }

                m_UserContext.UserTable.Add(user);
                UserContextTrySave();
                user.Id = FindUserByEmail(user.Email);
                
                var array = m_UserRelationContext.UserRelationTable.ToArray();
                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i].Relation == (System.Int32)RelationType.None)
                    {
                        array[i].List += $"{user.Id} ";
                    }
                }
                var newAttribute = new UserRelationTable()
                {
                    UserId = user.Id,
                    Relation = (System.Int32)RelationType.None,
                    List = sb.ToString()
                };
                m_UserRelationContext.UserRelationTable.Add(newAttribute);
                UserRelationContextTrySave();
            }
            else
            {
                //Log.Warn: $"This user's nickname already exists in db! [id = {index + 1}]" 
            }
        }

        public void RegisterUserToTable(string firstName, string secondName,
            string passswordHash, string nickname, string email)
        {
            RegisterUserToTable(new UserTable()
            {
                FirstName = firstName,
                SecondName = secondName,
                FullName = $"{firstName} {secondName}",
                PasswordHash = passswordHash,
                Nickname = nickname,
                Email = email
            });
        }

        public bool IsRegisteredUser(string email, string passwordHash)
        {
            var array = m_UserContext.UserTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Email.Equals(email) && array[i].PasswordHash.Equals(passwordHash))
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveUserByIdFromTable(System.Int32 id)
        {
            var user = m_UserContext.UserTable.Find(id);
            if (user != null)
            {
                m_UserContext.UserTable.Remove(user);
                m_UserContext.SaveChanges();
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
                    return array[i].Id;
                }
            }
            return -1;
        }

        [System.Runtime.CompilerServices.MethodImpl(256)]
        private UserRelationTable FindAttributeByIdRelation(System.Int32 id, RelationType group)
        {
            var array = m_UserRelationContext.UserRelationTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].UserId   == id && 
                    array[i].Relation == (System.Int32)group)
                {
                    return array[i];
                }
            }
            //группы нет, значит мы должны ее добавить
            var userRelationTable = new UserRelationTable()
            {
                UserId = id,
                Relation = (System.Int32)group,
                List = ""
            };
            m_UserRelationContext.UserRelationTable.Add(userRelationTable);
            UserRelationContextTrySave();
            return userRelationTable;
        }

            //AddUserToFriends(User this, User toAdd)
            //or
            //In progress...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisId">Кто добавляет в друзья</param>
        /// <param name="idToAdd">Кого добавляют в друзья (кидают инвайт)</param>
        /// <returns>Возвращает, удачно ли произошло добавление</returns>
        public void AddUserToFriends(System.Int32 thisId, System.Int32 idToAdd)
        {
            var array = m_UserRelationContext.UserRelationTable.ToArray();
            if (array.Length > 0)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i].UserId == thisId &&
                        (array[i].List.IndexOf(idToAdd.ToString()) != -1))
                    {
                        //None: done
                        if (array[i].Relation == (System.Int32)RelationType.None)
                        {
                            //удаляем idToAdd из thisId.List группы none
                            var list = array[i].List;
                            var temp = $"{idToAdd} ";
                            array[i].List = list.Remove(
                                list.IndexOf(temp),
                                temp.Length);

                            //добавляем thisId to Following group
                            var user = FindAttributeByIdRelation(thisId, RelationType.Following);
                            user.List += idToAdd.ToString() + " ";

                            //делаем тоже самое для idToAdd
                            var userToAdd = FindAttributeByIdRelation(idToAdd, RelationType.None);
                            list = userToAdd.List;
                            temp = $"{thisId} ";
                            userToAdd.List = list.Remove(
                                list.IndexOf(temp),
                                temp.Length);

                            userToAdd = FindAttributeByIdRelation(idToAdd, RelationType.Followers);
                            userToAdd.List += thisId.ToString() + " ";
                        }
                        else if (array[i].Relation == (System.Int32)RelationType.Following ||
                                 array[i].Relation == (System.Int32)RelationType.Friends)
                        {
                            //do nothing there
                        }
                        else //RelationType.Follower
                        {
                            var list = array[i].List;
                            var temp = $"{idToAdd} ";
                            array[i].List = list.Remove(
                                list.IndexOf(temp),
                                temp.Length);

                            //добавляем thisId to Friends group
                            var user = FindAttributeByIdRelation(thisId, RelationType.Friends);
                            user.List += idToAdd.ToString() + " ";

                            //удаляем thisID from Following
                            user = FindAttributeByIdRelation(idToAdd, RelationType.Following);
                            list = user.List;
                            temp = $"{thisId} ";
                            user.List = list.Remove(
                                list.IndexOf(temp),
                                temp.Length);
                            
                            //insert thisId into Friends group
                            user = FindAttributeByIdRelation(idToAdd, RelationType.Friends);
                            user.List += thisId.ToString() + " ";
                        }
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Error: User relation table is empty!");
                throw new System.Exception();
            }
        }

        public System.Collections.Generic.List<UserTable> GetListOfUsers()
        {
            return m_UserContext.UserTable.ToList();
        }

        public System.Data.Entity.DbSet<UserRelationTable> GetDbSet() => 
            m_UserRelationContext.UserRelationTable;

        public System.Collections.Generic.List<UserRelationTable> GetListOfUsersRelations()
        {
            return m_UserRelationContext.UserRelationTable.ToList();
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
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void UserRelationContextTrySave()
        {
            try
            {
                m_UserRelationContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }



}
