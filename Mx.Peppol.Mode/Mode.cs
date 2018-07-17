using System;
using System.Collections.Generic;

namespace Mx.Peppol.Mode
{
    using System.Linq;

    using Autofac;

    using Microsoft.Extensions.Configuration;

    public class Mode
    {

        public const string PRODUCTION = "PRODUCTION";

        public const string TEST = "TEST";

        private IConfigurationRoot config;

        private String identifier;

        private IContainer container;

        public Mode()
        {
            this.config = new ConfigurationBuilder().AddJsonFile("oxalis.json").Build();
            this.Parse();
        }


        public OxalisConfig Defaults { get; set; }

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
            this.Defaults = new OxalisConfig();
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
            return sections.Where(x => x.Key == key).Select(x => x?.Value).FirstOrDefault();
        }

        //public static Mode of(String identifier, IContainer container)
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json");
        //    var config = builder.Build();
        //    return of(config, identifier, container);
        //}

        //public static Mode of(IConfigurationRoot config, String identifier, IContainer container)
        //{
        //    // Config referenceConfig = ConfigFactory.defaultReference();

        //    // Config result = ConfigFactory.systemProperties().withFallback(config).withFallback(referenceConfig);

        //    // Loading configuration based on identifier.
        //    //if (identifier != null)
        //    //{
        //    //    if (referenceConfig.hasPath(String.format("mode.%s", identifier)))
        //    //    {
        //    //        result = result.withFallback(referenceConfig.getConfig(String.Format("mode.{0}", identifier)));
        //    //    }
        //    //}

        //    //// Load inherited configuration.
        //    //if (result.hasPath("inherit"))
        //    //{
        //    //    result = result.withFallback(
        //    //        referenceConfig.getConfig(String.Format("mode.{0}", result.getString("inherit"))));
        //    //}

        //    //// Load default configuration.
        //    //if (referenceConfig.hasPath("mode.default"))
        //    //{
        //    //    result = result.withFallback(referenceConfig.getConfig("mode.default"));
        //    //}

        //    return new Mode(config, identifier, container);
        //}

        //private Mode(IConfigurationRoot config, String identifier, IContainer container)
        //{
        //    this.config = config;
        //    this.identifier = identifier;
        //    this.container = container;
        //}

        public String getIdentifier()
        {
            return this.identifier;
        }

        //public String getString(String key)
        //{

        //}

        //public Config getConfig()
        //{
        //    return config;
        //}

        //@SuppressWarnings({ "unchecked", "unused"})
        public T initiate<T>()
        {
            return this.container.Resolve<T>();
        }
    }
    //        public T initiate<T>(String key) where T : class // throws PeppolLoadingException
    //        {
    //            try
    //            {
    //                var typeName = getString(
    //                return this.container.Resolve<>())
    //                var typeName = getString(key);
    //                var targetType = Type.GetType(typeName);
    //                return (T) Activator.CreateInstance(targetType ?? throw new InvalidOperationException());
    ////                return (T)initiate(Class.forName(getString(key)));
    //            } catch (Exception e) {
    //                throw new PeppolLoadingException(String.Format("Unable to initiate '{0}'", this.getString(key)), e);
    //            }
    //        }

    //public <T> T initiate(Class<T> cls) throws PeppolLoadingException
    //{
    //        try {
    //        try
    //        {
    //            return cls.getConstructor(Mode.class).newInstance(this);
    //            } catch (NoSuchMethodException e) {
    //                return cls.newInstance();
    //            }
    //        } catch (InstantiationException | IllegalAccessException | InvocationTargetException e) {
    //            throw new PeppolLoadingException(String.format("Unable to initiate '%s'", cls), e);
    //        }
    //    }
    //}

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