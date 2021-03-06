﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicGame.Game
{

    public enum LogType
    {
        None = 0, 
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
    }

    public struct LogResponse
    {
        public int UserId { get; set; }
        public Log LogToAdd { get; set; }
    }

    public static class EventLogger
    {

        private struct UpdateLog
        {
            public bool IsUpdated { get; set; }
            public int LastClientLogId { get; set; }
        }

        private static Dictionary<int, List<Log>> s_LogData;
        private static Dictionary<int, UpdateLog> s_UpdateFlags;
        public static int s_NextLogId { get; private set; }
        static EventLogger()
        {
            s_LogData = new Dictionary<int, List<Log>>();
            s_UpdateFlags = new Dictionary<int, UpdateLog>();
        }

        public static bool IsLogUpdated(int userId)
        {
            if (s_UpdateFlags.ContainsKey(userId))
            {
                return s_UpdateFlags[userId].IsUpdated;
            }
            return false;
        }

        private static int MaxLogIdForUser(int userId)
        {
            int prevMaxId;
            var logDataArray = s_LogData[userId].ToArray();
            if (logDataArray != null)
            {
                if (logDataArray.Length != 0)
                {
                    prevMaxId = logDataArray[0].Id;
                    for (int i = 1; i < logDataArray.Length; i++)
                    {
                        if (logDataArray[i].Id > prevMaxId)
                        {
                            prevMaxId = logDataArray[i].Id;
                        }
                    }
                }
                else
                {
                    prevMaxId = 0;
                }
            }
            else
            {
                prevMaxId = 0;
            }

            return prevMaxId;
        }

        public static Log[] GetAllUserLogData(LogRequest request)
        {
            if (s_LogData.ContainsKey(request.UserId))
            {
                if (s_UpdateFlags.ContainsKey(request.UserId))
                {
                    s_UpdateFlags[request.UserId] = new UpdateLog { 
                        IsUpdated = false, 
                        LastClientLogId = MaxLogIdForUser(request.UserId)
                    };
                }

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
                if (s_UpdateFlags.ContainsKey(request.UserId))
                {
                    s_UpdateFlags[request.UserId] = new UpdateLog
                    {
                        IsUpdated = false,
                        LastClientLogId = s_UpdateFlags[request.UserId].LastClientLogId
                    };
                }
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
                if (s_UpdateFlags.ContainsKey(request.UserId))
                {
                    s_UpdateFlags[request.UserId] = new UpdateLog
                    {
                        IsUpdated = false,
                        LastClientLogId = s_UpdateFlags[request.UserId].LastClientLogId
                    };
                }
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
                if (s_UpdateFlags.ContainsKey(request.UserId))
                {
                    s_UpdateFlags[request.UserId] = new UpdateLog
                    {
                        IsUpdated = false,
                        LastClientLogId = s_UpdateFlags[request.UserId].LastClientLogId
                    };
                }
                return result;
            }
            return null;
        }

        public static Log[] UpdateLogData(LogRequest request)
        {
            int prevLastClientLogId = s_UpdateFlags[request.UserId].LastClientLogId;
            if (s_LogData.ContainsKey(request.UserId))
            {
                if (s_UpdateFlags.ContainsKey(request.UserId))
                {
                    s_UpdateFlags[request.UserId] = new UpdateLog
                    {
                        IsUpdated = false,
                        LastClientLogId = MaxLogIdForUser(request.UserId)
                    };
                }
                if (prevLastClientLogId == 0)
                {
                    return s_LogData[request.UserId]
                        .Where(obj => obj.Id >= prevLastClientLogId)
                        .ToArray();
                }
                else
                {
                    return s_LogData[request.UserId]
                       .Where(obj => obj.Id > prevLastClientLogId)
                       .ToArray();
                }
            }
            return null;
        }

        public static void AddLogForUser(int userId, LogType type, string message)
        {
            var log = new Log();
            log.Id = s_NextLogId;
            log.Message = message;
            log.Time = DateTime.Now.ToShortTimeString();
            log.Type = type;

            if (s_LogData.ContainsKey(userId))
            {
                s_LogData[userId].Add(log);
                if (s_NextLogId > 2_000_000_000)
                {
                    s_NextLogId = 0;
                }
                ++s_NextLogId;
            }
            else 
            {
                var list = new List<Log>() { log };
                s_LogData.Add(userId, list);
                if (s_NextLogId > 2_000_000_000)
                {
                    s_NextLogId = 0;
                }
                ++s_NextLogId;
            }

            if (s_UpdateFlags.ContainsKey(userId))
            {
                s_UpdateFlags[userId] = new UpdateLog
                {
                    IsUpdated = true,
                    LastClientLogId = s_UpdateFlags[userId].LastClientLogId
                };
            }
            else
            {
                s_UpdateFlags.Add(userId, new UpdateLog
                {
                    IsUpdated = false,
                    LastClientLogId = 0
                });
            }
        }

    }
}
