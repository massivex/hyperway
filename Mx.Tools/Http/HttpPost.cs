using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Tools.Http
{
    using System.Net;
    using System.Net.Http;

    public class HttpPost
    {
        public HttpPost(Uri uri)
        {
            this.Url = uri;
            this.Method = HttpMethod.Post;
            this.Headers = new WebHeaderCollection();
        }
        public void AddHeader(string name, string value)
        {
            this.Headers.Add(name, value);
        }

        public HttpMethod Method { get; }
        public Uri Url { get;  }
        public WebHeaderCollection Headers { get; }
        public byte[] Entity { get; set; }

        public string Host { get; set; }
    }
}
