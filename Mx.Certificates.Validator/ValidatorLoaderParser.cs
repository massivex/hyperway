using System;
using System.Collections.Generic;
using System.Text;

using Mx.Certificates.Validator.Api;

namespace Mx.Certificates.Validator
{
    using System.Diagnostics;
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Lang;
    using Mx.Certificates.Validator.Rules;
    using Mx.Certificates.Validator.Structure;
    using Mx.Certificates.Validator.Util;
    using Mx.Certificates.Validator.Xml;
    using Mx.Tools;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto.Tls;
    using Org.BouncyCastle.X509;
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
            ValidatorRecipe recipe = XmlTools.Read<ValidatorRecipe>(inputStream);

            loadKeyStores(recipe, objectStorage);
            loadBuckets(recipe, objectStorage);

            Dictionary<String, ValidatorRule> rulesMap = new Dictionary<string, ValidatorRule>();

            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType), typeof(ClassType) };
            foreach (ValidatorType validatorType in recipe.Validators)
            {


                ValidatorRule validatorRule = parse(
                    validatorType.Rules.OfTypes(typeFilter),
                    objectStorage,
                    JunctionEnum.AND);

                if (validatorType.Timeout != null)
                {
                    validatorRule = new CachedValidatorRule(validatorRule, validatorType.Timeout ?? -1);
                }

                String name = validatorType.Name == null ? "default" : validatorType.Name;
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
            foreach (KeyStoreType keyStoreType in recipe.KeyStores)
            {
                var key = String.Format("#keyStore::{0}", keyStoreType.Name ?? "default");
                var value = new KeyStoreCertificateBucket(
                    Convert.FromBase64String(keyStoreType.Value).ToStream(),
                    keyStoreType.Password);

                objectStorage.Add(key, value);
            }
        }

        private static void
            loadBuckets(
                ValidatorRecipe recipe,
                Dictionary<String, Object> objectStorage) // throws CertificateValidationException
        {
            foreach (CertificateBucketType certificateBucketType in recipe.CertificateBuckets)
            {
                SimpleCertificateBucket certificateBucket = new SimpleCertificateBucket();

                foreach (Object o in certificateBucketType
                    .getCertificateOrCertificateReferenceOrCertificateStartsWith())
                {
                    if (o is Xml.CertificateType)
                    {
                        certificateBucket.add(Validator.getCertificate(((Xml.CertificateType)o).AsBuffer()));
                    }
                    else if (o is CertificateReferenceType)
                    {
                        CertificateReferenceType c = (CertificateReferenceType)o;
                        foreach (X509Certificate certificate in getKeyStore(c.KeyStore, objectStorage).toSimple(c.Value))
                        {
                            certificateBucket.add(certificate);
                        }
                    }
                    else /* if (o instanceof CertificateStartsWithType) */
                    {
                        CertificateStartsWithType c = (CertificateStartsWithType)o;
                        foreach (X509Certificate certificate in getKeyStore(c.KeyStore, objectStorage).startsWith(c.Value))
                        {
                            certificateBucket.add(certificate);
                        }
                        
                    }
                }

                objectStorage.Add($"#bucket::{certificateBucketType.Name}", certificateBucket);
            }
        }


        private static ValidatorRule parse(
            IEnumerable<Object> rules,
            Dictionary<string, object> objectStorage,
            JunctionEnum junctionEnum) // throws CertificateValidationException
        {
            List<ValidatorRule> ruleList = new List<ValidatorRule>();

            foreach (Object rule in rules)
            {
                ruleList.Add(parse(rule, objectStorage));

                if (junctionEnum == JunctionEnum.AND)
                {
                    return Junction.and(ruleList.ToArray());
                }

                if (junctionEnum == JunctionEnum.OR)
                {
                    return Junction.or(ruleList.ToArray());
                }
                
                // if (junctionEnum == JunctionEnum.XOR)
                return Junction.xor(ruleList.ToArray());
            }

            throw new InvalidOperationException("Rules list cannot be empty");
        }

        private static ValidatorRule parse(Object rule, Dictionary<String, Object> objectStorage)
            //  throws CertificateValidationException
        {
            if (rule is CachedType)
            {
                return parse((CachedType)rule, objectStorage);
            }

            if (rule is ChainType)
            {
                return parse((ChainType)rule, objectStorage);
            }

            if (rule is ClassType)
            {
                return parse((ClassType)rule);
            }

            if (rule is CriticalExtensionRecognizedType)
            {
                return parse((CriticalExtensionRecognizedType)rule);
            }

            if (rule is CriticalExtensionRequiredType)
            {
                return parse((CriticalExtensionRequiredType)rule);
            }

            if (rule is CRLType)
            {
                return parse((CRLType)rule, objectStorage);
            }

            if (rule is DummyType)
            {
                return parse((DummyType)rule);
            }

            if (rule is ExpirationType)
            {
                return parse((ExpirationType)rule);
            }

            if (rule is JunctionType)
            {
                return parse((JunctionType)rule, objectStorage);
            }

            if (rule is KeyUsageType)
            {
                return parse((KeyUsageType)rule);
            }

            if (rule is OCSPType)
            {
                return parse((OCSPType)rule, objectStorage);
            }

            if (rule is HandleErrorType)
            {
                return parse((HandleErrorType)rule, objectStorage);
            }

            if (rule is PrincipleNameType)
            {
                return parse((PrincipleNameType)rule, objectStorage);
            }

            if (rule is RuleReferenceType)
            {
                return parse((RuleReferenceType)rule, objectStorage);
            }

            if (rule is SigningType)
            {
                return parse((SigningType)rule);
            }

            if (rule is TryType)
            {
                return parse((TryType)rule, objectStorage);
            }
            
            // if (rule instanceof ValidatorReferenceType)
            return parse((ValidatorReferenceType)rule, objectStorage);
        }

        private static ValidatorRule parse(CachedType rule, Dictionary<string, object> objectStorage) // throws CertificateValidationException
        {
            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType), typeof(ClassType) };
            return new CachedValidatorRule(
                parse(rule.Rules.OfTypes(typeFilter), objectStorage, JunctionEnum.AND),
                rule.Timeout);
        }

        private static ValidatorRule parse(ChainType rule, Dictionary<string, object> objectStorage)
        {
            return new ChainRule(
                getBucket(rule.RootBucketReference, objectStorage),
                getBucket(rule.IntermediateBucketReference, objectStorage),
                rule.Policies.ToArray());
        }

        private static ValidatorRule parse(ClassType rule) // throws CertificateValidationException
        {
            try
            {
                var classType = Type.GetType(rule.Value);
                Debug.Assert(classType != null, nameof(classType) + " != null");
                return (ValidatorRule)Activator.CreateInstance(classType);
            }
            catch (Exception e) {
                throw new CertificateValidationException($"Unable to load rule '{rule.Value}'.", e);
            }
        }

        private static ValidatorRule parse(CriticalExtensionRecognizedType rule)
        {
            return new CriticalExtensionRecognizedRule(rule.Values.ToArray());
        }

        private static ValidatorRule parse(CriticalExtensionRequiredType rule)
        {
            return new CriticalExtensionRequiredRule(rule.Values.ToArray());
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

        private static ValidatorRule parse(DummyType dummyType)
        {
            return new DummyRule(dummyType.Value);
        }

        private static ValidatorRule parse(ExpirationType expirationType)
        {
            if (expirationType.Millis == null)
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
            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType), typeof(ClassType) };
            List<ValidatorRule> validatorRules = new List<ValidatorRule>();
            foreach (Object o in optionalType.Rules.OfTypes(typeFilter))
            {
                validatorRules.Add(parse(o, objectStorage));
            }

            return new HandleErrorRule(validatorRules);
        }

        private static ValidatorRule parse(JunctionType junctionType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType), typeof(ClassType) };
            return parse(junctionType.Rules.OfTypes(typeFilter), objectStorage, junctionType.Type);
        }

        private static ValidatorRule parse(KeyUsageType keyUsageType)
        {
            List<Util.KeyUsage> keyUsages = keyUsageType.Identifier;
            Org.BouncyCastle.Asn1.X509.KeyUsage[] result = new Org.BouncyCastle.Asn1.X509.KeyUsage[keyUsages.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Org.BouncyCastle.Asn1.X509.KeyUsage((int) keyUsages[i]);
            }

            return new KeyUsageRule(result);
        }

        private static ValidatorRule parse(OCSPType ocspType, Dictionary<String, Object> objectStorage)
        {
            return new OCSPRule(getBucket(ocspType.IntermediateBucketReference.Value, objectStorage));
        }

        private static ValidatorRule parse(RuleReferenceType ruleReferenceType, Dictionary<String, Object> objectStorage)
            // throws CertificateValidationException
        {
            if (!objectStorage.ContainsKey(ruleReferenceType.Value))
            {
                throw new CertificateValidationException(
                    String.Format("Rule for '{0}' not found.", ruleReferenceType.Value));
            }

            return (ValidatorRule)objectStorage[ruleReferenceType.Value];
        }

        // uppressWarnings("unchecked")
        private static ValidatorRule parse(PrincipleNameType principleNameType, Dictionary<String, Object> objectStorage)
        {
            PrincipalNameProvider<String> principalNameProvider;
            if (principleNameType.Reference != null)
            {
                principalNameProvider =
                    (PrincipalNameProvider<String>)objectStorage[principleNameType.Reference];
            }
            else
            {
                principalNameProvider = new SimplePrincipalNameProvider(principleNameType.Values);
            }

            return new PrincipalNameRule(
                principleNameType.Field,
                principalNameProvider,
                principleNameType.Principal ?? PrincipalEnum.SUBJECT);
        }

        private static ValidatorRule parse(SigningType signingType)
        {
            if (signingType.Type.Equals(SigningEnum.SELF_SIGNED.ToString()))
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
            Type[] typeFilter = new Type[] { typeof(CachedType), typeof(ChainType), typeof(ClassType) };
            foreach (Object rule in tryType.Rules.OfTypes(typeFilter)) {
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
            String identifier = $"#validator::{validatorReferenceType.Value}";
            if (!objectStorage.ContainsKey(identifier))
            {
                throw new CertificateValidationException($"Unable to find validator '{validatorReferenceType.Value}'.");
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