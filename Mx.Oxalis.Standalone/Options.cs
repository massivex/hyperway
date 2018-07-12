using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Standalone
{
    using CommandLine;

    class Options
    {
        [Option('f', Required = true, HelpText = "File(s) to be transmitted")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('d', Required = false, HelpText = "Document type")]
        public string DocumentType { get; set; }

        [Option('p', Required = false, HelpText = "Profile type")]
        public string ProfileType { get; set; }

        [Option('s', Required = true, HelpText = "sender [e.g. 9908:976098897]")]
        public string Sender { get; set; }

        [Option('r', Required = true, HelpText = "recipient [e.g. 9908:976098897]")]
        public string Recipient { get; set; }

        [Option('e', Required = false, HelpText = "Evidence storage dir")]
        public string Evidence { get; set; }

        [Option('x', Required = false, HelpText = "Evidence storage dir", Default = 10)]
        public int ThreadCount { get; set; }

        [Option("factory", Required = false, HelpText = "Use TransmissionRequestFactory (no overrides!)", Default = false)]
        public bool UseRequestFactory { get; set; }

        [Option("repeat", Required = false, HelpText = "Number of repeats to use", Default = 1)]
        public int RepeatCount { get; set; }

        [Option("probe", Required = false, HelpText = "Perform probing of endpoint", Default = false)]
        public bool Probe { get; set; }

        [Option('u', Required = false, HelpText = "Destination URL")]
        public string DestinationUrl { get; set; }

        [Option('c', "cert", Required = false, HelpText = "Destination certificate")]
        public string DestinationCertificate { get; set; }

        [Option('m', Required = false, HelpText = "Max number of transmission", Default = int.MaxValue)]
        public int MaxTransmissions { get; set; }
    }
}
