namespace EpicGame
{
    public static class JsonHelper
    {
        private static readonly Newtonsoft.Json.JsonSerializerSettings Settings = 
            new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
            };

        public static string ToJson(this object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(
                    obj,
                    Newtonsoft.Json.Formatting.Indented, 
                    Settings);
            }
            catch
            {
                return null;
            }
        }

        public static T FromJson<T>(this string jsonString)
        {
            try
            {
                return Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<T>(jsonString, Settings);
            }
            catch
            {
                return default(T);
            }
        }
    }
}