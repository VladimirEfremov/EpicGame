using System;
using System.Collections.Generic;
using System.Linq;

namespace EpicGame.Game
{
    public enum EventType
    {
        None, 
        BattleEvent,
        ChatMessageSend
    }

    public class EventStack
    {
        private static Dictionary<int, List<EventType>> s_EventData;

        static EventStack()
        {
            s_EventData = new Dictionary<int, List<EventType>>();
        }

        public static bool IsLogUpdated(int userId)
        {
            if (s_EventData.ContainsKey(userId))
            {
                return s_EventData[userId].Count > 0;
            }
            return false;
        }

        public static EventType[] UpdateEventData(LogRequest request)
        {
            if (s_EventData.ContainsKey(request.UserId))
            {
                var eventArray = s_EventData[request.UserId]?.ToArray();
                s_EventData[request.UserId].Clear();
                return eventArray;
            }
            return null;
        }

        public static void AddEventForUser(int userId, EventType type)
        {
            if (s_EventData.ContainsKey(userId))
            {
                if (!s_EventData[userId].Contains(type))
                {
                    s_EventData[userId].Add(type);
                }
            }
        }

    }
}
