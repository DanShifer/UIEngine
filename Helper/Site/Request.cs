using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UIEngine.Helper.Enum;

namespace UIEngine.Helper.Site
{
    public class Request
    {
        private readonly string Address = null;
        private readonly RequestMethod RequestMethod;

        private WebRequest WebRequest;
        private WebResponse WebResponse;

        private Dictionary<string, string> KeyValuePairs = new Dictionary<string, string>();

        public Request(string Address, RequestMethod RequestMethod = RequestMethod.POST)
        {
            this.Address = Address;
            this.RequestMethod = RequestMethod;
        }

        public virtual object this[string Param]
        {
            set => KeyValuePairs.Add(Param, value.ToString());
        }

        public virtual string GetRespone()
        {
            string ReadResponse = null;

            this.WebRequest = this.RequestMethod == RequestMethod.GET ? WebRequest.Create(this.Address + Encoding.UTF8.GetString(GetParam())) : WebRequest.Create(this.Address);

            this.WebRequest.Method = this.RequestMethod == RequestMethod.GET ? "GET" : "POST";
            this.WebRequest.ContentType = "application/x-www-form-urlencoded";

            if (this.RequestMethod == RequestMethod.POST)
            {
                this.WebRequest.ContentLength = GetParam().Length;
                using (Stream StreamRequest = this.WebRequest.GetRequestStream())
                {
                    StreamRequest.Write(GetParam(), 0, GetParam().Length);
                }
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

                return this.RequestMethod == RequestMethod.GET ? Encoding.UTF8.GetBytes(Params.Insert(0, "?").TrimEnd('&')) : Encoding.UTF8.GetBytes(Params.TrimEnd('&'));
            }
            catch
            {
                return new byte[1];
            }
        }
    }
}