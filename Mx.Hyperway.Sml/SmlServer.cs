using System;

namespace Mx.Hyperway.Sml
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using ARSoft.Tools.Net.Dns;

    public class SmlServer
    {
        private readonly SmlServerOptions options;

        private readonly DnsServer server;

        public SmlServer(SmlServerOptions options)
        {
            this.options = options;

            var ip = IPAddress.Parse(this.options.IPAddress);
            this.server = new DnsServer(ip, 5, 5);
            this.server.QueryReceived += this.SOnQueryReceived;
        }

        public void Start()
        {
            this.server.Start();
        }

        public void Stop()
        {
            this.server.Stop();
        }

        private SmpRecord Find(SmlRequest request)
        {
            return this.options.Directory.SmpRecords
                .FirstOrDefault(x => x.Hash == request.Hash && x.Scheme == request.Scheme);
        }

        private Task SOnQueryReceived(object sender, QueryReceivedEventArgs eventargs)
        {
            var dnsMessage = eventargs.Query as DnsMessage;
            if (dnsMessage == null)
            {
                return Task.CompletedTask;
            }

            var unknowQuestion = false;
            foreach (var question in dnsMessage.Questions)
            {
                if (question.RecordType != RecordType.Naptr)
                {
                    unknowQuestion = true;
                    break;
                }

                var query = question.Name.ToString();
                var smlRequest = SmlRequest.Parse(query);
                var smpRecord = this.Find(smlRequest);
                if (smpRecord != null)
                {
                    string services = "Meta:SMP";
                    string flags = "U";
                    string regExp = smpRecord.RegExp;

                    NaptrRecord rec = new NaptrRecord(question.Name, 1000, 0, 0, flags, services, regExp, null);

                    var response = new DnsMessage();
                    response.AnswerRecords.Add(rec);
                    eventargs.Response = response;
                }
            }

            if (unknowQuestion)
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
