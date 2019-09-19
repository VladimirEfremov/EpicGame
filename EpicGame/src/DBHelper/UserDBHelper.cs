namespace EpicGame.src.UserDB
{
    using System.Linq;
    using EpicGame.src.Models;

    class UserDBHelper 
    {
        public UserDBHelper()
        {
            using (var context = new UserTableEntity())
            {
                var list = context.UserTable.ToList();
                //var user = context.UserTable.Find(1);
                //if (user != null) context.UserTable.Remove(user);
                context.UserTable.Add(new UserTable()
                {
                    FirstName = "Vova",
                    SecondName = "Efremov",
                    FullName = "Vova Efremov",
                    PasswordHash = "123",
                    Nickname = "vez",
                    Email = "VE@yandex.com"
                });
                context.SaveChanges();
                foreach (var el in list)
                {
                    System.Console.WriteLine($"{el.Id} {el.FirstName} {el.SecondName} {el.FullName} {el.Nickname} {el.Email}");
                }
            }
        }
    }



}
