namespace EpicGame.src.DBHelper
{
    enum RelationType
    {
        None = 0,
        Friends,
        Followers,
        Following
    }

    static class RelationConverter
    {
        public static string ConvertToString(RelationType type)
        {
            switch(type)
            {
                case RelationType.Friends: return "Friends";
                case RelationType.Followers: return "Followers";
                case RelationType.Following: return "Following";
                default: return "None";
            }
        }
    }
}
