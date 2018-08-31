using System;
using System.Collections.Generic;

namespace Mx.Peppol.Mode
{
    using System.Linq;

    using Autofac;

    using Microsoft.Extensions.Configuration;

    public class Mode
    {
        private readonly IComponentContext context;

        public const string Production = "PRODUCTION";

        public const string Test = "TEST";

        private readonly IConfigurationRoot config;

        public Mode(IComponentContext context)
        {
            this.context = context;
            this.config = new ConfigurationBuilder().AddJsonFile("hyperway.json").Build();
            this.Parse();
        }


        public HyperwayConfig Defaults { get; set; }

        public T Resolve<T>()
        {
            return this.context.Resolve<T>();
        }

        public string GetValue(string key)
        {
            key = key.Replace(".", ":");
            var value = this.config[key];
            if (value == null)
            {
                value = this.config["defaults:" + key];
            }

            return value;
        }

        private void Parse()
        {
            this.Defaults = new HyperwayConfig();
            this.Defaults.Transports = new List<TransportConfig>();

            var transports = this.GetSection(this.config, "defaults", "transport")?.GetChildren();
            if (transports != null)
            {
                foreach (var item in transports)
                {
                    var values = item.GetChildren().ToList();
                    var transportConfig = new TransportConfig();
                    transportConfig.Profile = this.GetString(values, "profile");
                    transportConfig.Weight = this.GetInt(values, "weight");
                    transportConfig.Sender = this.GetString(values, "sender");
                    this.Defaults.Transports.Add(transportConfig);
                }
            }
        }

        private IConfigurationSection GetSection(IConfigurationRoot sectionRoot, params string[] path)
        {
            var curChildren = sectionRoot.GetChildren().ToList();
            if (path.Length == 0)
            {
                return null;
            }

            IConfigurationSection curSection = null;
            for (int i = 0; i < path.Length; i++)
            {
                curSection = curChildren.FirstOrDefault(x => x.Key == path[i]);
                if (curSection == null)
                {
                    return null;
                }
                curChildren = curSection.GetChildren().ToList();
            }

            return curSection;
        }

        private int GetInt(IList<IConfigurationSection> section, string key, int defaultValue = 99999)
        {
            var intText = this.GetString(section, key);
            if (string.IsNullOrWhiteSpace(intText))
            {
                return defaultValue;
            }

            return Convert.ToInt32(intText);
        }

        private string GetString(IList<IConfigurationSection> sections, string key)
        {
            return sections.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
        }
    }
}