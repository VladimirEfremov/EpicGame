namespace EpicGame
{
    using System;
    using EpicGame.src.DBHelper;
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
                                Int32.TryParse(thisIdString, out thisIdInt);
                                Console.Write("Write userid [whom want to add]: ");
                                string idToAddString = Console.ReadLine();
                                Int32.TryParse(idToAddString, out idToAddInt);

                                db.AddUserToFriends(thisIdInt, idToAddInt);
                                break;
                            }
                        case ConsoleKey.D4:
                            {
                                Int32 thisIdInt;
                                Int32 idToAddInt;

                                Console.Write("Write userid [who want to remove]: ");
                                string thisIdString = Console.ReadLine();
                                Int32.TryParse(thisIdString, out thisIdInt);
                                Console.Write("Write userid [whom want to remove]: ");
                                string idToAddString = Console.ReadLine();
                                Int32.TryParse(idToAddString, out idToAddInt);

                                db.RemoveUserFromFriends(thisIdInt, idToAddInt);
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
                                var relationList = db.GetListOfUsersRelations();
                                foreach (var el in relationList)
                                {
                                    Console.WriteLine($"{el.UserId} " +
                                        $"{RelationConverter.ConvertToString((RelationType)el.Relation)} " +
                                        $"{el.List}");
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

        static void Main(string[] args)
        {
#if TRUE
            TestUserDB();
#else
            string str = "2 ";//"21 213 2232 2 3 32 43 ";
            int index = str.IndexOfId("2");
            index = str.IndexOfId("3");
            index = str.IndexOfId("32");
            index = str.IndexOfId("13");
            index = str.IndexOfId("4");
            index = str.IndexOfId("1");
#endif
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
