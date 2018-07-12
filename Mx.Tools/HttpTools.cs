namespace Mx.Tools
{
    using System.IO;
    using System.Net;

    public class HttpTools
    {
        public Stream GetHttpStream(string url)
        {
            HttpWebRequest req = WebRequest.CreateHttp(url);
            byte[] data;
            using (var response = req.GetResponse())
            {
                data = response.GetResponseStream().ToBuffer();
            }

            return data.ToStream();
        }
    }
}
