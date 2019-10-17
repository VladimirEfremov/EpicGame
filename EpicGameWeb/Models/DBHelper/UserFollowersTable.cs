namespace EpicGameWeb.Models.DBHelper
{
    public class UserFollowersTable
    {
        public int UserFollowersId { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }
    }
}