using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AccountData
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string[] FriendsList { get; set; }
        public string[] FollowersList { get; set; }
        public string[] FollowingsList { get; set; }

    }
}