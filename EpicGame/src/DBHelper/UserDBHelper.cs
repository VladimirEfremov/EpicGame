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
                m_UserContext.SaveChanges();
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
                m_UserRelationContext.SaveChanges();
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

        //AddUserToFriends(User this, User toAdd)
        //or
        //In progress...
        public bool AddUserToFriends(UserTable @this, UserTable toAdd, RelationType group)
        {
            var array = m_UserRelationContext.UserRelationTable.ToArray();
            if (array.Length  > 0)
            {
                var inFriends = m_UserRelationContext.UserRelationTable
                    .Where(p => p.UserId == @this.Id)
                    .Where(p => p.Relation == (System.Int32)RelationType.Friend);
                var follower = m_UserRelationContext.UserRelationTable
                    .Where(p => p.UserId == @this.Id)
                    .Where(p => p.Relation == (System.Int32)RelationType.Follower);
                var followers = m_UserRelationContext.UserRelationTable
                    .Where(p => p.UserId == @this.Id)
                    .Where(p => p.Relation == (System.Int32)RelationType.Followers);

                if (inFriends == null)
                {
                    
                }

                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i].UserId == @this.Id)
                    {
                        if ((array[i].Relation == (System.Int32)RelationType.Friend ||
                             array[i].Relation == (System.Int32)RelationType.Follower) &&
                             array[i].List.IndexOf(toAdd.Id.ToString()) != -1)
                        {
                            //skip
                        }
                        else if (array[i].Relation == (System.Int32)RelationType.Followers &&
                                 array[i].List.IndexOf(toAdd.Id.ToString()) != -1)
                        {
                            //@this && toAdd become friends
                            //@this.List[followers].delete(toAdd)
                            array[i].List.Substring(
                                array[i].List.IndexOf($" {toAdd.Id} "), $"  {toAdd.Id}".Length);
                            //@this.List[friends].add(toAdd)
                            
                        }
                        else
                        {
                            
                        }
                    }

                    /*
                        if (array[i].UserId == @this.Id && 
                        array[i].Relation == (System.Int32)group)
                    {
                        var list = array[i].List;
                        if (list.IndexOf(toAdd.Id.ToString()) == -1)
                        {
                            array[i].List += $"{toAdd.Id.ToString()} ";
                            m_UserRelationContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            //Log.Warn: {toAdd} already in the group
                            return false;
                        }
                    }
                    */
                }
                var attribute = new UserRelationTable();
                attribute.UserId = @this.Id;
                attribute.Relation = 1;
                attribute.List = "";
            }
            else
            {

            }
           
            return true;

        }

        public System.Collections.Generic.List<UserTable> GetListOfUsers()
        {
            return m_UserContext.UserTable.ToList();
        }

        public System.Collections.Generic.List<UserRelationTable> GetListOfUsersRelations()
        {
            return m_UserRelationContext.UserRelationTable.ToList();
        }

    }



}
