//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Mx.Oxalis.Commons.Interop
//{
//    using System.Data;
//    using System.IO;
//    using System.Linq;
//    using System.Text.RegularExpressions;
//    using System.Xml;

//    using Microsoft.Extensions.Configuration;
//    using Microsoft.Extensions.Configuration.Json;

//    public class Config
//    {
//        private readonly JsonConfigurationProvider provider;

//        private readonly IConfigurationRoot root;

//        private readonly string configFile;

//        private string configContent;

//        private string rxPropPattern = "^defaults\\.({0}[^\\s])";

//        public Config()
//        {
//            this.root = new ConfigurationBuilder().AddJsonFile("oxalis.json").Build();
//            // this.configFile = configFile;
//            this.Parse();
//        }

//        public OxalisConfig Defaults { get; set; }

//        public string GetValue(string path)
//        {
//            var segments = path.Split('.');
//            return this.GetSection(this.root, segments)?.Value;
//        }

//        private void Parse()
//        {
//            this.Defaults = new OxalisConfig();
//            this.Defaults.Transports = new List<TransportConfig>();

//            var transports = this.GetSection(this.root, "defaults", "transport")?.GetChildren();
//            if (transports != null)
//            {
//                foreach (var item in transports)
//                {
//                    var values = item.GetChildren().ToList();
//                    var transportConfig = new TransportConfig();
//                    transportConfig.Profile = this.GetString(values, "profile");
//                    transportConfig.Weight = this.GetInt(values, "weight");
//                    transportConfig.Sender = this.GetString(values, "sender");
//                    this.Defaults.Transports.Add(transportConfig);
//                }
//            }
//        }

//        private IConfigurationSection GetSection(IConfigurationRoot sectionRoot, params string[] path)
//        {
//            var curChildren = sectionRoot.GetChildren().ToList();
//            if (path.Length == 0)
//            {
//                return null;
//            }

//            IConfigurationSection curSection = null;
//            for (int i = 0; i < path.Length; i++)
//            {
//                curSection = curChildren.FirstOrDefault(x => x.Key == path[i]);
//                if (curSection == null)
//                {
//                    return null;
//                }
//                curChildren = curSection.GetChildren().ToList();
//            }

//            return curSection;
//        }

//        private int GetInt(IList<IConfigurationSection> section, string key, int defaultValue = 99999)
//        {
//            var intText = this.GetString(section, key);
//            if (string.IsNullOrWhiteSpace(intText))
//            {
//                return defaultValue;
//            }

//            return Convert.ToInt32(intText);
//        }

//        private string GetString(IList<IConfigurationSection> sections, string key)
//        {
//            return sections.Where(x => x.Key == key).Select(x => x?.Value).FirstOrDefault();
//        }
//    }

//    public class OxalisConfig 
//    {
//        public List<TransportConfig> Transports { get; set; }
//    }

//    public class TransportConfig
//    {
//        public TransportConfig()
//        {
//            // Default behaviour
//            this.Enabled = true;
//        }
//        public string Profile { get; set; }
//        public string Sender { get; set; }
//        public int Weight { get; set; }
//        public bool Enabled { get; set; }
//    }
//}
