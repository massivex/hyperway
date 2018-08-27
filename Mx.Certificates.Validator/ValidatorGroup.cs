using System.Collections.Generic;

namespace Mx.Certificates.Validator
{
    using System.IO;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public class ValidatorGroup : Validator
    {

    private readonly Dictionary<string, IValidatorRule> rulesMap;

    private readonly string name;

    private readonly string version;

    public ValidatorGroup(Dictionary<string, IValidatorRule> rulesMap) : base(null)
    {
        this.rulesMap = rulesMap;
    }

    public ValidatorGroup(Dictionary<string, IValidatorRule> rulesMap, string name, string version): this(rulesMap)
    {
        this.name = name;
        this.version = version;
    }

    public string GetName()
    {
        return this.name;
    }

    public string GetVersion()
    {
        return this.version;
    }

    public override void Validate(X509Certificate certificate)
    {
        this.Validate("default", certificate);
    }

    public void Validate(string ruleName, X509Certificate certificate)
    {
        if (!this.rulesMap.ContainsKey(ruleName))
        {
            throw new CertificateValidationException($"Unknown validator '{ruleName}'.");
        }

        this.rulesMap[ruleName].Validate(certificate);
    }

    public X509Certificate Validate(string ruleName, Stream inputStream)
    {
        X509Certificate certificate = GetCertificate(inputStream);
        this.Validate(ruleName, certificate);
        return certificate;
    }

    public X509Certificate Validate(string ruleName, byte[] bytes)
    {
        X509Certificate certificate = GetCertificate(bytes);
        this.Validate(ruleName, certificate);
        return certificate;
    }

    public bool IsValid(string ruleName, X509Certificate certificate)
    {
        try
        {
            this.Validate(ruleName, certificate);
            return true;
        }
        catch (CertificateValidationException)
        {
            return false;
        }
    }

    public bool IsValid(string nameToValidate, Stream inputStream)
    {
        try
        {
            return this.IsValid(nameToValidate, GetCertificate(inputStream));
        }
        catch (CertificateValidationException)
        {
            return false;
        }
    }

    public bool IsValid(string nameToValidate, byte[] bytes)
    {
        try
        {
            return this.IsValid(nameToValidate, GetCertificate(bytes));
        }
        catch (CertificateValidationException)
        {
            return false;
        }
    }
}
}
