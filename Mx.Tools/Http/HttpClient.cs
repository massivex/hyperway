﻿using System;

namespace Mx.Tools.Http
{
    using System.Net;

    using Encoding = System.Text.Encoding;

    public class HttpClient : IDisposable
    {

        public HttpResponse Execute(HttpPost call)
        {

            var request = WebRequest.CreateHttp(call.Url);
            foreach (string header in call.Headers.Keys)
            {
                request.Headers.Add(header, call.Headers[header]);
            }
            request.Method = call.Method.ToString();
            if (call.Entity != null && call.Entity.Length > 0)
            {
                using (var rs = request.GetRequestStream())
                {
                    rs.Write(call.Entity, 0, call.Entity.Length);
                }
            }

            HttpResponse result = new HttpResponse();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                result.StatusCode = response.StatusCode;
                result.Headers = response.Headers;
                string charset = response.CharacterSet;
                if (string.IsNullOrWhiteSpace(charset))
                {
                    charset = "utf-8";
                }
                Encoding encoding = Encoding.GetEncoding(charset);
                using (var stream = response.GetResponseStream())
                {
                    var content = stream.ToBuffer();
                    result.Entity = new HttpEntity(content, encoding); 
                }

            }

            return result;
        }

        public void Dispose()
        {

        }
    }

    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public HttpEntity Entity { get; set; }
    }

    public class HttpEntity
    {
        public HttpEntity(byte[] content, Encoding encoding)
        {
            this.Content = content;
            this.Encoding = encoding;
        }
        public byte[] Content { get; }
        public Encoding Encoding { get; }

        public string GetText()
        {
            return this.Encoding.GetString(this.Content);
        }
    }
}
