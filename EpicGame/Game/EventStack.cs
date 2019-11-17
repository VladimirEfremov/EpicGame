using EpicGameCommon.Models;
using System.Collections.Generic;

namespace EpicGame.Game
{
    public class EventStack
    {
        private static Dictionary<int, List<EventType>> s_EventData;

        static EventStack()
        {
            s_EventData = new Dictionary<int, List<EventType>>();
        }

        public static int IsEventStackUpdated(int userId)
        {
            if (s_EventData.ContainsKey(userId))
            {
                return s_EventData[userId].Count > 0 ? 1 : 0;
            }
            return -1;
        }

        public static EventType[] UpdateEventData(int userId)
        {
            if (s_EventData.ContainsKey(userId))
            {
                var eventArray = s_EventData[userId]?.ToArray();
                s_EventData[userId].Clear();
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
            else
            {
                var list = new List<EventType>(1) { type };
                s_EventData.Add(userId, list);
            }
        }

    }
}