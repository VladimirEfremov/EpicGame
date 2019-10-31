using System.Web.Http;
using EpicGameCommon;
using EpicGameCommon.Response;
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

        [HttpPost]
        [Route("GetCoreById")]
        public string GetCoreById([FromBody]string coreInfo)
        {
            CoreInfo core = coreInfo.FromJson<CoreInfo>();
            if (core.CoreId > 0)
            {
                return RemoteProcedureCallClass
                    .GetGameChannel()
                    .GetCoreById(core.CoreId);
            }
            return new CoreInfo().ToJson();
        }

        [HttpGet]
        public string GetCoreInfoById()
        {
            var s = AuthController.UserId;
            string coreInfoJson = RemoteProcedureCallClass
                .GetGameChannel()
                .GetCoreInfoById(2015);
            return coreInfoJson;
        }

        [HttpPost]
        [Route("DuelBattle")]
        public string DuelBattle([FromBody]string data)
        {
            var result = RemoteProcedureCallClass
                .GetGameChannel()
                .DuelBattle(AuthController.CoreId, System.Int32.Parse(data))
                .ToJson();
            return result;
        }

        [HttpPost]
        public string CoreBattle([FromBody]string defenderCoreId)
        {
            int defenderCoreIdInt;
            if (System.Int32.TryParse(defenderCoreId, out defenderCoreIdInt))
            {
                var result = RemoteProcedureCallClass
                    .GetGameChannel()
                    .CoreBattle(AuthController.CoreId, defenderCoreIdInt)
                    .ToJson();
                return result;
            }
            return "";
        }

    }
}