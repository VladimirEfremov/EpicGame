using System.Web;
using System.Web.Http;
using EpicGameCommon;
using EpicGameCommon.Response;
using EpicGameWeb.Models;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        [Route("CasernGetNumberOfWarriors")]
        public int CasernGetNumberOfWarriors([FromBody]int coreId)
        {
            return RemoteProcedureCallClass
                .GetGameChannel()
                .CasernGetNumberOfWarriors(coreId);
        }

        [Route("CasernGetNumberOfAttackAircraft")]
        public int CasernGetNumberOfAttackAircraft([FromBody]int coreId)
        {
            return RemoteProcedureCallClass
                .GetGameChannel()
                .CasernGetNumberOfAttackAircraft(coreId);
        }

        [Route("CoreBuildCasern")]
        public void CoreBuildCasern([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CoreBuildCasern(coreId);
        }

        [Route("CoreBuildGoldMining")]
        public void CoreBuildGoldMining([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CoreBuildGoldMining(coreId);
        }

        [Route("CoreBuildDefenceTower")]
        public void CoreBuildDefenceTower([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CoreBuildDefenceTower(coreId);
        }

        [HttpPost]
        [Route("BaseProduceWorker")]
        public void BaseProduceWorker([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .BaseProduceWorker(coreId);
        }

        [Route("CasernProduceWarrior")]
        public void CasernProduceWarrior([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CasernProduceWarrior(coreId);
        }

        [Route("CasernProduceAttackAircraft")]
        public void CasernProduceAttackAircraft([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CasernProduceAttackAircraft(coreId);
        }

        [Route("GoldMiningAddWorker")]
        public void GoldMiningAddWorker([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .GoldMiningAddWorker(coreId);
        }

        [HttpGet]
        [Route("GetCoreById")]
        public string GetCoreById()
        {
            var result = RemoteProcedureCallClass
                .GetGameChannel()
                .GetCoreById(MySession.CoreId);
            return result;
        }

        [HttpGet]
        [Route("GetCoreInfoById")]
        public CoreInfo GetCoreInfoById()
        {
            string coreInfoJson = RemoteProcedureCallClass
                .GetGameChannel()
                .GetCoreInfoById(MySession.CoreId);
            CoreInfo coreInfo = coreInfoJson.FromJson<CoreInfo>();
            return coreInfo;
        }

        [HttpPost]
        [Route("DuelBattle")]
        public int DuelBattle([FromBody]object defenderCoreId)
        {
            int defenderCoreIdInt; 
            if (System.Int32.TryParse(defenderCoreId.ToString(), out defenderCoreIdInt))
            {
                var resultJson = RemoteProcedureCallClass
                    .GetGameChannel()
                    .DuelBattle(MySession.CoreId, defenderCoreIdInt);
                var result = resultJson.FromJson<BattleResponse>().WhoWonTheBattle;
                return result;
            }
            return -2;
        }

        [HttpPost]
        public string CoreBattle([FromBody]int defenderCoreId)
        {
            var result = RemoteProcedureCallClass
                .GetGameChannel()
                .CoreBattle(MySession.CoreId, defenderCoreId)
                .ToJson();
            return result;
        }

    }
}