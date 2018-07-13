using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Mode
{
    using System.IO;
    using System.Runtime.CompilerServices;

    using Autofac;
    using Autofac.Core;

    using Microsoft.Extensions.Configuration;

    using Mx.Peppol.Common.Lang;

    public class Mode
    {

        public const string PRODUCTION = "PRODUCTION";

        public const string TEST = "TEST";

        private IConfigurationRoot config;

        private String identifier;

        private IContainer container;

        public static Mode of(String identifier, IContainer container)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return of(config, identifier, container);
        }

        public static Mode of(IConfigurationRoot config, String identifier, IContainer container)
        {
            // Config referenceConfig = ConfigFactory.defaultReference();

            // Config result = ConfigFactory.systemProperties().withFallback(config).withFallback(referenceConfig);

            // Loading configuration based on identifier.
            //if (identifier != null)
            //{
            //    if (referenceConfig.hasPath(String.format("mode.%s", identifier)))
            //    {
            //        result = result.withFallback(referenceConfig.getConfig(String.Format("mode.{0}", identifier)));
            //    }
            //}

            //// Load inherited configuration.
            //if (result.hasPath("inherit"))
            //{
            //    result = result.withFallback(
            //        referenceConfig.getConfig(String.Format("mode.{0}", result.getString("inherit"))));
            //}

            //// Load default configuration.
            //if (referenceConfig.hasPath("mode.default"))
            //{
            //    result = result.withFallback(referenceConfig.getConfig("mode.default"));
            //}

            return new Mode(config, identifier, container);
        }

        private Mode(IConfigurationRoot config, String identifier, IContainer container)
        {
            this.config = config;
            this.identifier = identifier;
            this.container = container;
        }

        public String getIdentifier()
        {
            return this.identifier;
        }

        public String getString(String key)
        {
            return this.config[key];
        }

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

}