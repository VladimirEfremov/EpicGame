using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicGameCommon.Models
{
    public class StatInfo
    {
        public string Nickname { get; set; }
        public int SessionCoreId { get; set; }
        public int Rating { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int SuccessfullCoreAttacks { get; set; }
        public int NotSuccessfullCoreAttacks { get; set; }
        public int Scores { get; set; }
    }
}
