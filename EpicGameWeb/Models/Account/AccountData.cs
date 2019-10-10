using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AccountData
    {
        public string Nickname { get; set; }
        public string[] FriendList { get; set; }

        public AccountData( string nickname)
        {
            Nickname = nickname;
            FriendList = new string[] { "Nobody is ur friend!" };
        }
    }
}