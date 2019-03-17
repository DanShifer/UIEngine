using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UIEngine.Helper.Enum;
using UIEngine.Helper.Site.Helper;

namespace UIEngine.Helper.Site
{
    public class Request
    {
        #region Params
        private readonly string Address = null;
        private readonly RequestMethod RequestMethod;

        private HttpWebRequest HttpWebRequest;
        private HttpWebResponse HttpWebResponse;
        private RequestHeader RequestHeader;

        private Dictionary<string, string> KeyValuePairs = new Dictionary<string, string>();
        #endregion

        public Request(string Address, RequestMethod RequestMethod = RequestMethod.POST, RequestHeader RequestHeader = null)
        {
            this.Address = Address;
            this.RequestMethod = RequestMethod;
            this.RequestHeader = RequestHeader;
        }

        public object this[string Param]
        {
            set => KeyValuePairs.Add(Param, value.ToString());
        }

        #region Params
        public string ContentType
        {
            private get;
            set;
        }

        public string Accept
        {
            private get;
            set;
        }

        public string UserAgent
        {
            private get;
            set;
        }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        public string GetRespone()
        {
            try
            {
                string ReadResponse = null;

                this.HttpWebRequest = this.RequestMethod == RequestMethod.GET ? (HttpWebRequest)WebRequest.Create(this.Address + Encoding.UTF8.GetString(GetParam())) : (HttpWebRequest)WebRequest.Create(this.Address);

                this.HttpWebRequest.Method = this.RequestMethod == RequestMethod.GET ? "GET" : "POST";

                if (RequestHeader != null)
                {
                    foreach (var Headers in RequestHeader?.KeyValuePairs)
                    {
                        this.HttpWebRequest.Headers.Add(Headers.Key, Headers.Value);
                    }
                }

                this.HttpWebRequest.ContentType = ContentType;
                this.HttpWebRequest.Accept = Accept;
                this.HttpWebRequest.UserAgent = UserAgent;

                if (this.RequestMethod == RequestMethod.POST)
                {
                    this.HttpWebRequest.ContentLength = GetParam().Length;
                    using (Stream StreamRequest = this.HttpWebRequest.GetRequestStream())
                    {
                        StreamRequest.Write(GetParam(), 0, GetParam().Length);
                    }
                }

                this.HttpWebResponse = (HttpWebResponse)this.HttpWebRequest.GetResponse();
                using (Stream StreamResponse = HttpWebResponse.GetResponseStream())
                {
                    using (StreamReader StreamReader = new StreamReader(StreamResponse))
                    {
                        ReadResponse += StreamReader.ReadToEnd();
                    }
                }

                return ReadResponse;
            }
            catch (Exception EX)
            {
                return EX.Message;
            }
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