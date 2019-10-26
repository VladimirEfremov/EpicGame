using System.Web.Http;
using EpicGameCommon;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    public class GameController : ApiController
    {
        [Route("GetCoreById")]
        public string GetCoreById([FromBody]int coreId)
        {
            return RemoteProcedureCallClass
                .GetGameChannel()
                .GetCoreById(coreId);
        }

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
        [Route("GetCoreInfoById")]
        public string GetCoreInfoById([FromBody]int coreId)
        {
            return RemoteProcedureCallClass
                .GetGameChannel()
                .GetCoreInfoById(coreId);
        }
    }
}