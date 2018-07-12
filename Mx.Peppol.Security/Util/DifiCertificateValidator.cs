using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Util
{
    using System.Security.Cryptography.X509Certificates;

    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Lang;

    public class DifiCertificateValidator : CertificateValidator
    {

    private ValidatorGroup validator;

    private Mode mode;

    public DifiCertificateValidator(Mode mode) // throws PeppolLoadingException
    {
        this.mode = mode;

    try {
        validator = ValidatorLoader.newInstance().build(
            getClass().getResourceAsStream(mode.getString("security.pki")));
    } catch (ValidatorParsingException e) {
    throw new PeppolLoadingException("Unable to initiate PKI.", e);
}
}

@Override
public void validate(Service service, X509Certificate certificate) // throws PeppolSecurityException
{
try {
validator.validate(mode.getString(String.format("security.validator.%s", service.toString())), certificate);
} catch (CertificateValidationException e) {
throw new PeppolSecurityException(e.getMessage(), e);
}
}
}

}
