using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpicGameWeb.Models.DBHelper
{
    public class UserFriendsTable
    {
        public int UserFriendId { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}