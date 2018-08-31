using System;

namespace Mx.Peppol.Security
{
    using System.Security.Cryptography.X509Certificates;

    using Microsoft.Extensions.Configuration;

    using Mx.Peppol.Common.Configuration;
    using Mx.Peppol.Mode;

    public class ModeDetector
    {
        public static Mode Detect(X509Certificate certificate) // throws PeppolLoadingException
        {
            return Detect(certificate, ConfigFactory.Load());
        }

        public static Mode Detect(X509Certificate certificate, IConfigurationRoot config) // throws PeppolLoadingException
        {
            throw new NotImplementedException();
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
            // throw new PeppolLoadingException($"Unable to detect mode for certificate '{certificate.Subject}'.");
        }
    }
}
