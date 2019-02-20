using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UIEngine.Helper.Enum;

namespace UIEngine.Helper.Site
{
    class Request
    {
        private string Address = null;
        private RequestMethod RequestMethod;

        private WebRequest WebRequest;
        private WebResponse WebResponse;

        private Dictionary<string, string> KeyValuePairs = new Dictionary<string, string>();

        public Request(string Address,RequestMethod RequestMethod)
        {
            this.Address = Address;
            this.RequestMethod = RequestMethod;

            this.WebRequest = WebRequest.Create(this.Address);

            switch (RequestMethod)
            {
                case RequestMethod.GET:
                    this.WebRequest.Method = "GET";
                    break;
                case RequestMethod.POST:
                    this.WebRequest.Method = "POST";
                    break;
            }

            this.WebRequest.ContentType = "application/x-www-form-urlencoded";
        }

        public object this[string Param]
        {
            set => KeyValuePairs.Add(Param, value.ToString());
        }

        public string GetRespone()
        {
            string ReadResponse = null;
            
            this.WebRequest.ContentLength = GetParam().Length;

            using (Stream StreamRequest = this.WebRequest.GetRequestStream())
            {
                StreamRequest.Write(GetParam(), 0, GetParam().Length);
            }

            this.WebResponse = this.WebRequest.GetResponse();

            using (Stream StreamResponse = WebResponse.GetResponseStream())
            {
                using (StreamReader StreamReader = new StreamReader(StreamResponse))
                {
                    ReadResponse += StreamReader.ReadToEnd();
                }
            }

            return ReadResponse;
        }

        private byte[] GetParam()
        {
            try
            {
                string Params = null;

                foreach (var Param in KeyValuePairs)
                {
                    Params += Param.Key + "=" + Param.Value + "&";
                }

                return Encoding.UTF8.GetBytes(Params.TrimEnd('&'));
            }
            catch
            {
               return new byte[1];
            }
        }
    }
}