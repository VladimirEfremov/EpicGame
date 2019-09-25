using System;
using System.ServiceModel;
using EpicGame.src.DBHelper;
using EpicGame.src.Services;
using System.Web.Services.Description;

namespace EpicGame
{
    class EntryPoint
    {
        static Random rand = new Random();

        private static void TestUserDB()
        {
            Console.WriteLine("It works!");

            var names = new string[]
            {
                "Vladimir", "Konstantin", "Victor", "Dmitriy",
                "Aleksei", "Anton", "Alex", "Klim", "Stas", "Ivan"
            };

            var sunnames = new string[]
            {
                "Efremov", "Kalugin", "Antonov", "Balabanov", "Alimov",
                "Aglicev", "Agopov", "Agranov", "Agrenev", "Mavrodi"
            };

            var emails = new string[]
            {
                "@gmail.com", "@yandex.com"
            };

            using (var db = new UserDBHelper())
            {
                bool run = true;
                while (run)
                {
                    Console.WriteLine("1. Add user\n2. Remove user by id\n3. Add to friends\n" +
                        "4. Remove from friends\n" +
                        "5. Print users\n6. Print relations");
                    var key = Console.ReadKey().Key;
                    Console.WriteLine();
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            {
                                Console.WriteLine();
                                //Console.Write("FirstName: ");
                                //var fist = Console.ReadLine();
                                //Console.Write("SecondName: ");
                                //var sec = Console.ReadLine();
                                //Console.Write("Nickname: ");
                                //var nick = Console.ReadLine();
                                //Console.Write("Password: ");
                                //var pass = Console.ReadLine();
                                //Console.Write("Email: ");
                                //var email = Console.ReadLine();

                                var fName = names[rand.Next(0, names.Length)];
                                var sName = sunnames[rand.Next(0, sunnames.Length)];
                                var r = rand.Next(1, 1000);
                                var email = fName + sName +
                                    ((r < 300) ? "_" :
                                    (r >= 300 && r <= 600) ? "+" : "-")
                                    + rand.Next(0, 2000).ToString()
                                    + emails[rand.Next(0, 1)];
                                var nick =
                                    $"{fName}{sName[0]}{sName[rand.Next(1, sName.Length - 2)]}_{rand.Next(0, 2000)}";
                                db.RegisterUserToTable(fName, sName, "123", nick, email);
                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                Console.WriteLine();
                                System.Int32 id;
                                Console.Write("Write id to delete: ");
                                var idStr = Console.ReadLine();
                                if (Int32.TryParse(idStr, out id))
                                {
                                    db.RemoveUserByIdFromTable(id);
                                }
                                break;
                            }
                        case ConsoleKey.D3:
                            {
                                Int32 thisIdInt;
                                Int32 idToAddInt;

                                Console.Write("Write userid [who want to add]: ");
                                string thisIdString = Console.ReadLine();
                                if (thisIdString.Length > 0)
                                {
                                    if (Int32.TryParse(thisIdString, out thisIdInt))
                                    {
                                        Console.Write("Write userid [whom want to remove]: ");
                                        string idToAddString = Console.ReadLine();
                                        if (idToAddString.Length > 0)
                                        {
                                            if (Int32.TryParse(idToAddString, out idToAddInt))
                                            {
                                                if (thisIdInt != 0 && idToAddInt != 0)
                                                {
                                                    db.AddUserToFriends(thisIdInt, idToAddInt);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Can't pasrse idToAddString: {idToAddString}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Can't pasrse thisIdString: {thisIdString}");
                                    }
                                }
                                break;
                            }
                        case ConsoleKey.D4:
                            {
                                Int32 thisIdInt;
                                Int32 idToRemoveInt;

                                Console.Write("Write userid [who want to remove]: ");
                                string thisIdString = Console.ReadLine();
                                if (thisIdString.Length > 0)
                                {
                                    if (Int32.TryParse(thisIdString, out thisIdInt))
                                    {
                                        Console.Write("Write userid [whom want to remove]: ");
                                        string idToRemoveString = Console.ReadLine();
                                        if (idToRemoveString.Length > 0)
                                        {
                                            if (Int32.TryParse(idToRemoveString, out idToRemoveInt))
                                            {
                                                if (thisIdInt != 0 && idToRemoveInt != 0)
                                                {
                                                    db.RemoveUserFromFriends(thisIdInt, idToRemoveInt);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Can't pasrse idToAddString: {idToRemoveString}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Can't pasrse thisIdString: {thisIdString}");
                                    }
                                }
                                break;
                            }
                        case ConsoleKey.D5:
                            {
                                Console.Clear();
                                Console.WriteLine("Users:");
                                var usersList = db.GetListOfUsers();
                                foreach (var el in usersList)
                                {
                                    Console.WriteLine($"{el.Id} {el.FullName} {el.Nickname} {el.Email}");
                                }
                                break;
                            }
                        case ConsoleKey.D6:
                            {
                                Console.Clear();
                                Console.WriteLine("\nRelations:");
                                var friends = db.GetListOfUserFriendsTable();
                                Console.WriteLine("Frineds: ");
                                foreach (var friend in friends)
                                {
                                    Console.WriteLine($"{friend.UserId} - {friend.FriendId}");
                                }

                                var followers = db.GetListOfUserFollowersTable();
                                Console.WriteLine("Followers: ");
                                foreach (var follower in followers)
                                {
                                    Console.WriteLine($"{follower.UserId} - {follower.FollowerId}");
                                }

                                var followings = db.GetListOfUserFollowingsTable();
                                Console.WriteLine("Followings: ");
                                foreach (var following in followings)
                                {
                                    Console.WriteLine($"{following.UserId} - {following.FollowingId}");
                                }
                                Console.WriteLine();
                                break;
                            }
                        case ConsoleKey.C: { Console.Clear(); break; }
                        case ConsoleKey.Escape: { run = false; break; }
                        default: {  break; }
                    }
                }
            }
        }

        class MStruct
        {
            int id;
            int relation;
            string list;
        }

        static void Main(string[] args)
        {
#if FALSE
            Uri address = new Uri("http://127.0.0.1:2001/IServiceUserDBHelper");
            BasicHttpBinding binding = new BasicHttpBinding();
            Type contract = typeof(IServiceUserDBHelper);
            ServiceHost host = new ServiceHost(typeof(ServiceUserDBHelper));
            host.AddServiceEndpoint(contract, binding, address);
            host.Open();

            Console.WriteLine("Service is open!");
            Console.ReadKey();

#else
            TestUserDB();

#endif
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
