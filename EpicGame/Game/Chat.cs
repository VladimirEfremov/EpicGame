using System;
using System.Linq;
using EpicGameCommon.Models;
using System.Collections.Generic;

namespace EpicGame.Game
{

    public class Dialog
    {
        public List<Message> Data { get; set; }
        public Dialog()
        {
            Data = new List<Message>();
        }
    }

    public class Chat
    {

        //UserId [CompanionId, Dialog]
        public static Dictionary<int, Dictionary<int, Dialog>> s_ConverasetionsList;


        static Chat()
        {
            s_ConverasetionsList = new Dictionary<int, Dictionary<int, Dialog>>();
        }

        //user1: Hi
        //> Chat.AddMessageForUser(user1, user2, new Message(){Content="Hi", DateTime.Now} 
        //user2: hello
        //
        public static void AddMessageForUser(int senderId, int getterId, Message msg)
        {
            if (s_ConverasetionsList.ContainsKey(senderId))
            {
                if (s_ConverasetionsList[senderId].ContainsKey(getterId))
                {
                    s_ConverasetionsList[senderId][getterId].Data.Add(msg);
                }
                else
                {
                    var dialog = new Dialog();
                    dialog.Data.Add(msg);
                    s_ConverasetionsList[senderId].Add(getterId, dialog);
                }
            }
            else
            {
                var dictionary = new Dictionary<int, Dialog>();
                var dialog = new Dialog();
                dialog.Data.Add(msg);
                dictionary.Add(getterId, dialog);
                s_ConverasetionsList.Add(senderId, dictionary);
            }
            EventStack.AddEventForUser(getterId, EventType.ChatMessageSend);
        }

        public static Message[] GetDialogMessages(int userId, int companionId)
        {
            if (s_ConverasetionsList.ContainsKey(userId))
            {
                if (s_ConverasetionsList[userId].ContainsKey(companionId))
                {
                    return s_ConverasetionsList[userId][companionId].Data.ToArray();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static MessageInfo[] GetDialogForUser(DialogId dialogId)
        {
            int index = 0;
            var userNick = src.DBHelper.GameDBHelper.GetUserNickById(dialogId.UserId)?.Trim();
            var companionNick = src.DBHelper.GameDBHelper.GetUserNickById(dialogId.CompanionId)?.Trim();
            
            var thisIdMessages = GetDialogMessages(dialogId.UserId, dialogId.CompanionId);
            var companionIdMessages = GetDialogMessages(dialogId.CompanionId, dialogId.UserId);
            
            if (thisIdMessages != null && companionIdMessages != null)
            {
                int resultLength = thisIdMessages.Length + companionIdMessages.Length;
                var result = new MessageInfo[resultLength];
                var temp = new List<(string Nick, string Content, DateTime Time)>(resultLength);

                foreach (var message in thisIdMessages)
                {
                    temp.Add((Nick: userNick, Content: message.Content, Time: message.Time));
                }

                foreach (var message in companionIdMessages)
                {
                    temp.Add((Nick: companionNick, Content: message.Content, Time: message.Time));
                }

                var sortedTemp = temp.OrderBy(obj => obj.Time.Ticks);

                foreach (var element in sortedTemp)
                {
                    if (index < resultLength)
                    {
                        result[index++] = new MessageInfo()
                        {
                            Nickname = element.Nick,
                            Content = element.Content,
                            Time = element.Time.ToShortTimeString()
                        };
                    }
                }

                return result;
            }
            else if (thisIdMessages != null)
            {
                var result = new MessageInfo[thisIdMessages.Length];
                for (int i = 0; i < thisIdMessages.Length; i++)
                {
                    if (i < thisIdMessages.Length)
                    {
                        result[i] = new MessageInfo()
                        {
                            Nickname = src.DBHelper.GameDBHelper.GetUserNickById(dialogId.UserId),
                            Content = thisIdMessages[i].Content,
                            Time = thisIdMessages[i].Time.ToShortTimeString()
                        };
                    }
                }
                return result;
            }
            else if (companionIdMessages != null)
            {
                var result = new MessageInfo[companionIdMessages.Length];
                for (int i = 0; i < companionIdMessages.Length; i++)
                {
                    if (i < companionIdMessages.Length)
                    {
                        result[i] = new MessageInfo()
                        {
                            Nickname = src.DBHelper.GameDBHelper.GetUserNickById(dialogId.CompanionId),
                            Content = companionIdMessages[i].Content,
                            Time = companionIdMessages[i].Time.ToShortTimeString()
                        };
                    }
                }
                return result;
            }
            return new MessageInfo[0];
        }

        public int[] GetDialogsId(int userId)
        {
            if (s_ConverasetionsList.ContainsKey(userId))
            {
                //чат который мы начали
                var resultList = s_ConverasetionsList[userId].Keys.ToList();
                //чат, который начали с нами
                var allKeys = s_ConverasetionsList.Keys.ToArray();
                for (int i = 0; i < allKeys.Length; i++)
                {
                    if (allKeys[i] != userId)
                    {
                        if (s_ConverasetionsList[allKeys[i]].ContainsKey(userId))
                        {
                            resultList.Add(allKeys[i]);
                        }
                    }
                }
                return resultList.ToArray();
            }
            return new int[0];
        }


    }
}
