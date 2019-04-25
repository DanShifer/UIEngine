using System.Collections.Generic;

namespace UIEngine.Helper.Site.Helper
{
    public class RequestHeader
    {
        public readonly Dictionary<string, string> RequestValue = new Dictionary<string, string>();

        public string this[string Key]
        {
            get => RequestValue[Key];
            set => RequestValue.Add(Key, value);
        }
    }
}