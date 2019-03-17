using System.Collections.Generic;

namespace UIEngine.Helper.Site.Helper
{
    public class RequestHeader
    {
        public readonly Dictionary<string, string> KeyValuePairs = new Dictionary<string, string>();

        public string this[string Key]
        {
            get => KeyValuePairs[Key];
            set => KeyValuePairs.Add(Key, value);
        }

        public Dictionary<string,string> this[string Key,string Value]
        {
            get => KeyValuePairs;
            set => KeyValuePairs.Add(Key, Value);
        }       
    }
}