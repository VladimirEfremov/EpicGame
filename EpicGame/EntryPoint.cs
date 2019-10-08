using System;
using System.ServiceModel;
using EpicGame.src.Services;
using System.Web.Services.Description;

using EpicGame.src.DBHelper;
using EpicGame.src.GameDB;
using System.Collections.Generic;

namespace EpicGame
{

    public class GameEngine
    {

        public GameEngine()
        {
            //
        }

        // 0 - attackers win
        // 1 - defenders win
        public static int Battle(
            List<src.Models.Game.GameUnitsTable> attackers, 
            List<src.Models.Game.GameUnitsTable> defenders)
        {
            while (
                attackers.Count != 0 || 
                defenders.Count != 0)
            {
                foreach (var attacker in attackers)
                {
                    if (defenders.Count <= 0) { return 0; }
                    
                    defenders[0].GameUnitHP -= 
                        attacker.GameUnitAttack * (100 - defenders[0].GameUnitDefence);
                    if (defenders[0].GameUnitHP <= 0)
                    {
                        defenders.RemoveAt(0);
                        Console.WriteLine($"Delete defender[0]: [hp: {defenders[0].GameUnitHP}]");
                    }
                }

                foreach (var defender in defenders)
                {
                    if (attackers.Count <= 0) { return 1; }

                    attackers[0].GameUnitHP -=
                        defender.GameUnitAttack * (100 - attackers[0].GameUnitDefence);
                    if (attackers[0].GameUnitHP <= 0)
                    {
                        attackers.RemoveAt(0);
                        Console.WriteLine($"Delete attacker[0]: [hp: {attackers[0].GameUnitHP}]");
                    }
                }
            }
            return -1;
        }

    }


    class EntryPoint
    {

        static Random rand = new Random();

        private delegate bool RegisterationDelegate(string s1, string s2, string s3, string s4, string s5);
        private static string RandomUserRegistration(RegisterationDelegate registerationDelegate)
        {
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
            registerationDelegate(fName, sName, "123", nick, email);
            return nick;
        }

        private static void TestUserDB()
        {
            Console.WriteLine("It works!");

            using (var db = new UserDBHelper())
            {
                bool run = true;
                while (run)
                {
                    Console.WriteLine(
                        "1. Add user\n" +
                        "2. Remove user by id\n" +
                        "3. Add to friends\n" +
                        "4. Remove from friends\n" +
                        "5. Print users\n6. Print relations");
                    var key = Console.ReadKey().Key;
                    Console.WriteLine();
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            {
                                Console.WriteLine();
                                RandomUserRegistration(db.RegisterUserToTable);
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
                                        Console.Write("Write userid [whom want to add]: ");
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
                                    Console.WriteLine($"{el.UserId} {el.FullName} {el.Nickname} {el.Email}");
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

        private static void TestGameDB()
        {
            using (var gamedb = new GameDBHelper())
            {
                bool run = true;
                while (run)
                {
                    Console.WriteLine(
                        "1. Show all\n" +
                        "2. Show battle\n" + 
                        "3. Random registration\n" +
                        "4. Show all cores\n"
                        );
                    var key = Console.ReadKey().Key;
                    Console.WriteLine();
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            {
                                    Console.WriteLine();
                                    var unitsList = gamedb.GetAllGameUnitsList();
                                    Console.WriteLine("units:");
                                    foreach (var el in unitsList)
                                    {
                                        Console.WriteLine($"id: {el.GameUnitId} name: {el.GameUnitName}");
                                    }

                                    var unitsTypeList = gamedb.GetAllGameUnitsTypeList();
                                    Console.WriteLine("unit types:");
                                    foreach (var el in unitsTypeList)
                                    {
                                        Console.WriteLine($"id: {el.GameUnitTypeId} name: {el.GameUnitTypeName}");
                                    }

                                    var buildingsList = gamedb.GetAllGameBuildingsList();
                                    Console.WriteLine("buildings list:");
                                    foreach (var el in buildingsList)
                                    {
                                        Console.WriteLine($"id: {el.GameBuildingId} name: {el.GameBuildingName}");
                                    }

                                    var buildingsTypeList = gamedb.GetAllGameBuildingsTypeList();
                                    Console.WriteLine("buildings list:");
                                    foreach (var el in buildingsTypeList)
                                    {
                                        Console.WriteLine($"id: {el.GameBuildingTypeId} name: {el.GameBuildingTypeName}");
                                    }

                                    var buildingsProdList = gamedb.GetAllGameBuildingProductionList();
                                    Console.WriteLine("buildings list:");
                                    foreach (var el in buildingsProdList)
                                    {
                                        Console.WriteLine($"id: {el.GameBuildingId} " +
                                            $"building id: {el.GameBuildingProductionTableId} " +
                                            $"unit to produce: {el.GameUnitId}");
                                    }
                                    break;
                                }
                        case ConsoleKey.D2:
                            {
                                    Console.WriteLine();


                                    Console.WriteLine("Battle begins");
                                    List<src.Models.Game.GameUnitsTable> attackers =
                                        new List<src.Models.Game.GameUnitsTable>();
                                    List<src.Models.Game.GameUnitsTable> defenders =
                                        new List<src.Models.Game.GameUnitsTable>();

                                    var result = GameEngine.Battle(attackers, defenders);
                                    Console.WriteLine(result == 0 ? 
                                        "Attackers won the battle!" : 
                                        "Defenders won the battle!");
                                    break;
                                }
                        case ConsoleKey.D3:
                            {
                                using (var userdb = new UserDBHelper())
                                {
                                    var nick = RandomUserRegistration(userdb.RegisterUserToTable);
                                    var id = userdb.FindUserByNickname(nick);
                                    GameDBHelper.GenerateCoreForUser(id);
                                }
                                break;
                            }
                        case ConsoleKey.D4:
                            {
                                Console.WriteLine();
                                Console.WriteLine("Cores: ");
                                
                                var coreList = gamedb.GetAllCores();
                                foreach (var el in coreList)
                                {
                                    var elmap = gamedb.FindCoreMapByMapId(el.CoreMapId);
                                    Console.WriteLine(
                                        $"userid: {el.UserId} " +
                                        $"mapid: {el.CoreMapId}" +
                                        $"map [{elmap.XCoord}, {elmap.YCoord}]");
                                }
                                break;
                            }
                        case ConsoleKey.D5:
                            {
                                Console.Clear();
                                Console.WriteLine("Users:");
                            
                                break;
                            }
                        case ConsoleKey.D6:
                            {
                                Console.Clear();
                                Console.WriteLine("\nRelations:");
                            
                                Console.WriteLine();
                                break;
                            }
                        case ConsoleKey.C: { Console.Clear(); break; }
                        case ConsoleKey.Escape: { run = false; break; }
                        default: { break; }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
#if FALSE
            Uri address = new Uri("http://127.0.0.1:2001/IServiceUserDBHelper");
            BasicHttpBinding binding = new BasicHttpBinding();
            Type contract = typeof(IServiceUserDBHelper);
            using (ServiceHost host = new ServiceHost(typeof(ServiceUserDBHelper)))
            {
                host.AddServiceEndpoint(contract, binding, address);
                host.Open();

                logger.Info("Service is open!");
                Console.ReadKey();

                host.Close();
            }
#else

            //TestUserDB();

            TestGameDB();

#endif
            logger.Info("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
