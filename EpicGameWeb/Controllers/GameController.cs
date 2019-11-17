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
                //critical bug
                try
                {
                    var result = RemoteProcedureCallClass
                        .GetGameChannel()
                        .CoreBattle(coreid, defenderCoreId)
                        .ToJson();
                    return result;
                }
                catch
                {
                }
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

        [HttpPost]
        [Route("GetDialogForUser")]
        public MessageInfo[] GetDialogForUser([FromBody]int companionId)
        {
            int userId;
            string userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var resultJson = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetDialogForUser(new DialogId() { UserId = userId, CompanionId = companionId});
                var result = resultJson.FromJson<MessageInfo[]>();
                return result;
            }
            return new MessageInfo[0];
        }

        [HttpGet]
        [Route("GetDialogButtonInfo")]
        public DialogButtonInfo[] GetDialogButtonInfo()
        {
            int userId;
            var userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var resultJson = RemoteProcedureCallClass.GetBaseChannel().GetDialogButtonInfo(userId);
                var result = resultJson.FromJson<DialogButtonInfo[]>();
                return result;
            }
            return new DialogButtonInfo[0];
        }

        [HttpPost]
        [Route("SendMessage")]
        public void SendMessage(SendMessageStruct toSend)
        {
            int userId;
            var userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                RemoteProcedureCallClass.GetBaseChannel()
                    .SendMessage(
                    new MessageToAdd()
                    {
                        UserId = userId,
                        CompanionId = toSend.CompanionId,
                        Message = new Message() { Content = toSend.Message, Time = System.DateTime.Now }
                    });
            }
        }

        [HttpGet]
        [Route("IsEventStackUpdated")]
        public int IsEventStackUpdated()
        {
            int userId;
            var userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var isEventStackUpdated = RemoteProcedureCallClass.GetBaseChannel().IsEventStackUpdated(userId);
                return isEventStackUpdated;
            }
            return -1;
        }

        [HttpGet]
        [Route("GetAllEvents")]
        public EventType[] GetAllEvents()
        {
            int userId;
            var userIdString = GetSessionVariable("UserId");
            if (System.Int32.TryParse(userIdString, out userId))
            {
                var resultJson = RemoteProcedureCallClass
                    .GetBaseChannel()
                    .GetAllEvents(userId);
                EventType[] result = 
                    resultJson?.FromJson<EventType[]>();
                return result;
            }
            return new EventType[0];
        }

    }
}