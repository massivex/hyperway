using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Locator
{
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using ARSoft.Tools.Net;
    using ARSoft.Tools.Net.Dns;

    using Microsoft.Extensions.FileSystemGlobbing;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Util;
    using Mx.Peppol.Mode;
    using Mx.Tools;
    using Mx.Tools.Encoding;

    /**
     * Implementation of Business Document Metadata Service Location Version 1.0.
     *
     * @see <a href="http://docs.oasis-open.org/bdxr/BDX-Location/v1.0/BDX-Location-v1.0.html">Specification</a>
     */
    public class BdxlLocator : AbstractLocator
    {
        private readonly string sml;

        private DynamicHostnameGenerator hostnameGenerator;

        private DnsClient dnsClient;

        public BdxlLocator(Mode mode)
        : this(
                mode.GetValue("lookup.locator.bdxl.prefix"),
                mode.GetValue("lookup.locator.hostname"),
                mode.GetValue("lookup.locator.bdxl.algorithm"),
                EncodingUtils.get(mode.GetValue("lookup.locator.bdxl.encoding")),
                mode.GetValue("lookup.locator.sml"))
        {
           
        }

        /**
         * Initiate a new instance of BDXL lookup functionality using SHA-224 for hashing.
         *
         * @param hostname Hostname used as base for lookup.
         */
        //public BdxlLocator(string hostname)
        //    : this(hostname, "SHA-256")
        //{

        //}

        /**
         * Initiate a new instance of BDXL lookup functionality.
         *
         * @param hostname        Hostname used as base for lookup.
         * @param digestAlgorithm Algorithm used for generation of hostname.
         */
        //public BdxlLocator(string hostname, string digestAlgorithm)
        //    : this("", hostname, digestAlgorithm)
        //{

        //}

        ///**
        // * Initiate a new instance of BDXL lookup functionality.
        // *
        // * @param prefix          Value attached in front of calculated hash.
        // * @param hostname        Hostname used as base for lookup.
        // * @param digestAlgorithm Algorithm used for generation of hostname.
        // */
        //public BdxlLocator(string prefix, string hostname, string digestAlgorithm)
        //    : this(prefix, hostname, digestAlgorithm, EncodingUtils.get(BaseEncodingType.Base32))
        //{

        //}

        /**
         * Initiate a new instance of BDXL lookup functionality.
         *
         * @param prefix          Value attached in front of calculated hash.
         * @param hostname        Hostname used as base for lookup.
         * @param digestAlgorithm Algorithm used for generation of hostname.
         * @param encoding        Encoding of hash for hostname.
         */
        private BdxlLocator(string prefix, string hostname, string digestAlgorithm, IBaseEncoding encoding, string sml)
        {
            this.sml = sml;
            this.hostnameGenerator = new DynamicHostnameGenerator(prefix, hostname, digestAlgorithm, encoding);
        }

        public override Uri lookup(ParticipantIdentifier participantIdentifier)
        {
            // Create hostname for participant identifier.
            string hostname = this.hostnameGenerator.Generate(participantIdentifier).ReplaceAll("=*", "");

            try
            {
                // var records = System.Net.Dns.GetHostAddresses(hostname);
                // Fetch all records of type NAPTR registered on hostname.
                // Record[] records = new Lookup<,>(hostname, Type.NAPTR).run();
                var dn = DomainName.Parse(hostname);
                var client = this.GetDnsClient();
                var records = client.Resolve(dn, RecordType.Naptr, RecordClass.Any).AnswerRecords;
                if (records == null || records.Count == 0)
                {
                    throw new LookupException($"Identifier '{participantIdentifier}' not registered in SML.");
                }

                // Loop records found.
                foreach (DnsRecordBase record in records)
                {
                    // Simple cast.
                    NaptrRecord naptrRecord = (NaptrRecord)record;

                    // Handle only those having "Meta:SMP" as service.
                    if ("Meta:SMP".Equals(naptrRecord.Services) && "U".EqualsIgnoreCase(naptrRecord.Flags))
                    {
                        // Create URI and return.
                        string result = HandleRegex(naptrRecord.RegExp, hostname);
                        if (result != null)
                        {
                            return new Uri(result);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new LookupException("Error when handling DNS lookup for BDXL.", e);
            }

            throw new LookupException("Record for SMP not found in SML.");
        }

        private DnsClient GetDnsClient()
        {
            if (this.dnsClient == null)
            {
                var ipEntry = System.Net.Dns.GetHostEntry(this.sml);
                this.dnsClient = new DnsClient(ipEntry.AddressList.First(), 5000);
            }

            return this.dnsClient;
        }

        private string HandleRegex(string naptrRegex, string hostname)
        {
            string[] regexp = naptrRegex.Split(new [] { "!" }, StringSplitOptions.None);

            // Simple stupid
            if ("^.*$".Equals(regexp[1]))
            {
                return regexp[2];
            }

            // Using regex
            // Pattern pattern = Pattern.compile(regexp[1]);
            Match matcher = Regex.Match(hostname, regexp[1]);
            if (matcher.Success)
            {
                var replacement = regexp[2].ReplaceAll("\\\\{2}", "\\$");
                return Regex.Replace(hostname, regexp[1], replacement);
            }

            // No match
            return null;
        }
    }

}
