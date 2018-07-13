using System;

namespace Mx.Peppol.Lookup.Fetcher
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;
    using Mx.Tools;

    public class BasicApacheFetcher : AbstractFetcher
    {
        public BasicApacheFetcher(Mode mode)
            : base(mode)
        {

        }


        public override FetcherResponse fetch(Uri uri) // throws LookupException, FileNotFoundException
        {
            try
            {
                HttpWebRequest wr = WebRequest.CreateHttp(uri);
                wr.AllowAutoRedirect = false;
                wr.Timeout = this.timeout;
                wr.ContinueTimeout = this.timeout;
                wr.ReadWriteTimeout = this.timeout;
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
            catch (Exception ex) when (ex is LookupException || ex is FileNotFoundException e)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }


        }
    }
}
