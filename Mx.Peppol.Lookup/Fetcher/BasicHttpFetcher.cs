using System;

namespace Mx.Peppol.Lookup.Fetcher
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;
    using Mx.Tools;

    public class BasicHttpFetcher : AbstractFetcher
    {
        public BasicHttpFetcher(Mode mode)
            : base(mode)
        {

        }


        public override FetcherResponse Fetch(Uri uri)
        {
            try
            {
                HttpWebRequest wr = WebRequest.CreateHttp(uri.AbsoluteUri);
                wr.AllowAutoRedirect = false;
                wr.Timeout = this.Timeout;
                wr.ContinueTimeout = this.Timeout;
                wr.ReadWriteTimeout = this.Timeout;
                wr.Method = "GET";
                using (var response = (HttpWebResponse)wr.GetResponse())
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK: // 200
                            var content = response.GetResponseStream().ToBuffer();
                            var header = response.GetResponseHeader("X-SMP-Namespace");
                            return new FetcherResponse(content.ToStream(), header);

                        case HttpStatusCode.NotFound: // 404
                            throw new FileNotFoundException(uri.ToString());

                        default:
                            throw new LookupException($"Received code {response.StatusCode} for lookup. URI: {uri}");
                    }

                }
            }
            catch (Exception ex) when (ex is SocketException || ex is WebException)
            {
                throw new LookupException($"Unable to fetch '{uri}'", ex);
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }


        }
    }
}
