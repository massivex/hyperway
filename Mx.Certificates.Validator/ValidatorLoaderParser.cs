using System;
using System.Collections.Generic;
using System.Text;

using Mx.Certificates.Validator.Api;

namespace Mx.Certificates.Validator
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Lang;
    using Mx.Certificates.Validator.Rules;
    using Mx.Certificates.Validator.Structure;
    using Mx.Certificates.Validator.Util;
    using Mx.Tools;
    using Mx.Xml.tns;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto.Tls;
    using Org.BouncyCastle.X509;

    using CachedType = Mx.Xml.tns.CachedType;
    using CertificateBucketType = Mx.Xml.tns.CertificateBucketType;
    using CertificateReferenceType = Mx.Xml.tns.CertificateReferenceType;
    using CertificateStartsWithType = Mx.Xml.tns.CertificateStartsWithType;
    using ChainType = Mx.Xml.tns.ChainType;
    using CriticalExtensionRecognizedType = Mx.Xml.tns.CriticalExtensionRecognizedType;
    using CriticalExtensionRequiredType = Mx.Xml.tns.CriticalExtensionRequiredType;
    using CRLType = Mx.Xml.tns.CRLType;
    using ExpirationType = Mx.Xml.tns.ExpirationType;
    using HandleErrorType = Mx.Xml.tns.HandleErrorType;
    using JunctionType = Mx.Xml.tns.JunctionType;
    using KeyStoreType = Mx.Xml.tns.KeyStoreType;
    using KeyUsageType = Mx.Xml.tns.KeyUsageType;
    using OCSPType = Mx.Xml.tns.OCSPType;
    using PrincipleNameType = Mx.Xml.tns.PrincipleNameType;
    using RuleReferenceType = Mx.Xml.tns.RuleReferenceType;
    using SigningType = Mx.Xml.tns.SigningType;
    using TryType = Mx.Xml.tns.TryType;
    using ValidatorRecipe = Mx.Xml.tns.ValidatorRecipe;
    using ValidatorReferenceType = Mx.Xml.tns.ValidatorReferenceType;
    using ValidatorType = Mx.Xml.tns.ValidatorType;

    /// using Xml = Mx.Certificates.Validator.Xml;


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

        public static ValidatorGroup
            parse(Stream inputStream, Dictionary<String, Object> objectStorage) // throws ValidatorParsingException
        {
            ValidatorRecipe recipe = new ValidatorRecipe(); // XmlTools.Read<ValidatorRecipe>(inputStream);
            recipe.FromXmlStream(inputStream);

            loadKeyStores(recipe, objectStorage);
            loadBuckets(recipe, objectStorage);

            Dictionary<String, ValidatorRule> rulesMap = new Dictionary<string, ValidatorRule>();

            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType) };
            foreach (ValidatorType validatorType in recipe.Validator)
            {
                var rules = validatorType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
                ValidatorRule validatorRule = parse(
                    rules,
                    objectStorage,
                    Enumerations.JunctionEnum.AND);

                if (validatorType.ValidatorTypeData.Timeout != null)
                {
                    validatorRule = new CachedValidatorRule(validatorRule, validatorType.ValidatorTypeData.Timeout ?? -1);
                }

                String name = validatorType.ValidatorTypeData.Name == null ? "default" : validatorType.ValidatorTypeData.Name;
                rulesMap.Add(name, validatorRule);
                objectStorage.Add(String.Format("#validator::{0}", name), validatorRule);
            }

            return new ValidatorGroup(rulesMap, recipe.Name, recipe.Version);
            //    } catch (JAXBException | CertificateValidationException e) {
            //                    throw new ValidatorParsingException(e.getMessage(), e);
            //}
        }

        private static void
            loadKeyStores(
                ValidatorRecipe recipe,
                Dictionary<String, Object> objectStorage) // throws CertificateValidationException
        {
            foreach (KeyStoreType keyStoreType in recipe.KeyStore)
            {
                var key = String.Format("#keyStore::{0}", keyStoreType.Name ?? "default");
                var value = new KeyStoreCertificateBucket(
                    Convert.FromBase64String(keyStoreType.PrimitiveValue.Base64Encoded).ToStream(),
                    keyStoreType.Password);

                objectStorage.Add(key, value);
            }
        }

        private static void
            loadBuckets(
                ValidatorRecipe recipe,
                Dictionary<String, Object> objectStorage) // throws CertificateValidationException
        {
            foreach (CertificateBucketType certificateBucketType in recipe.CertificateBucket)
            {
                SimpleCertificateBucket certificateBucket = new SimpleCertificateBucket();

                foreach (CertificateBucketType_Group o in certificateBucketType.CertificateBucketType_Group)
                {
                    if (!string.IsNullOrWhiteSpace(o.Certificate))
                    {
                        certificateBucket.add(Validator.getCertificate(Convert.FromBase64String(o.Certificate)));
                    }
                    else if (o.CertificateReference != null)
                    {
                        CertificateReferenceType c = o.CertificateReference;
                        foreach (X509Certificate certificate in getKeyStore(c.KeyStore, objectStorage).toSimple(c.PrimitiveValue))
                        {
                            certificateBucket.add(certificate);
                        }
                    }
                    else if (o.CertificateStartsWith != null)
                    {
                        CertificateStartsWithType c = o.CertificateStartsWith;
                        foreach (X509Certificate certificate in getKeyStore(c.KeyStore, objectStorage).startsWith(c.PrimitiveValue))
                        {
                            certificateBucket.add(certificate);
                        }
                        
                    }
                }

                objectStorage.Add($"#bucket::{certificateBucketType.Name}", certificateBucket);
            }
        }


        private static ValidatorRule parse(
            IEnumerable<ExtensibleType_Type_Group> rules,
            Dictionary<string, object> objectStorage,
            Enumerations.JunctionEnum junctionEnum) // throws CertificateValidationException
        {
            List<ValidatorRule> ruleList = new List<ValidatorRule>();

            foreach (ExtensibleType_Type_Group rule in rules)
            {
                ruleList.Add(parse(rule, objectStorage));

                if (junctionEnum == Enumerations.JunctionEnum.AND)
                {
                    return Junction.and(ruleList.ToArray());
                }

                if (junctionEnum == Enumerations.JunctionEnum.OR)
                {
                    return Junction.or(ruleList.ToArray());
                }
                
                // if (junctionEnum == JunctionEnum.XOR)
                return Junction.xor(ruleList.ToArray());
            }

            throw new InvalidOperationException("Rules list cannot be empty");
        }

        private static ValidatorRule parse(ExtensibleType_Type_Group rule, Dictionary<String, Object> objectStorage)
            //  throws CertificateValidationException
        {
            var node = rule.ChoiceSelectedElement;
            if (node == "Cached")
            {
                return parse(rule.Cached, objectStorage);
            }

            if (node == "Chain")
            {
                return parse(rule.Chain, objectStorage);
            }

            if (node == "Class")
            {
                throw new NotSupportedException();
                // return parse(rule.class_);
            }

            if (node == "CriticalExtensionRecognized")
            {
                return parse(rule.CriticalExtensionRecognized);
            }

            if (node == "CriticalExtensionRequired")
            {
                return parse(rule.CriticalExtensionRequired);
            }

            if (node == "CRL")
            {
                return parse(rule.CRL, objectStorage);
            }

            //if (rule is DummyType)
            //{
            //    return parse((DummyType)rule);
            //}

            if (node == "Expiration")
            {
                return parse(rule.Expiration);
            }

            if (node == "Junction")
            {
                return parse(rule.Junction, objectStorage);
            }

            if (node == "KeyUsage")
            {
                return parse(rule.KeyUsage);
            }

            if (node == "OCSP")
            {
                return parse(rule.OCSP, objectStorage);
            }

            if (node == "HandleError")
            {
                return parse(rule.HandleError, objectStorage);
            }

            if (node == "PrincipleName")
            {
                return parse(rule.PrincipleName, objectStorage);
            }

            if (node == "RuleReferenceT")
            {
                return parse(rule.RuleReference, objectStorage);
            }

            if (node == "Signing")
            {
                return parse(rule.Signing);
            }

            if (node == "Try")
            {
                return parse(rule.try_, objectStorage);
            }

            if (node == "ValidatorReference")
            {
                return parse(rule.ValidatorReference, objectStorage);
            }
            // if (rule instanceof ValidatorReferenceType)
            // return parse((ValidatorReferenceType)rule, objectStorage);

            throw new NotSupportedException($"Nodo '{node}' non supportato");
        }

        private static ValidatorRule parse(CachedType rule, Dictionary<string, object> objectStorage) // throws CertificateValidationException
        {
            var filteredRules = rule.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            
            return new CachedValidatorRule(
                parse(filteredRules, objectStorage, Enumerations.JunctionEnum.AND),
                rule.CachedTypeData.Timeout);
        }

        private static ValidatorRule parse(ChainType rule, Dictionary<string, object> objectStorage)
        {
            return new ChainRule(
                getBucket(rule.RootBucketReference, objectStorage),
                getBucket(rule.IntermediateBucketReference, objectStorage),
                rule.Policy.ToArray());
        }

        //private static ValidatorRule parse(ClassType rule) // throws CertificateValidationException
        //{
        //    try
        //    {
        //        var classType = Type.GetType(rule.Value);
        //        Debug.Assert(classType != null, nameof(classType) + " != null");
        //        return (ValidatorRule)Activator.CreateInstance(classType);
        //    }
        //    catch (Exception e) {
        //        throw new CertificateValidationException($"Unable to load rule '{rule.Value}'.", e);
        //    }
        //}

        private static ValidatorRule parse(CriticalExtensionRecognizedType rule)
        {
            return new CriticalExtensionRecognizedRule(rule.Value.ToArray());
        }

        private static ValidatorRule parse(CriticalExtensionRequiredType rule)
        {
            return new CriticalExtensionRequiredRule(rule.Value.ToArray());
        }

        private static ValidatorRule parse(CRLType rule, Dictionary<String, Object> objectStorage)
        {
            if (!objectStorage.ContainsKey("crlFetcher") && !objectStorage.ContainsKey("crlCache"))
            {
                objectStorage.Add("crlCache", new SimpleCrlCache());
            }


            if (!objectStorage.ContainsKey("crlFetcher"))
            {
                objectStorage.Add("crlFetcher", new SimpleCachingCrlFetcher((CrlCache)objectStorage["crlCache"]));
            }

            return new CRLRule((CrlFetcher)objectStorage["crlFetcher"]);
        }

        //private static ValidatorRule parse(DummyType dummyType)
        //{
        //    return new DummyRule(dummyType.Value);
        //}

        private static ValidatorRule parse(ExpirationType expirationType)
        {
            if (expirationType.Millis == 0)
            {
                return new ExpirationRule();
            }
            else
            {
                return new ExpirationSoonRule(expirationType.Millis ?? -1);
            }
        }

        private static ValidatorRule parse(HandleErrorType optionalType, Dictionary<String, Object> objectStorage)
            //  throws CertificateValidationException
        {
            List<ValidatorRule> validatorRules = new List<ValidatorRule>();
            var filteredRules = optionalType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            foreach (var o in filteredRules)
            {
                validatorRules.Add(parse(o, objectStorage));
            }

            return new HandleErrorRule(validatorRules);
        }

        private static ValidatorRule parse(JunctionType junctionType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            var filteredRules = junctionType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            return parse(filteredRules, objectStorage, junctionType.JunctionTypeData.Type);
        }

        private static ValidatorRule parse(KeyUsageType keyUsageType)
        {
            var keyUsages = keyUsageType.Identifier.ToList();
            Org.BouncyCastle.Asn1.X509.KeyUsage[] result = new Org.BouncyCastle.Asn1.X509.KeyUsage[keyUsages.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Org.BouncyCastle.Asn1.X509.KeyUsage((int) keyUsages[i]);
            }

            return new KeyUsageRule(result);
        }

        private static ValidatorRule parse(OCSPType ocspType, Dictionary<String, Object> objectStorage)
        {
            return new OCSPRule(getBucket(ocspType.IntermediateBucketReference, objectStorage));
        }

        private static ValidatorRule parse(RuleReferenceType ruleReferenceType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            if (!objectStorage.ContainsKey(ruleReferenceType.PrimitiveValue))
            {
                throw new CertificateValidationException(
                    String.Format("Rule for '{0}' not found.", ruleReferenceType));
            }

            return (ValidatorRule)objectStorage[ruleReferenceType.PrimitiveValue];
        }

        // uppressWarnings("unchecked")
        private static ValidatorRule parse(PrincipleNameType principleNameType, Dictionary<String, Object> objectStorage)
        {
            PrincipalNameProvider<String> principalNameProvider;
            if (principleNameType.Reference != null)
            {
                principalNameProvider =
                    (PrincipalNameProvider<String>)objectStorage[principleNameType.Reference.PrimitiveValue];
            }
            else
            {
                principalNameProvider = new SimplePrincipalNameProvider(principleNameType.Value);
            }

            return new PrincipalNameRule(
                principleNameType.Field,
                principalNameProvider,
                principleNameType.Principal ?? Enumerations.PrincipalEnum.SUBJECT);
        }

        private static ValidatorRule parse(SigningType signingType)
        {
            if (signingType.Type.Equals(Enumerations.SigningEnum.SELF_SIGNED.ToString()))
            {
                return SigningRule.SelfSignedOnly();
            }
            else
            {
                return SigningRule.PublicSignedOnly();
            }
        }

        private static ValidatorRule parse(TryType tryType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            var filteredRules = tryType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            foreach (var rule in filteredRules) {
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

        private static ValidatorRule parse(
                ValidatorReferenceType validatorReferenceType,
                Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            String identifier = $"#validator::{validatorReferenceType.PrimitiveValue}";
            if (!objectStorage.ContainsKey(identifier))
            {
                throw new CertificateValidationException($"Unable to find validator '{validatorReferenceType.PrimitiveValue}'.");
            }

            return (ValidatorRule)objectStorage[identifier];
        }

        // HELPERS

        private static KeyStoreCertificateBucket getKeyStore(String name, Dictionary<String, Object> objectStorage)
        {
            var key = $"#keyStore::{name ?? "default"}";
            return (KeyStoreCertificateBucket)objectStorage[key];
        }

        private static CertificateBucket getBucket(String name, Dictionary<String, Object> objectStorage)
        {
            var key = $"#bucket::{name}";
            return (CertificateBucket)objectStorage[key];
        }
    }

}