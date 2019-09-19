using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EpicGame.src.UserDB;
using EpicGame.src.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace EpicGame
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            Console.WriteLine("It works!");
            new UserDBHelper();
            Console.ReadKey();
        }
    }
}
