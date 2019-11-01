using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicGame.Game
{

    public enum LogType
    {
        None, 
        BattleSuccess, BattleFailure, BattleBeenAttacked,
        ProduceSuccess, ProduceFailure,
        Communication
    }

    public struct Log
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
    }

    public struct LogRequest
    {
        public int UserId { get; set; }
        public int LastClientLogId { get; set; }
    }

    public struct LogResponse
    {
        public int UserId { get; set; }
        public Log LogToAdd { get; set; }
    }

    public static class EventLogger
    {
        private static Dictionary<int, List<Log>> s_LogData;
        public static int s_NextLogId { get; private set; }
        static EventLogger()
        {
            s_LogData = new Dictionary<int, List<Log>>();
        }

        public static Log[] GetAllUserLogData(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                return s_LogData[request.UserId].ToArray();
            }
            return null;
        }

        public static Log[] GetAllUserBattleEvents(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                var result = s_LogData[request.UserId]
                    .Where(obj =>
                        (obj.Type == LogType.BattleBeenAttacked) ||
                        (obj.Type == LogType.BattleFailure) ||
                        (obj.Type == LogType.BattleSuccess))
                    .ToArray();
                return result;
            }
            return null;
        }

        public static Log[] GetAllUserProduceEvents(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                var result = s_LogData[request.UserId]
                    .Where(obj =>
                        (obj.Type == LogType.ProduceFailure) ||
                        (obj.Type == LogType.ProduceSuccess))
                    .ToArray();
                return result;
            }
            return null;
        }

        public static Log[] GetAllUserCommunicationEvents(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                var result = s_LogData[request.UserId]
                    .Where(obj => (obj.Type == LogType.Communication))
                    .ToArray();
                return result;
            }
            return null;
        }

        public static Log[] UpdateLogData(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                var result = s_LogData[request.UserId]
                    .Where(obj => obj.Id > request.LastClientLogId)
                    .ToArray();
                return result;
            }
            return null;
        }

        public static void AddLogForUser(int userId, string message)
        {
            var log = new Log();
            log.Id = s_NextLogId;
            log.Message = message;
            log.Time = DateTime.Now.ToShortTimeString();

            if (s_LogData.ContainsKey(userId))
            {
                s_LogData[userId].Add(log);
                ++s_NextLogId;
            }
            else 
            {
                var list = new List<Log>() { log };
                s_LogData.Add(userId, list);
                ++s_NextLogId;
            }
        }

    }
}
