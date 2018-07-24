namespace Mx.Hyperway.Inbound.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    public class HomeController
    {
        // GET api/values
        [HttpGet]
        public ContentResult Get()
        {
            var result = new ContentResult();
            result.ContentType = "text/html;charset=UTF-8";
            result.Content = "<!DOCTYPE html>\n" +
            "<html>\n" +
                "    <head>\n" +
                "        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\n" +
                "        <title>HYPERWAY PEPPOL server</title>\n" +
                "    </head>\n" +
                "    <body>\n" +
                "        <h1>Welcome to the HYPERWAY PEPPOL server</h1>\n" +
                "        <p>The protocols for this Access Point are :</p>\n" +
                "        <ul>\n" +
                "            <li>The AS2 endpoint can be found <a href=\"as2\">here</a>.</li>\n" +
                "        </ul>\n" +
                "        <p>Some status information can be found at <a href=\"status\">status</a>.</p>\n" +
                "    </body>\n" +
                "</html>\n";
            return result;
        }
    }
}
