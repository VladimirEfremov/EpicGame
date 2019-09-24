using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public class LoginModel
    {
        public string Email { get;set; }
        public string PasswordHash { get; set; }
    }
}