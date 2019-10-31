using System;

namespace EpicGameCommon.Response
{
    public struct BattleResponse
    {
        public int WhoWonTheBattle { get; set; }
        public string Message { get; set; }
    }
}
