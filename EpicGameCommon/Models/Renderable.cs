using System;

namespace EpicGameCommon.Models
{
    public struct Renderable
    {
        public int SessionMapId { get; set; }
        public int CoreId { get; set; }
        public string Nickname { get; set; }
        public decimal MapX { get; set; }
        public decimal MapY { get; set; }
    }
}
