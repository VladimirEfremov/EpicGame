namespace EpicGameWeb.Models.DBHelper
{
    public class UserFollowingTable
    {
        public int UserFollowingId { get; set; }
        public int UserId { get; set; }
        public int FollowingId { get; set; }
    }
}