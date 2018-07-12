using System;
using System.Collections.Generic;

namespace Mx.Certificates.Validator
{
    using System.IO;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public class ValidatorGroup : Validator
    {

    private Dictionary<String, ValidatorRule> rulesMap;

    private String name;

    private String version;

    public ValidatorGroup(Dictionary<String, ValidatorRule> rulesMap) : base(null)
    {
        this.rulesMap = rulesMap;
    }

    public ValidatorGroup(Dictionary<String, ValidatorRule> rulesMap, String name, String version): this(rulesMap)
    {
        this.name = name;
        this.version = version;
    }

    public String getName()
    {
        return name;
    }

    public String getVersion()
    {
        return version;
    }

    public void validate(X509Certificate certificate) // throws CertificateValidationException
    {
        validate("default", certificate);
    }

    public void validate(String name, X509Certificate certificate) // throws CertificateValidationException
    {
        if (!this.rulesMap.ContainsKey(name))
        {
            throw new CertificateValidationException($"Unknown validator '{name}'.");
        }

        this.rulesMap[name].validate(certificate);
    }

    public X509Certificate validate(String name, Stream inputStream) // throws CertificateValidationException
    {
        X509Certificate certificate = getCertificate(inputStream);
        validate(name, certificate);
        return certificate;
    }

    public X509Certificate validate(String name, byte[] bytes) // throws CertificateValidationException
    {
        X509Certificate certificate = getCertificate(bytes);
        validate(name, certificate);
        return certificate;
    }

    public bool isValid(String name, X509Certificate certificate)
    {
        try
        {
            validate(name, certificate);
            return true;
        }
        catch (CertificateValidationException e)
        {
            return false;
        }
    }

    public bool isValid(String name, Stream inputStream)
    {
        try
        {
            return isValid(name, getCertificate(inputStream));
        }
        catch (CertificateValidationException e)
        {
            return false;
        }
    }

    public bool isValid(String name, byte[] bytes)
    {
        try
        {
            return isValid(name, getCertificate(bytes));
        }
        catch (CertificateValidationException e)
        {
            return false;
        }
    }
}
}
