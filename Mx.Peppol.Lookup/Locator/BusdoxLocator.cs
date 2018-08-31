using System;

namespace Mx.Peppol.Lookup.Locator
{
    using System.Linq;
    using System.Net;

    using ARSoft.Tools.Net;
    using ARSoft.Tools.Net.Dns;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Util;
    using Mx.Peppol.Mode;

    public class BusdoxLocator : AbstractLocator
    {

        private readonly DynamicHostnameGenerator hostnameGenerator;

        private DnsClient dnsClient;

        private readonly string sml;

        public BusdoxLocator(Mode mode) : this(
            mode.GetValue("lookup.locator.busdox.prefix"),
            mode.GetValue("lookup.locator.hostname"),
            mode.GetValue("lookup.locator.busdox.algorithm"),
            mode.GetValue("lookup.locator.sml"))
        {
        }

        public BusdoxLocator(string prefix, string hostname, string algorithm, string sml)
        {
            this.hostnameGenerator = new DynamicHostnameGenerator(prefix, hostname, algorithm);
            this.sml = sml;
        }

        public override Uri Lookup(ParticipantIdentifier participantIdentifier)
        {
            // Create hostname for participant identifier.
            string hostname = this.hostnameGenerator.Generate(participantIdentifier);

            try
            {
                var client = this.GetDnsClient();
                var message = client.Resolve(DomainName.Parse(hostname));
                if (message.ReturnCode != ReturnCode.NoError)
                {
                    throw new LookupException(
                        string.Format("Identifier '{0}' not registered in SML.", participantIdentifier.Identifier));
                }
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }

            return new Uri(string.Format("http://{0}", hostname));
        }


        private DnsClient GetDnsClient()
        {
            if (this.dnsClient == null)
            {
                if (!string.IsNullOrWhiteSpace(this.sml))
                {
                    var ipEntry = Dns.GetHostEntry(this.sml);
                    this.dnsClient = new DnsClient(ipEntry.AddressList.First(), 5000);

                }
                else
                {
                    this.dnsClient = DnsClient.Default;
                }
            }

            return this.dnsClient;
        }
    }

}
