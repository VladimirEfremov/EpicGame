namespace EpicGame.src.DBHelper
{
    using System.Linq;
    using EpicGame.src.Models;

    public static class AdditionalString
    {
        public static System.Int32 IndexOfId(this string _string, string _id)
        {
            System.Int32 i;
            System.Int32 j;
            string sid = $"{_id} ";
            bool isNumberFound = false;

            for (i = 0; i < _string.Length; i++)
            {
                if (_string[i] == sid[0])
                {
                    isNumberFound = true;
                    if (i + sid.Length <= _string.Length)
                    {
                        if (i != 0)
                        {
                            if (_string[i - 1] != ' ')
                            {
                                isNumberFound = false;
                                j = sid.Length;
                            }
                        }

                        for (j = 1; j < sid.Length; j++)
                        {
                            if (_string[i + j] != sid[j])
                            {
                                isNumberFound = false;
                                j = sid.Length;
                            }
                        }

                        if (isNumberFound)
                        {
                             return i; 
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return -1;
        }
    }

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

        private void RegisterUserToTable(UserTable user)
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
            string temail = email.Trim(' ');
            string tpasswordHash = passwordHash.Trim(' ');

            var array = m_UserContext.UserTable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Email.Trim(' ').Equals(temail) && array[i].PasswordHash.Trim(' ').Equals(tpasswordHash))
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
            m_UserRelationContext.UserRelationTable.Add(new UserRelationTable()
            {
                UserId = id,
                Relation = (System.Int32)group,
                List = ""
            });
            UserRelationContextTrySave();
            array = m_UserRelationContext.UserRelationTable.ToArray();
            return array[array.Length - 1];
        }

        /// <summary>
        /// Используется при инвайте в друзья [от thisId для idToAdd]
        /// </summary>
        /// <param name="thisId">Кто добавляет в друзья</param>
        /// <param name="idToAdd">Кого добавляют в друзья (кидают инвайт)</param>
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
                        if (array[i].Relation == (System.Int32)RelationType.None)
                        {
                            //удаляем idToAdd из thisId.List группы none
                            var list = array[i].List;
                            var temp = $"{idToAdd}";
                            var indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                array[i].List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //добавляем thisId to Following group
                            var user = FindAttributeByIdRelation(thisId, RelationType.Following);
                            temp = $"{idToAdd}";
                            if (user.List.IndexOfId(temp) == -1)
                            {
                                user.List += $"{temp} ";
                            }

                            //делаем тоже самое для idToAdd
                            var userToAdd = FindAttributeByIdRelation(idToAdd, RelationType.None);
                            list = userToAdd.List;
                            temp = $"{thisId}";
                            indexOfId = userToAdd.List.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                userToAdd.List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            userToAdd = FindAttributeByIdRelation(idToAdd, RelationType.Followers);
                            temp = $"{thisId}";
                            if (user.List.IndexOfId(temp) == -1)
                            {
                                userToAdd.List += $"{temp} ";
                            }
                            UserRelationContextTrySave();
                        }
                        //Following -> followers
                        else if (array[i].Relation == (System.Int32)RelationType.Followers)
                        {
                            //удаляем thisid из following
                            var list = array[i].List;
                            var temp = $"{idToAdd}";
                            var indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                array[i].List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //добавляем idToAdd to Friends group
                            var user = FindAttributeByIdRelation(thisId, RelationType.Friends);
                            temp = $"{idToAdd}";
                            if (user.List.IndexOfId(temp) == -1)
                            {
                                user.List += $"{temp} ";
                            }

                            //удаляем thisID from Following
                            user = FindAttributeByIdRelation(idToAdd, RelationType.Following);
                            list = user.List;
                            temp = $"{thisId}";
                            indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                user.List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //insert thisId into Friends group
                            user = FindAttributeByIdRelation(idToAdd, RelationType.Friends);
                            temp = $"{thisId}";
                            if (user.List.IndexOf(temp) == -1)
                            {
                                user.List += $"{temp} ";
                            }
                            UserRelationContextTrySave();
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

        /// <summary>
        /// не тестил
        /// </summary>
        /// <param name="thisId">Кто удаляет из друзей</param>
        /// <param name="idToRemove">Кого удаляют из друзей</param>
        public void RemoveUserFromFriends(System.Int32 thisId, System.Int32 idToRemove)
        {
            var array = m_UserRelationContext.UserRelationTable.ToArray();
            if (array.Length > 0)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i].UserId == thisId &&
                        (array[i].List.IndexOf(idToRemove.ToString()) != -1))
                    {
                        if (array[i].Relation == (System.Int32)RelationType.Following)
                        {
                            //thisid: удаляем idToAdd из thisId.List группы following
                            var list = array[i].List;
                            var temp = $"{idToRemove}";
                            var indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                array[i].List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //thisid: заносим idToRemove в None
                            var thisIdUser = FindAttributeByIdRelation(thisId, RelationType.None);
                            temp = $"{idToRemove}";
                            if (thisIdUser.List.IndexOfId(temp) == -1)
                            {
                                thisIdUser.List += $"{temp} ";
                            }

                            //idToRemove: удаляем thisId из Followers group
                            var userToRemove = FindAttributeByIdRelation(idToRemove, RelationType.Followers);
                            list = userToRemove.List;
                            temp = $"{thisId}";
                            indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                userToRemove.List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //idToRemove: thisId -> None
                            userToRemove = FindAttributeByIdRelation(idToRemove, RelationType.None);
                            temp = $"{thisId}";
                            if (userToRemove.List.IndexOfId(temp) == -1)
                            {
                                userToRemove.List += $"{temp} ";
                            }

                            UserRelationContextTrySave();
                        }
                        else if (array[i].Relation == (System.Int32)RelationType.Friends)
                        {
                            //do nothing there
                            //thisid: удаляем idToAdd из thisId.List группы following
                            var list = array[i].List;
                            var temp = $"{idToRemove}";
                            var indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                array[i].List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //thisid: заносим idToRemove в None
                            var thisIdUser = FindAttributeByIdRelation(thisId, RelationType.Followers);
                            temp = $"{idToRemove}";
                            if (thisIdUser.List.IndexOfId(temp) == -1)
                            {
                                thisIdUser.List += $"{temp} ";
                            }

                            //idToRemove: удаляем thisId из Followers group
                            var userToRemove = FindAttributeByIdRelation(idToRemove, RelationType.Friends);
                            list = userToRemove.List;
                            temp = $"{thisId}";
                            indexOfId = list.IndexOfId(temp);
                            if (indexOfId != -1)
                            {
                                userToRemove.List = list.Remove(
                                    indexOfId, temp.Length + 1);
                            }

                            //idToRemove: thisId -> None
                            userToRemove = FindAttributeByIdRelation(idToRemove, RelationType.Following);
                            temp = $"{thisId}";
                            if (userToRemove.List.IndexOfId(temp) == -1)
                            {
                                userToRemove.List += $"{temp} ";
                            }

                            UserRelationContextTrySave();
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
            return m_UserRelationContext.UserRelationTable.OrderBy(user=>user.Relation).ToList();
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
