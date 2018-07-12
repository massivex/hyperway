using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Interop
{
    using System.Data;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Config
    {
        private readonly string configFile;

        private string configContent;

        private string rxPropPattern = "^defaults\\.({0}[^\\s])";

        public Config(string configFile)
        {
            this.configFile = configFile;
        }

        public OxalisConfig Defaults { get; set; }
    }

    public class OxalisConfig 
    {
        public List<TransportConfig> Transports { get; set; }
    }

    public class TransportConfig
    {
        public TransportConfig()
        {
            // Default behaviour
            this.Enabled = true;
        }
        public string Profile { get; set; }
        public string Sender { get; set; }
        public int Weight { get; set; }
        public bool Enabled { get; set; }
    }
}
