using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security
{
    using System.Security.Cryptography.X509Certificates;

    using log4net;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;

    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Configuration;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Lang;

    public class ModeDetector
    {

        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(ModeDetector));

        public static Mode detect(X509Certificate certificate) // throws PeppolLoadingException
        {
            return detect(certificate, ConfigFactory.Load());
        }

        public static Mode detect(X509Certificate certificate, IConfigurationRoot config) // throws PeppolLoadingException
        {
            // TODO: change configuration provider!
            //foreach (String token in config.getObject("mode").keySet())
            //{
            //    if (!"default".Equals(token))
            //    {
            //        try
            //        {
            //            Mode mode = Mode.of(config, token);
            //            // TODO: Manage IoC based on Mode
            //            throw new NotImplementedException();
            //            // mode.initiate("security.validator.class", CertificateValidator.class)
            //            //  .validate(Service.ALL, certificate);
            //            // return mode;
            //        }
            //        catch (PeppolSecurityException e)
            //        {
            //            LOGGER.InfoFormat("Detection error ({0}): {1}", token, e.Message);
            //        }
            //    }
            //}

            throw new PeppolLoadingException($"Unable to detect mode for certificate '{certificate.Subject}'.");
        }
    }
}
