//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Mx.Peppol.Lookup.Fetcher
//{
//    using Mx.Peppol.Mode;

//    public class ApacheFetcher : BasicApacheFetcher
//    {

//    private PoolingHttpClientConnectionManager httpClientConnectionManager;

//    public ApacheFetcher(Mode mode)
//    {
//        super(mode);

//        this.httpClientConnectionManager = new PoolingHttpClientConnectionManager();
//    }

//    @Override
//    protected CloseableHttpClient createClient()
//    {
//        return HttpClients.custom()
//            .setDefaultRequestConfig(requestConfig)
//            .setConnectionManager(httpClientConnectionManager)
//            .setConnectionManagerShared(true)
//            .build();
//    }
//    }
//}
