using System;

namespace Mx.Hyperway.Sml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using ARSoft.Tools.Net;
    using ARSoft.Tools.Net.Dns;

    public class SmlServer
    {
        private readonly SmlServerOptions options;

        private readonly DnsServer server;

        private readonly IPAddress serverIP;

        public SmlServer(SmlServerOptions options)
        {
            this.options = options;

            this.serverIP = IPAddress.Parse(this.options.IPAddress);
            this.server = new DnsServer(this.serverIP, 5, 5);
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
                Console.WriteLine($"Request {question.RecordType} for {question.Name}");
                if (question.RecordType != RecordType.Naptr
                        && question.RecordType != RecordType.A
                        && question.RecordType != RecordType.Txt
                        && question.RecordType != RecordType.Ptr
                        && question.RecordType != RecordType.Aaaa)
                {
                    unknowQuestion = true;
                    break;
                }

                var query = question.Name.ToString();
                var smlRequest = SmlRequest.Parse(query);
                var smpRecord = this.Find(smlRequest);
                var response = new DnsMessage();
                if (smpRecord != null)
                {
                    List<DnsRecordBase> answers = new List<DnsRecordBase>();
                    if (question.RecordType == RecordType.A
                        || question.RecordType == RecordType.Ptr
                        || question.RecordType == RecordType.Aaaa
                        || question.RecordType == RecordType.Txt)
                    {
                        response.IsEDnsEnabled = true;
                        response.IsRecursionDesired = true;
                        response.IsQuery = true;
                        response.ReturnCode = ReturnCode.NotImplemented;
                    }
                    else if (question.RecordType == RecordType.Naptr)
                    {
                        string services = "Meta:SMP";
                        string flags = "U";
                        string regExp = smpRecord.RegExp;

                        NaptrRecord rec = new NaptrRecord(question.Name, 1000, 0, 0, flags, services, regExp, null);
                        answers.Add(rec);
                    }
                    else
                    {
                        throw new NotSupportedException($"RecordType not suppoerted '{question.RecordType}'");
                    }


                    response.AnswerRecords.AddRange(answers);

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
