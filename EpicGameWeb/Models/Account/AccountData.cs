using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AccountData
    {
        public int UserId { get; set; }
        public int CoreId { get; set; }
        public string Nickname { get; set; }
    }
}