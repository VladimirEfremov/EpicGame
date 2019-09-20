namespace EpicGame.src.UserDB
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
                m_UserContext.UserTable.Add(user);
            }
            else
            {
                //Log.Warn: $"This user's nickname already exists in db! [id = {index + 1}]" 
            }
        }

        public void RegisterUserToTable(string firstName, string secondName,
            string passswordHash, string nickname, string email)
        {
            var userIndex = FindUserByEmail(email);
            if (userIndex == -1)
            {
                m_UserContext.UserTable.Add(new UserTable()
                {
                    FirstName = firstName,
                    SecondName = secondName,
                    FullName = $"{firstName} {secondName}",
                    PasswordHash = passswordHash,
                    Nickname = nickname,
                    Email = email
                });
                m_UserContext.SaveChanges();
            }
            else
            {
                //Log.Warn: $"This user's nickname already exists in db! [id = {index + 1}]" 
            }
        }

        public bool IsRegisterUser(string email, string passwordHash)
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
                    return i;
                }
            }
            return -1;
        }

        public System.Collections.Generic.List<UserTable> GetListOfUsers()
        {
            return m_UserContext.UserTable.ToList();
        }

        //AddUserToFriends(User this, User toAdd)
        //or
        //AddUserToRelationGroup(User this, User toAdd, RelationGroup group)

    }



}
