using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpicGameWeb.Models.DBHelper
{
    public class UserTable
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}