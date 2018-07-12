﻿using System;
using System.Collections.Generic;
using System.Text;

using Mx.Certificates.Validator.Api;

namespace Mx.Certificates.Validator
{
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Lang;
    using Mx.Certificates.Validator.Xml;

    using Org.BouncyCastle.Crypto.Tls;

    public class ValidatorLoaderParser
    {

        // private static JAXBContext jaxbContext;

//        static {
//        try {
//            jaxbContext = JAXBContext.newInstance(ValidatorRecipe.class);
//        } catch (JAXBException e) {
//            throw new RuntimeException(e.getMessage(), e);
//}
//    }

    public static ValidatorGroup parse(Stream inputStream, Dictionary<String, Object> objectStorage) // throws ValidatorParsingException
{
    // TODO: XmlDeserialization missing
    throw new NotSupportedException();
//        try {
//        ValidatorRecipe recipe = jaxbContext.createUnmarshaller()
//                .unmarshal(new StreamSource(inputStream), ValidatorRecipe.class)
//                    .getValue();

//loadKeyStores(recipe, objectStorage);
//loadBuckets(recipe, objectStorage);

//Map<String, ValidatorRule> rulesMap = new HashMap<>();

//            for (ValidatorType validatorType : recipe.getValidator()) {
//                ValidatorRule validatorRule = parse(validatorType.getCachedOrChainOrClazz(), objectStorage, JunctionEnum.AND);

//                if (validatorType.getTimeout() != null)
//                    validatorRule = new CachedValidatorRule(validatorRule, validatorType.getTimeout());

//String name = validatorType.getName() == null ? "default" : validatorType.getName();
//rulesMap.put(name, validatorRule);
//                objectStorage.put(String.format("#validator::%s", name), validatorRule);
//            }

//            return new ValidatorGroup(rulesMap, recipe.getName(), recipe.getVersion());
//        } catch (JAXBException | CertificateValidationException e) {
//            throw new ValidatorParsingException(e.getMessage(), e);
//        }
    }

    private static void loadKeyStores(ValidatorRecipe recipe, Dictionary<String, Object> objectStorage) // throws CertificateValidationException
{
        foreach (KeyStoreType keyStoreType in recipe.getKeyStore()) {
        objectStorage.put(
                String.format("#keyStore::%s", keyStoreType.getName() == null ? "default" : keyStoreType.getName()),
                new KeyStoreCertificateBucket(
                        new ByteArrayInputStream(keyStoreType.getValue()),
                        keyStoreType.getPassword()
                )
        );
    }
}

private static void loadBuckets(ValidatorRecipe recipe, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
        for (CertificateBucketType certificateBucketType : recipe.getCertificateBucket()) {
        SimpleCertificateBucket certificateBucket = new SimpleCertificateBucket();

        for (Object o : certificateBucketType.getCertificateOrCertificateReferenceOrCertificateStartsWith())
        {
            if (o instanceof CertificateType) {
            certificateBucket.add(Validator.getCertificate(((CertificateType)o).getValue()));
        } else if (o instanceof CertificateReferenceType) {
            CertificateReferenceType c = (CertificateReferenceType)o;
            for (X509Certificate certificate :
                            getKeyStore(c.getKeyStore(), objectStorage).toSimple(c.getValue()))
                certificateBucket.add(certificate);
        } else /* if (o instanceof CertificateStartsWithType) */ {
            CertificateStartsWithType c = (CertificateStartsWithType)o;
            for (X509Certificate certificate :
                            getKeyStore(c.getKeyStore(), objectStorage).startsWith(c.getValue()))
                certificateBucket.add(certificate);
        }
    }

    objectStorage.put(String.format("#bucket::%s", certificateBucketType.getName()), certificateBucket);
}
    }

    private static ValidatorRule parse(List<Object> rules, Map<String, Object> objectStorage,
                                       JunctionEnum junctionEnum) throws CertificateValidationException
{
    List<ValidatorRule> ruleList = new ArrayList<>();

        for (Object rule : rules)
            ruleList.add(parse(rule, objectStorage));

        if (junctionEnum == JunctionEnum.AND)
            return Junction.and(ruleList.toArray(new ValidatorRule[ruleList.size()]));
        else if (junctionEnum == JunctionEnum.OR)
            return Junction.or(ruleList.toArray(new ValidatorRule[ruleList.size()]));
        else // if (junctionEnum == JunctionEnum.XOR)
            return Junction.xor(ruleList.toArray(new ValidatorRule[ruleList.size()]));
    }

    private static ValidatorRule parse(Object rule, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
        if (rule is CachedType)
            return parse((CachedType) rule, objectStorage);
        else if (rule is ChainType)
            return parse((ChainType) rule, objectStorage);
        else if (rule is ClassType)
            return parse((ClassType) rule);
        else if (rule is CriticalExtensionRecognizedType)
            return parse((CriticalExtensionRecognizedType) rule);
        else if (rule is CriticalExtensionRequiredType)
            return parse((CriticalExtensionRequiredType) rule);
        else if (rule is CRLType)
            return parse((CRLType) rule, objectStorage);
        else if (rule is DummyType)
            return parse((DummyType) rule);
        else if (rule is ExpirationType)
            return parse((ExpirationType) rule);
        else if (rule is JunctionType)
            return parse((JunctionType) rule, objectStorage);
        else if (rule is KeyUsageType)
            return parse((KeyUsageType) rule);
        else if (rule is OCSPType)
            return parse((OCSPType) rule, objectStorage);
        else if (rule is HandleErrorType)
            return parse((HandleErrorType) rule, objectStorage);
        else if (rule is PrincipleNameType)
            return parse((PrincipleNameType) rule, objectStorage);
        else if (rule si RuleReferenceType)
            return parse((RuleReferenceType) rule, objectStorage);
        else if (rule is SigningType)
            return parse((SigningType) rule);
        else if (rule is TryType)
            return parse((TryType) rule, objectStorage);
        else // if (rule instanceof ValidatorReferenceType)
            return parse((ValidatorReferenceType) rule, objectStorage);
}

private static ValidatorRule parse(CachedType rule, Map<String, Object> objectStorage) throws
        CertificateValidationException
{
        return new CachedValidatorRule(
                parse(rule.getCachedOrChainOrClazz(), objectStorage, JunctionEnum.AND),
                rule.getTimeout()
        );
    }

    private static ValidatorRule parse(ChainType rule, Map<String, Object> objectStorage)
{
    return new ChainRule(
            getBucket(rule.getRootBucketReference().getValue(), objectStorage),
            getBucket(rule.getIntermediateBucketReference().getValue(), objectStorage),
            rule.getPolicy().toArray(new String[rule.getPolicy().size()])
    );
}

private static ValidatorRule parse(ClassType rule) throws CertificateValidationException
{
        try {
        return (ValidatorRule)Class.forName(rule.getValue()).newInstance();
    } catch (ClassNotFoundException | InstantiationException | IllegalAccessException e) {
        throw new CertificateValidationException(String.format("Unable to load rule '%s'.", rule.getValue()), e);
    }
}

private static ValidatorRule parse(CriticalExtensionRecognizedType rule)
{
    return new CriticalExtensionRecognizedRule(rule.getValue().toArray(new String[rule.getValue().size()]));
}

private static ValidatorRule parse(CriticalExtensionRequiredType rule)
{
    return new CriticalExtensionRequiredRule(rule.getValue().toArray(new String[rule.getValue().size()]));
}

private static ValidatorRule parse(CRLType rule, Map<String, Object> objectStorage)
{
    if (!objectStorage.containsKey("crlFetcher") && !objectStorage.containsKey("crlCache"))
        objectStorage.put("crlCache", new SimpleCrlCache());

    if (!objectStorage.containsKey("crlFetcher"))
        objectStorage.put("crlFetcher", new SimpleCachingCrlFetcher((CrlCache)objectStorage.get("crlCache")));

    return new CRLRule((CrlFetcher)objectStorage.get("crlFetcher"));
}

private static ValidatorRule parse(DummyType dummyType)
{
    return new DummyRule(dummyType.getValue());
}

private static ValidatorRule parse(ExpirationType expirationType)
{
    if (expirationType.getMillis() == null)
        return new ExpirationRule();
    else
        return new ExpirationSoonRule(expirationType.getMillis());
}

private static ValidatorRule parse(HandleErrorType optionalType, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
    List<ValidatorRule> validatorRules = new ArrayList<>();
        for (Object o : optionalType.getCachedOrChainOrClazz())
            validatorRules.add(parse(o, objectStorage));

        return new HandleErrorRule(validatorRules);
    }

    private static ValidatorRule parse(JunctionType junctionType, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
        return parse(junctionType.getCachedOrChainOrClazz(),
                objectStorage, junctionType.getType());
}

private static ValidatorRule parse(KeyUsageType keyUsageType)
{
    List<KeyUsageEnum> keyUsages = keyUsageType.getIdentifier();
    KeyUsage[] result = new KeyUsage[keyUsages.size()];

    for (int i = 0; i < result.length; i++)
        result[i] = KeyUsage.valueOf(keyUsages.get(i).name());

    return new KeyUsageRule(result);
}

private static ValidatorRule parse(OCSPType ocspType, Map<String, Object> objectStorage)
{
    return new OCSPRule(getBucket(ocspType.getIntermediateBucketReference().getValue(), objectStorage));
}

private static ValidatorRule parse(RuleReferenceType ruleReferenceType, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
        if (!objectStorage.containsKey(ruleReferenceType.getValue()))
            throw new CertificateValidationException(
                    String.format("Rule for '%s' not found.", ruleReferenceType.getValue()));

        return (ValidatorRule) objectStorage.get(ruleReferenceType.getValue());
    }

    @SuppressWarnings("unchecked")
    private static ValidatorRule parse(PrincipleNameType principleNameType, Map<String, Object> objectStorage)
{
    PrincipalNameProvider<String> principalNameProvider;
    if (principleNameType.getReference() != null)
        principalNameProvider = (PrincipalNameProvider<String>)objectStorage.get(principleNameType.getReference().getValue());
    else
        principalNameProvider = new SimplePrincipalNameProvider(principleNameType.getValue());

    return new PrincipalNameRule(
            principleNameType.getField(),
            principalNameProvider,
            principleNameType.getPrincipal() != null ?
                    PrincipalNameRule.Principal.valueOf(principleNameType.getPrincipal().toString()) : PrincipalNameRule.Principal.SUBJECT
    );
}

private static ValidatorRule parse(SigningType signingType)
{
    if (signingType.getType().equals(SigningEnum.SELF_SIGNED))
        return SigningRule.SelfSignedOnly();
    else
        return SigningRule.PublicSignedOnly();
}

private static ValidatorRule parse(TryType tryType, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
        for (Object rule : tryType.getCachedOrChainOrClazz()) {
        try
        {
            return parse(rule, objectStorage);
        }
        catch (Exception e)
        {
            // No action
        }
    }

        throw new CertificateValidationException("Unable to find valid rule in try.");
    }

    private static ValidatorRule parse(ValidatorReferenceType validatorReferenceType, Map<String, Object> objectStorage)
            throws CertificateValidationException
{
    String identifier = String.format("#validator::%s", validatorReferenceType.getValue());
        if (!objectStorage.containsKey(identifier))
            throw new CertificateValidationException(
                    String.format("Unable to find validator '%s'.", validatorReferenceType.getValue()));

        return (ValidatorRule) objectStorage.get(identifier);
    }

    // HELPERS

    private static KeyStoreCertificateBucket getKeyStore(String name, Map<String, Object> objectStorage)
{
    return (KeyStoreCertificateBucket)objectStorage.get(
            String.format("#keyStore::%s", name == null ? "default" : name));
}

private static CertificateBucket getBucket(String name, Map<String, Object> objectStorage)
{
    return (CertificateBucket)objectStorage.get(String.format("#bucket::%s", name));
}
}

}
