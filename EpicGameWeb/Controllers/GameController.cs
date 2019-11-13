using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using EpicGameCommon;
using EpicGameCommon.Models;
using EpicGameCommon.Response;
using EpicGameWeb.Models;
using EpicGameWeb.Models.DBHelper;

namespace EpicGameWeb.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        public string GetSessionVariable(string variableName)
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies(variableName).FirstOrDefault();
            if (cookie != null)
            {
                return cookie[variableName].Value;
            }
            return "";
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
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                var result = RemoteProcedureCallClass
                    .GetGameChannel()
                    .GetCoreById(coreid);
                return result;
            }
            return "";
        }

        [HttpGet]
        [Route("GetCoreInfoById")]
        public CoreInfo GetCoreInfoById()
        {
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                string coreInfoJson = RemoteProcedureCallClass
                    .GetGameChannel()
                    .GetCoreInfoById(coreid);
                CoreInfo coreInfo = coreInfoJson.FromJson<CoreInfo>();
                return coreInfo;
            }
            return new CoreInfo();
        }

        [HttpPost]
        [Route("DuelBattle")]
        public int DuelBattle([FromBody]object defenderCoreId)
        {
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                int defenderCoreIdInt;
                if (System.Int32.TryParse(defenderCoreId.ToString(), out defenderCoreIdInt))
                {
                    var resultJson = RemoteProcedureCallClass
                        .GetGameChannel()
                        .DuelBattle(coreid, defenderCoreIdInt);
                    var result = resultJson.FromJson<BattleResponse>().WhoWonTheBattle;
                    return result;
                }
            }
            return -2;
        }

        [HttpPost]
        [Route("CoreBattle")]
        public string CoreBattle([FromBody]int defenderCoreId)
        {
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                var result = RemoteProcedureCallClass
                    .GetGameChannel()
                    .CoreBattle(coreid, defenderCoreId)
                    .ToJson();
                return result;
            }
            return "";
        }

        [HttpPost]
        [Route("BaseAttackUpgrade")]
        public void BaseAttackUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .BaseAttackUpgrade(coreId);
        }

        [HttpPost]
        [Route("BaseDefenceUpgrade")]
        public void BaseDefenceUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .BaseDefenceUpgrade(coreId);
        }

        [HttpPost]
        [Route("BaseCapacityUpgrade")]
        public void BaseCapacityUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .BaseCapacityUpgrade(coreId);
        }

        [HttpPost]
        [Route("CasernCapacityUpgrade")]
        public void CasernCapacityUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .CasernCapacityUpgrade(coreId);
        }

        [HttpPost]
        [Route("GoldMiningCapacityUpgrade")]
        public void GoldMiningCapacityUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .GoldMiningCapacityUpgrade(coreId);
        }

        [HttpPost]
        [Route("DefenceTowerAttackUpgrade")]
        public void DefenceTowerAttackUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .DefenceTowerAttackUpgrade(coreId);
        }

        [HttpPost]
        [Route("DefenceTowerDefenceUpgrade")]
        public void DefenceTowerDefenceUpgrade([FromBody]int coreId)
        {
            RemoteProcedureCallClass
                .GetGameChannel()
                .DefenceTowerDefenceUpgrade(coreId);
        }

        [HttpGet]
        [Route("GetCoreRenderable")]
        public Renderable GetCoreRenderable()
        {
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                var result = RemoteProcedureCallClass
                    .GetGameChannel()
                    .GetCoreRenderable(coreid).FromJson<Renderable>();
                return result;
            }
            return new Renderable();
        }

        [HttpGet]
        [Route("GetOtherRenderable")]
        public Renderable[] GetOtherRenderable()
        {
            int coreid;
            var coreIdString = GetSessionVariable("CoreId");
            if (System.Int32.TryParse(coreIdString, out coreid))
            {
                var result = RemoteProcedureCallClass
                   .GetGameChannel()
                   .GetOtherRenderable(coreid).FromJson<Renderable[]>();
                return result;
            }
            return new Renderable[0];
        }

        [HttpGet]
        [Route("GetAllStats")]
        public StatInfo[] GetAllStats()
        {
            var resultJson = RemoteProcedureCallClass.GetGameChannel().GetAllStats();
            var result = resultJson.FromJson<StatInfo[]>();
            return result;
        }
    }
}