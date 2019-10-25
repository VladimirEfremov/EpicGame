﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using EpicGame.src.Models.Game;
using EpicGame.src.Services;
using EpicGameCommon;

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
            List<GameUnitsTable> attackers, 
            List<GameUnitsTable> defenders)
        {
            void lShowArmy(
                string name,
                List<GameUnitsTable> army)
            {
                int index = 0;
                Console.WriteLine($"Show {name} army:");
                foreach (var unit in army)
                {
                    Console.WriteLine($"{index}. {unit.GameUnitName.Trim(' ')} " +
                        $"[attack: {unit.GameUnitAttack}, " +
                        $"hp: {unit.GameUnitHP}]");
                    ++index;
                }
            }

            lShowArmy("attackers", attackers);
            lShowArmy("defenders", defenders);

            while (true)
            {
                if (defenders.Count <= 0 && attackers.Count > 0) { return 0; }
                if (attackers.Count <= 0 && defenders.Count > 0) { return 1; }
                if (attackers.Count <= 0 && defenders.Count <= 0) { return -1; }

                foreach (var attacker in attackers)
                {
                    var tempDamage = attacker.GameUnitAttack * (100 - defenders[0].GameUnitDefence) / 100;
                    defenders[0].GameUnitHP -= tempDamage;
                    if (defenders[0].GameUnitHP <= 0)
                    {
                        Console.WriteLine(
                            $"Defender {defenders[0].GameUnitName.Trim(' ')} " +
                            $"[hp:{defenders[0].GameUnitHP}] was destroyed!");

                        defenders.RemoveAt(0);
                        if (defenders.Count <= 0 && attackers.Count > 0) 
                        {
                            Console.WriteLine("Defenders been fully destroyed!");
                            return 0; 
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"defender been attacked ({tempDamage})");
                    }
                }

                foreach (var defender in defenders)
                {
                    var tempDamage = defender.GameUnitAttack * (100 - attackers[0].GameUnitDefence) / 100;
                    attackers[0].GameUnitHP -= tempDamage;
                    if (attackers[0].GameUnitHP <= 0)
                    {
                        Console.WriteLine(
                            $"Attacker {attackers[0].GameUnitName.Trim(' ')} " +
                            $"[hp:{attackers[0].GameUnitHP}] was destroyed!");
                        
                        attackers.RemoveAt(0);
                        if (attackers.Count <= 0 && defenders.Count > 0) 
                        {
                            Console.WriteLine("Attackers been fully destroyed!");
                            return 1; 
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"attacker been attacked ({tempDamage})");
                    }
                }

                lShowArmy("attackers", attackers);
                lShowArmy("defenders", defenders);
            }
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
                        "5. Print users\n" +
                        "6. Print relations\n" + 
                        "7. Print user info");
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
                                var usersList = db.GetAllUsers();
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
                                var friends = db.GetUsersFriendsTable(1);
                                Console.WriteLine("Frineds: ");
                                foreach (var friend in friends)
                                {
                                    Console.WriteLine($"{friend.UserId} - {friend.FriendId}");
                                }

                                var followers = db.GetUsersFollowersTable(1);
                                Console.WriteLine("Followers: ");
                                foreach (var follower in followers)
                                {
                                    Console.WriteLine($"{follower.UserId} - {follower.FollowerId}");
                                }

                                var followings = db.GetUsersFollowingsTable(1);
                                Console.WriteLine("Followings: ");
                                foreach (var following in followings)
                                {
                                    Console.WriteLine($"{following.UserId} - {following.FollowingId}");
                                }
                                Console.WriteLine();
                                break;
                            }
                        case ConsoleKey.D7:
                            {
                                var usersInfo = db.GetAllUsersInfo();
                                //foreach (var user in usersInfo)
                                //{
                                //    Console.WriteLine($"{user.Nickname} {user.UserId} {user.CoreId}");
                                //}
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
                List<GameUnitsTable> GetCoreArmy(int coreId)
                {
                    var result = new List<GameUnitsTable>();

                    var warriorUnit = gamedb.GetUnitProperty("Warrior");
                    var attackAircraftUnit = gamedb.GetUnitProperty("AttackAircraft");

                    var numberWarriors = gamedb.CasernGetNumberOfWarriors(coreId);
                    var numberAttackAircraft = gamedb.CasernGetNumberOfAttackAircraft(coreId);

                    for (int i = 0; i < numberWarriors; i++)
                    {
                        result.Add(
                            new GameUnitsTable()
                            {
                                GameUnitId = warriorUnit.GameUnitId,
                                GameUnitType = warriorUnit.GameUnitType,
                                GameUnitName = warriorUnit.GameUnitName,
                                GameUnitHP = warriorUnit.GameUnitHP,
                                GameUnitAttack = warriorUnit.GameUnitAttack,
                                GameUnitDefence = warriorUnit.GameUnitDefence,
                                GameUnitGoldIncome = warriorUnit.GameUnitGoldIncome,
                                GameUnitGoldOutcome = warriorUnit.GameUnitGoldOutcome
                            });
                    }
                    for (int i = 0; i < numberAttackAircraft; i++)
                    {
                        result.Add(new GameUnitsTable()
                        {
                            GameUnitId = attackAircraftUnit.GameUnitId,
                            GameUnitType = attackAircraftUnit.GameUnitType,
                            GameUnitName = attackAircraftUnit.GameUnitName,
                            GameUnitHP = attackAircraftUnit.GameUnitHP,
                            GameUnitAttack = attackAircraftUnit.GameUnitAttack,
                            GameUnitDefence = attackAircraftUnit.GameUnitDefence,
                            GameUnitGoldIncome = attackAircraftUnit.GameUnitGoldIncome,
                            GameUnitGoldOutcome = attackAircraftUnit.GameUnitGoldOutcome
                        });
                    }

                    return result;
                }

                bool run = true;
                while (run)
                {
                    Console.WriteLine(
                        "1. Show all\n" +
                        "2. Show battle\n" + 
                        "3. Random registration\n" +
                        "4. Show all cores\n" + 
                        "5. Build casern test\n" + 
                        "6. Core info\n"
                        );
                    var key = Console.ReadKey().Key;
                    Console.WriteLine();
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            {
                                    //Console.WriteLine();
                                    //var unitsList = gamedb.GetAllGameUnitsList();
                                    //Console.WriteLine("units:");
                                    //foreach (var el in unitsList)
                                    //{
                                    //    Console.WriteLine($"id: {el.GameUnitId} name: {el.GameUnitName}");
                                    //}
                                    //
                                    //var unitsTypeList = gamedb.GetAllGameUnitsTypeList();
                                    //Console.WriteLine("unit types:");
                                    //foreach (var el in unitsTypeList)
                                    //{
                                    //    Console.WriteLine($"id: {el.GameUnitTypeId} name: {el.GameUnitTypeName}");
                                    //}
                                    //
                                    //var buildingsList = gamedb.GetAllGameBuildingsList();
                                    //Console.WriteLine("buildings list:");
                                    //foreach (var el in buildingsList)
                                    //{
                                    //    Console.WriteLine($"id: {el.GameBuildingId} name: {el.GameBuildingName}");
                                    //}
                                    //
                                    //var buildingsTypeList = gamedb.GetAllGameBuildingsTypeList();
                                    //Console.WriteLine("buildings list:");
                                    //foreach (var el in buildingsTypeList)
                                    //{
                                    //    Console.WriteLine($"id: {el.GameBuildingTypeId} name: {el.GameBuildingTypeName}");
                                    //}
                                    //
                                    //var buildingsProdList = gamedb.GetAllGameBuildingProductionList();
                                    //Console.WriteLine("buildings list:");
                                    //foreach (var el in buildingsProdList)
                                    //{
                                    //    Console.WriteLine($"id: {el.GameBuildingId} " +
                                    //        $"building id: {el.GameBuildingProductionTableId} " +
                                    //        $"unit to produce: {el.GameUnitId}");
                                    //}
                                    break;
                                }
                        case ConsoleKey.D2:
                            {
                                Console.WriteLine();

                                Console.WriteLine("Battle begins");

                                List<GameUnitsTable> attackers = GetCoreArmy(1);
                                List<GameUnitsTable> defenders = GetCoreArmy(2);

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
                                    //GameDBHelper.GenerateCoreForUser(id);
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
                                        $"mapid: {el.CoreMapId} " 
                                        //+$"map [{elmap.XCoord}, {elmap.YCoord}]"
                                        );
                                }
                                break;
                            }
                        case ConsoleKey.D5:
                            {
                                Console.Clear();
                                Console.WriteLine("Build casern");
                                gamedb.CoreBuildCasern(1);
                                Console.Read();
                                break;
                            }
                        case ConsoleKey.D6:
                            {
                                Console.Clear();
                                Console.WriteLine("Core info");
                                using (var context = new UserDBHelper())
                                {
                                    
                                    var list = context.GetAllUsers();
                                    foreach (var el in list)
                                    {
                                        var coreId = UserDBHelper.GetCoreIdByUserId(el.UserId);
                                        if (coreId != -1)
                                        {
                                            var coreInfo = gamedb
                                                .GetCoreInfoById(coreId)
                                                .FromJson<CoreInfo>();
                                            Console.WriteLine($"Core info [UserId={el.UserId}]");
                                            Console.WriteLine($"coreid: {coreInfo.CoreId} " +
                                                $"money: {coreInfo.Money}" +
                                                $"base capacity: {coreInfo.BaseCapacity}");
                                        }
                                        else
                                        {
                                            Console
                                                .WriteLine($"Can not find core id for userid: {el.UserId}");
                                        }
                                    }
                                    Console.WriteLine();
                                }
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
#if FALSE //TRUE FALSE

#if OLD
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
#endif

            Uri address = new Uri("http://127.0.0.1:2001/IServiceBase");
            BasicHttpBinding binding = new BasicHttpBinding();
            Type contract = typeof(IServiceBase);
            using (ServiceHost host = new ServiceHost(typeof(ServiceBase)))
            {
                host.AddServiceEndpoint(contract, binding, address);
                host.Open();

                logger.Info("Service is open!");
                Console.ReadKey();

                host.Close();
            }

#else
            //([system.reflection.assembly]::loadfile("E:\General\Programming\C_Sharp\EpamPractice\EpicGame\EpicGame\EpicGameCommon\bin\Debug\EpicGameCommon.dll")).FullName
            TestUserDB();

            //TestGameDB();

#endif
            logger.Info("Press any key to continue ...");
            Console.ReadKey();
        }

    }
}
