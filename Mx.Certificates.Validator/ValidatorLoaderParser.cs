using System;
using System.Collections.Generic;

namespace Mx.Certificates.Validator
{
    using System.IO;
    using System.Linq;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Rules;
    using Mx.Certificates.Validator.Structure;
    using Mx.Certificates.Validator.Util;
    using Mx.Tools;
    using Mx.Xml.tns;

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


    public class ValidatorLoaderParser
    {
        public static ValidatorGroup Parse(Stream inputStream, Dictionary<String, Object> objectStorage)
        {
            ValidatorRecipe recipe = new ValidatorRecipe(); // XmlTools.Read<ValidatorRecipe>(inputStream);
            recipe.FromXmlStream(inputStream);

            LoadKeyStores(recipe, objectStorage);
            LoadBuckets(recipe, objectStorage);

            Dictionary<String, IValidatorRule> rulesMap = new Dictionary<string, IValidatorRule>();

            foreach (ValidatorType validatorType in recipe.Validator)
            {
                var rules = validatorType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
                IValidatorRule validatorRule = Parse(
                    rules,
                    objectStorage,
                    Enumerations.JunctionEnum.AND);

                if (validatorType.ValidatorTypeData.Timeout != null)
                {
                    validatorRule = new CachedValidatorRule(validatorRule);
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
            LoadKeyStores(
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
            LoadBuckets(
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
                        certificateBucket.Add(Validator.GetCertificate(Convert.FromBase64String(o.Certificate)));
                    }
                    else if (o.CertificateReference != null)
                    {
                        CertificateReferenceType c = o.CertificateReference;
                        foreach (X509Certificate certificate in GetKeyStore(c.KeyStore, objectStorage).ToSimple(c.PrimitiveValue))
                        {
                            certificateBucket.Add(certificate);
                        }
                    }
                    else if (o.CertificateStartsWith != null)
                    {
                        CertificateStartsWithType c = o.CertificateStartsWith;
                        foreach (X509Certificate certificate in GetKeyStore(c.KeyStore, objectStorage).StartsWith(c.PrimitiveValue))
                        {
                            certificateBucket.Add(certificate);
                        }
                        
                    }
                }

                objectStorage.Add($"#bucket::{certificateBucketType.Name}", certificateBucket);
            }
        }


        private static IValidatorRule Parse(
            IEnumerable<ExtensibleType_Type_Group> rules,
            Dictionary<string, object> objectStorage,
            Enumerations.JunctionEnum junctionEnum) // throws CertificateValidationException
        {
            List<IValidatorRule> ruleList = new List<IValidatorRule>();

            foreach (ExtensibleType_Type_Group rule in rules)
            {
                ruleList.Add(Parse(rule, objectStorage));

                if (junctionEnum == Enumerations.JunctionEnum.AND)
                {
                    return Junction.And(ruleList.ToArray());
                }

                if (junctionEnum == Enumerations.JunctionEnum.OR)
                {
                    return Junction.Or(ruleList.ToArray());
                }
                
                // if (junctionEnum == JunctionEnum.XOR)
                return Junction.Xor(ruleList.ToArray());
            }

            throw new InvalidOperationException("Rules list cannot be empty");
        }

        private static IValidatorRule Parse(ExtensibleType_Type_Group rule, Dictionary<String, Object> objectStorage)
            //  throws CertificateValidationException
        {
            var node = rule.ChoiceSelectedElement;
            if (node == "Cached")
            {
                return Parse(rule.Cached, objectStorage);
            }

            if (node == "Chain")
            {
                return Parse(rule.Chain, objectStorage);
            }

            if (node == "Class")
            {
                throw new NotSupportedException();
                // return parse(rule.class_);
            }

            if (node == "CriticalExtensionRecognized")
            {
                return Parse(rule.CriticalExtensionRecognized);
            }

            if (node == "CriticalExtensionRequired")
            {
                return Parse(rule.CriticalExtensionRequired);
            }

            if (node == "CRL")
            {
                return Parse(rule.CRL, objectStorage);
            }

            //if (rule is DummyType)
            //{
            //    return parse((DummyType)rule);
            //}

            if (node == "Expiration")
            {
                return Parse(rule.Expiration);
            }

            if (node == "Junction")
            {
                return Parse(rule.Junction, objectStorage);
            }

            if (node == "KeyUsage")
            {
                return Parse(rule.KeyUsage);
            }

            if (node == "OCSP")
            {
                return Parse(rule.OCSP, objectStorage);
            }

            if (node == "HandleError")
            {
                return Parse(rule.HandleError, objectStorage);
            }

            if (node == "PrincipleName")
            {
                return Parse(rule.PrincipleName, objectStorage);
            }

            if (node == "RuleReferenceT")
            {
                return Parse(rule.RuleReference, objectStorage);
            }

            if (node == "Signing")
            {
                return Parse(rule.Signing);
            }

            if (node == "Try")
            {
                return Parse(rule.try_, objectStorage);
            }

            if (node == "ValidatorReference")
            {
                return Parse(rule.ValidatorReference, objectStorage);
            }
            // if (rule instanceof ValidatorReferenceType)
            // return parse((ValidatorReferenceType)rule, objectStorage);

            throw new NotSupportedException($"Nodo '{node}' non supportato");
        }

        private static IValidatorRule Parse(CachedType rule, Dictionary<string, object> objectStorage) // throws CertificateValidationException
        {
            var filteredRules = rule.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            
            return new CachedValidatorRule(
                Parse(filteredRules, objectStorage, Enumerations.JunctionEnum.AND));
        }

        private static IValidatorRule Parse(ChainType rule, Dictionary<string, object> objectStorage)
        {
            return new ChainRule(
                GetBucket(rule.RootBucketReference, objectStorage),
                GetBucket(rule.IntermediateBucketReference, objectStorage),
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

        private static IValidatorRule Parse(CriticalExtensionRecognizedType rule)
        {
            return new CriticalExtensionRecognizedRule(rule.Value.ToArray());
        }

        private static IValidatorRule Parse(CriticalExtensionRequiredType rule)
        {
            return new CriticalExtensionRequiredRule(rule.Value.ToArray());
        }

        // ReSharper disable once UnusedParameter.Local
        private static IValidatorRule Parse(CRLType rule, Dictionary<String, Object> objectStorage)
        {
            if (!objectStorage.ContainsKey("crlFetcher") && !objectStorage.ContainsKey("crlCache"))
            {
                objectStorage.Add("crlCache", new SimpleCrlCache());
            }


            if (!objectStorage.ContainsKey("crlFetcher"))
            {
                objectStorage.Add("crlFetcher", new SimpleCachingCrlFetcher((ICrlCache)objectStorage["crlCache"]));
            }

            return new CrlRule((ICrlFetcher)objectStorage["crlFetcher"]);
        }

        //private static ValidatorRule parse(DummyType dummyType)
        //{
        //    return new DummyRule(dummyType.Value);
        //}

        private static IValidatorRule Parse(ExpirationType expirationType)
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

        private static IValidatorRule Parse(HandleErrorType optionalType, Dictionary<String, Object> objectStorage)
            //  throws CertificateValidationException
        {
            List<IValidatorRule> validatorRules = new List<IValidatorRule>();
            var filteredRules = optionalType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            foreach (var o in filteredRules)
            {
                validatorRules.Add(Parse(o, objectStorage));
            }

            return new HandleErrorRule(validatorRules);
        }

        private static IValidatorRule Parse(JunctionType junctionType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            var filteredRules = junctionType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            return Parse(filteredRules, objectStorage, junctionType.JunctionTypeData.Type);
        }

        private static IValidatorRule Parse(KeyUsageType keyUsageType)
        {
            var keyUsages = keyUsageType.Identifier.ToList();
            Org.BouncyCastle.Asn1.X509.KeyUsage[] result = new Org.BouncyCastle.Asn1.X509.KeyUsage[keyUsages.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Org.BouncyCastle.Asn1.X509.KeyUsage((int) keyUsages[i]);
            }

            return new KeyUsageRule(result);
        }

        private static IValidatorRule Parse(OCSPType ocspType, Dictionary<String, Object> objectStorage)
        {
            return new OcspRule(GetBucket(ocspType.IntermediateBucketReference, objectStorage));
        }

        private static IValidatorRule Parse(RuleReferenceType ruleReferenceType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            if (!objectStorage.ContainsKey(ruleReferenceType.PrimitiveValue))
            {
                throw new CertificateValidationException(
                    String.Format("Rule for '{0}' not found.", ruleReferenceType));
            }

            return (IValidatorRule)objectStorage[ruleReferenceType.PrimitiveValue];
        }

        // uppressWarnings("unchecked")
        private static IValidatorRule Parse(PrincipleNameType principleNameType, Dictionary<String, Object> objectStorage)
        {
            IPrincipalNameProvider<String> principalNameProvider;
            if (principleNameType.Reference != null)
            {
                principalNameProvider =
                    (IPrincipalNameProvider<String>)objectStorage[principleNameType.Reference.PrimitiveValue];
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

        private static IValidatorRule Parse(SigningType signingType)
        {
            if (signingType.Type.Equals(Enumerations.SigningEnum.SELF_SIGNED))
            {
                return SigningRule.SelfSignedOnly();
            }
            else
            {
                return SigningRule.PublicSignedOnly();
            }
        }

        private static IValidatorRule Parse(TryType tryType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            var filteredRules = tryType.ExtensibleTypeData[0].ExtensibleType_Type_Group.ToList();
            foreach (var rule in filteredRules) {
                try
                {
                    return Parse(rule, objectStorage);
                }
                catch (Exception)
                {
                    // No action
                }
            }

            throw new CertificateValidationException("Unable to find valid rule in try.");
        }

        private static IValidatorRule Parse(
                ValidatorReferenceType validatorReferenceType,
                Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            String identifier = $"#validator::{validatorReferenceType.PrimitiveValue}";
            if (!objectStorage.ContainsKey(identifier))
            {
                throw new CertificateValidationException($"Unable to find validator '{validatorReferenceType.PrimitiveValue}'.");
            }

            return (IValidatorRule)objectStorage[identifier];
        }

        // HELPERS

        private static KeyStoreCertificateBucket GetKeyStore(String name, Dictionary<String, Object> objectStorage)
        {
            var key = $"#keyStore::{name ?? "default"}";
            return (KeyStoreCertificateBucket)objectStorage[key];
        }

        private static ICertificateBucket GetBucket(String name, Dictionary<String, Object> objectStorage)
        {
            var key = $"#bucket::{name}";
            return (ICertificateBucket)objectStorage[key];
        }
    }

}