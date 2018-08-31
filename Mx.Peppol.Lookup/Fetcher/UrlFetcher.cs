using System;

namespace Mx.Peppol.Lookup.Fetcher
{
    using System.IO;
    using System.Net;

    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;
    using Mx.Tools;

    public class UrlFetcher : AbstractFetcher
    {

        public UrlFetcher(Mode mode)
            : base(mode)
        {

        }


        public override FetcherResponse Fetch(Uri uri)
        {
            try
            {
                HttpWebRequest wr = WebRequest.CreateHttp(uri);
                wr.AllowAutoRedirect = false;
                wr.Timeout = this.Timeout;
                wr.ContinueTimeout = this.Timeout;
                wr.ReadWriteTimeout = this.Timeout;
                wr.Method = "GET";

                byte[] content;
                string header;
                using (var response = (HttpWebResponse)wr.GetResponse())
                {
                    content = response.GetResponseStream().ToBuffer();
                    header = response.GetResponseHeader("X-SMP-Namespace");
                }

                return new FetcherResponse(content.ToStream(), header);
            }
            catch (WebException e)
            {
                throw new LookupException($"Unable to fetch '{uri}' - Received code {e.Status} - {e.Message}", e);
            }
            catch (IOException e)
            {
                throw new LookupException(e.Message, e);
            }
        }
    }
}
