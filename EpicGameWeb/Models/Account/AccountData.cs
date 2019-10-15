using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AccountData
    {
        public string Nickname { get; set; }
        public string[] FriendsList { get; set; }
        public string[] FollowersList { get; set; }
        public string[] FollowingsList { get; set; }

        public AccountData(string nickname)
        {
            Nickname = nickname;
            FriendsList = new string[] { "fr1" };
            FollowersList = new string[] { "fo2" };
            FollowingsList = new string[] { "fol3" };
        }
    }
}