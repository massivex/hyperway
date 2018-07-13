using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Interop
{
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;

    public class Config
    {
        private readonly JsonConfigurationProvider provider;

        private readonly IConfigurationRoot root;

        private readonly string configFile;

        private string configContent;

        private string rxPropPattern = "^defaults\\.({0}[^\\s])";

        public Config()
        {
            this.root = new ConfigurationBuilder().AddJsonFile("oxalis.json").Build();
            // this.configFile = configFile;
            this.Parse();
        }

        public OxalisConfig Defaults { get; set; }

        private void Parse()
        {
            this.Defaults = new OxalisConfig();
            this.Defaults.Transports = new List<TransportConfig>();

            var defaults = this.root.GetChildren().FirstOrDefault(x => x.Key == "defaults");
            if (defaults != null)
            {
                var transport = defaults.GetChildren().FirstOrDefault(x => x.Key == "transport");
                if (transport != null)
                {
                    var transports = transport.GetChildren();
                    foreach (var item in transports)
                    {
                        var values = item.GetChildren().ToList();
                        var transportConfig = new TransportConfig();
                        transportConfig.Profile = values.Where(x => x.Key == "profile").Select(x => x?.Value).FirstOrDefault();
                        transportConfig.Weight = Convert.ToInt32(
                            values.Where(x => x.Key == "weight").Select(x => x?.Value).FirstOrDefault());
                        transportConfig.Sender = values.Where(x => x.Key == "sender").Select(x => x?.Value).FirstOrDefault();
                        this.Defaults.Transports.Add(transportConfig);            
                    }
                }
            }
        }
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
