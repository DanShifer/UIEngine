using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        private WebResponse HttpWebResponse;
        private RequestHeader RequestHeader;

        private Dictionary<string, string> ParamsValue = new Dictionary<string, string>();
        #endregion

        public Request(string Address, RequestMethod RequestMethod = RequestMethod.POST, RequestHeader RequestHeader = null)
        {
            this.Address = Address;

            this.RequestMethod = RequestMethod;
            this.RequestHeader = RequestHeader;
        }

        public object this[string Param]
        {
            set => ParamsValue.Add(Param, value.ToString());
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

        #region Methods
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        public string GetRespone()
        {
            string ReadResponse = null;

            HttpWebResponse = GetResponse();

            using (StreamReader StreamReader = new StreamReader(HttpWebResponse.GetResponseStream()))
            {
                ReadResponse += StreamReader.ReadToEnd();
            }

            return ReadResponse;
        }

        public async Task<string> GetResponeAsync()
        {
            string ReadResponse = null;

            HttpWebResponse = await GetResponseAsync();

            using (StreamReader StreamReader = new StreamReader(HttpWebResponse.GetResponseStream()))
            {
                ReadResponse += await StreamReader.ReadToEndAsync();
            }

            return ReadResponse;
        }

        public HttpWebResponse GetResponse()
        {
            HttpWebRequest = RequestMethod == RequestMethod.GET ? (HttpWebRequest)WebRequest.Create(Address + Encoding.UTF8.GetString(GetParam())) : (HttpWebRequest)WebRequest.Create(Address);
            HttpWebRequest.Method = RequestMethod == RequestMethod.GET ? "GET" : "POST";

            if (RequestHeader != null)
            {
                foreach (var Headers in RequestHeader?.RequestValue)
                {
                    this.HttpWebRequest.Headers.Add(Headers.Key, Headers.Value);
                }
            }

            HttpWebRequest.ContentType = ContentType ?? "application/x-www-form-urlencoded";
            HttpWebRequest.Accept = Accept ?? "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            HttpWebRequest.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 YaBrowser/19.3.1.887 Yowser/2.5 Safari/537.36";

            if (RequestMethod == RequestMethod.POST)
            {
                this.HttpWebRequest.ContentLength = GetParam().Length;

                using (Stream StreamRequest = this.HttpWebRequest.GetRequestStream())
                {
                    StreamRequest.Write(GetParam(), 0, GetParam().Length);
                }
            }

            ParamsValue.Clear();

            return (HttpWebResponse)HttpWebRequest.GetResponse();
        }

        public async Task<WebResponse> GetResponseAsync()
        {
            HttpWebRequest = RequestMethod == RequestMethod.GET ? (HttpWebRequest)WebRequest.Create(Address + Encoding.UTF8.GetString(GetParam())) : (HttpWebRequest)WebRequest.Create(Address);

            HttpWebRequest.Method = RequestMethod == RequestMethod.GET ? "GET" : "POST";

            if (RequestHeader != null)
            {
                foreach (var Headers in RequestHeader?.RequestValue)
                {
                    this.HttpWebRequest.Headers.Add(Headers.Key, Headers.Value);
                }
            }

            HttpWebRequest.ContentType = ContentType ?? "application/x-www-form-urlencoded";
            HttpWebRequest.Accept = Accept ?? "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            HttpWebRequest.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 YaBrowser/19.3.1.887 Yowser/2.5 Safari/537.36";

            if (RequestMethod == RequestMethod.POST)
            {
                this.HttpWebRequest.ContentLength = GetParam().Length;

                using (Stream StreamRequest = await this.HttpWebRequest.GetRequestStreamAsync())
                {
                    StreamRequest.Write(GetParam(), 0, GetParam().Length);
                }
            }

            return await HttpWebRequest.GetResponseAsync();
        }
        #endregion

        #region Helper Methods
        private byte[] GetParam()
        {
            try
            {
                string Params = null;

                foreach (var Param in ParamsValue)
                {
                    Params += Param.Key + "=" + Param.Value + "&";
                }

                return RequestMethod == RequestMethod.GET ? Encoding.UTF8.GetBytes(Params.Insert(0, "?").TrimEnd('&')) : Encoding.UTF8.GetBytes(Params.TrimEnd('&'));
            }
            catch
            {
                return new byte[1];
            }
        }
        #endregion
    }
}