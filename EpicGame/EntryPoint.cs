namespace EpicGame
{
    using System;
    using EpicGame.src.DBHelper;
    class EntryPoint
    {
        static void Main(string[] args)
        {
            Console.WriteLine("It works!");

            using (var db = new UserDBHelper())
            {
                bool run = true;
                while (run)
                {
                    var usersList = db.GetListOfUsers();
                    foreach (var el in usersList)
                    {
                        Console.WriteLine($"{el.Id} {el.FullName} {el.Nickname} {el.Email}");
                    }

                    var relationList = db.GetListOfUsersRelations();
                    foreach (var el in relationList)
                    {
                        Console.WriteLine($"{el.UserId} {el.Relation} {el.List}");
                    }

                    Console.WriteLine("1. Add user\n2. Remove user by id");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            {
                                Console.WriteLine();
                                Console.Write("FirstName: ");
                                var fist = Console.ReadLine();
                                Console.Write("SecondName: ");
                                var sec = Console.ReadLine();
                                Console.Write("Nickname: ");
                                var nick = Console.ReadLine();
                                Console.Write("Password: ");
                                var pass = Console.ReadLine();
                                Console.Write("Email: ");
                                var email = Console.ReadLine();

                                db.RegisterUserToTable(fist, sec, pass, nick, email);
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

                                break;
                            }
                        default:
                            run = false;
                            break;
                    }

                    

                }
            }


            Console.ReadKey();
        }
    }
}
