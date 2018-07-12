using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Fetcher
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Mx.Peppol.Common.Interop;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;

    public class UrlFetcher : AbstractFetcher
    {

        public UrlFetcher(Mode mode)
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
