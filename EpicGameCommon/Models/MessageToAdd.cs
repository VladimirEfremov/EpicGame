namespace EpicGameCommon.Models
{
    public struct MessageToAdd
    {
        public int UserId { get; set; }
        public int CompanionId { get; set; }
        public Message Message { get; set; }
    }
}
