using System;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Collections.Specialized;

/**********************************************************************************************
 * Copyright (c) 2001-2018 Liquid Technologies Limited. All rights reserved.
 * See www.liquid-technologies.com for product details.
 *
 * Please see products End User License Agreement for distribution permissions.
 *
 * WARNING: THIS FILE IS GENERATED
 * Changes made outside of ##HAND_CODED_BLOCK_START blocks will be overwritten
 *
 * Generation  :  by Liquid XML Data Binder 16.1.7.8497
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.CertValidator 
{
    public class Enumerations
    {
        // All the enumerations used within the Schema

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}
namespace Mx.Xml.tns 
{
    public class Enumerations
    {
        // All the enumerations used within the Schema

        #region Enumeration 'JunctionEnum'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum JunctionEnum
        {
            AND,
            OR,
            XOR
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a JunctionEnum enumeration
        /// </summary>
        public static String JunctionEnumToString(Mx.Xml.tns.Enumerations.JunctionEnum enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.tns.Enumerations.JunctionEnum.AND:
                return "AND";
            case Mx.Xml.tns.Enumerations.JunctionEnum.OR:
                return "OR";
            case Mx.Xml.tns.Enumerations.JunctionEnum.XOR:
                return "XOR";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.JunctionEnum [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a JunctionEnum enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.tns.Enumerations.JunctionEnum JunctionEnumFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "AND":
                return Mx.Xml.tns.Enumerations.JunctionEnum.AND;
            case "OR":
                return Mx.Xml.tns.Enumerations.JunctionEnum.OR;
            case "XOR":
                return Mx.Xml.tns.Enumerations.JunctionEnum.XOR;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.tns.Enumerations.JunctionEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.JunctionEnum [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.tns.Enumerations.JunctionEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection JunctionEnumNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(JunctionEnum);
            foreach (JunctionEnum e in Enum.GetValues(t))
                ret.Add(JunctionEnumToString(e));
            return ret;
        }
        
        #endregion
        #endregion

        #region Enumeration 'PrincipalEnum'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum PrincipalEnum
        {
            SUBJECT,
            ISSUER
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a PrincipalEnum enumeration
        /// </summary>
        public static String PrincipalEnumToString(Mx.Xml.tns.Enumerations.PrincipalEnum enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.tns.Enumerations.PrincipalEnum.SUBJECT:
                return "SUBJECT";
            case Mx.Xml.tns.Enumerations.PrincipalEnum.ISSUER:
                return "ISSUER";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.PrincipalEnum [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a PrincipalEnum enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.tns.Enumerations.PrincipalEnum PrincipalEnumFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "SUBJECT":
                return Mx.Xml.tns.Enumerations.PrincipalEnum.SUBJECT;
            case "ISSUER":
                return Mx.Xml.tns.Enumerations.PrincipalEnum.ISSUER;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.tns.Enumerations.PrincipalEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.PrincipalEnum [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.tns.Enumerations.PrincipalEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection PrincipalEnumNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(PrincipalEnum);
            foreach (PrincipalEnum e in Enum.GetValues(t))
                ret.Add(PrincipalEnumToString(e));
            return ret;
        }
        
        #endregion
        #endregion

        #region Enumeration 'SigningEnum'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum SigningEnum
        {
            PUBLIC_SIGNED,
            SELF_SIGNED
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a SigningEnum enumeration
        /// </summary>
        public static String SigningEnumToString(Mx.Xml.tns.Enumerations.SigningEnum enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.tns.Enumerations.SigningEnum.PUBLIC_SIGNED:
                return "PUBLIC_SIGNED";
            case Mx.Xml.tns.Enumerations.SigningEnum.SELF_SIGNED:
                return "SELF_SIGNED";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.SigningEnum [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a SigningEnum enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.tns.Enumerations.SigningEnum SigningEnumFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "PUBLIC_SIGNED":
                return Mx.Xml.tns.Enumerations.SigningEnum.PUBLIC_SIGNED;
            case "SELF_SIGNED":
                return Mx.Xml.tns.Enumerations.SigningEnum.SELF_SIGNED;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.tns.Enumerations.SigningEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.SigningEnum [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.tns.Enumerations.SigningEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection SigningEnumNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(SigningEnum);
            foreach (SigningEnum e in Enum.GetValues(t))
                ret.Add(SigningEnumToString(e));
            return ret;
        }
        
        #endregion
        #endregion

        #region Enumeration 'KeyUsageEnum'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum KeyUsageEnum
        {
            DIGITAL_SIGNATURE,
            NON_REPUDIATION,
            KEY_ENCIPHERMENT,
            DATA_ENCIPHERMENT,
            KEY_AGREEMENT,
            KEY_CERT_SIGN,
            CRL_SIGN,
            ENCIPHER_ONLY,
            DECIPHER_ONLY
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a KeyUsageEnum enumeration
        /// </summary>
        public static String KeyUsageEnumToString(Mx.Xml.tns.Enumerations.KeyUsageEnum enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.DIGITAL_SIGNATURE:
                return "DIGITAL_SIGNATURE";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.NON_REPUDIATION:
                return "NON_REPUDIATION";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_ENCIPHERMENT:
                return "KEY_ENCIPHERMENT";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.DATA_ENCIPHERMENT:
                return "DATA_ENCIPHERMENT";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_AGREEMENT:
                return "KEY_AGREEMENT";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_CERT_SIGN:
                return "KEY_CERT_SIGN";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.CRL_SIGN:
                return "CRL_SIGN";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.ENCIPHER_ONLY:
                return "ENCIPHER_ONLY";
            case Mx.Xml.tns.Enumerations.KeyUsageEnum.DECIPHER_ONLY:
                return "DECIPHER_ONLY";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.KeyUsageEnum [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a KeyUsageEnum enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.tns.Enumerations.KeyUsageEnum KeyUsageEnumFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "DIGITAL_SIGNATURE":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.DIGITAL_SIGNATURE;
            case "NON_REPUDIATION":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.NON_REPUDIATION;
            case "KEY_ENCIPHERMENT":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_ENCIPHERMENT;
            case "DATA_ENCIPHERMENT":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.DATA_ENCIPHERMENT;
            case "KEY_AGREEMENT":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_AGREEMENT;
            case "KEY_CERT_SIGN":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.KEY_CERT_SIGN;
            case "CRL_SIGN":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.CRL_SIGN;
            case "ENCIPHER_ONLY":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.ENCIPHER_ONLY;
            case "DECIPHER_ONLY":
                return Mx.Xml.tns.Enumerations.KeyUsageEnum.DECIPHER_ONLY;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.tns.Enumerations.KeyUsageEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.tns.Enumerations.KeyUsageEnum [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.tns.Enumerations.KeyUsageEnum"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection KeyUsageEnumNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(KeyUsageEnum);
            foreach (KeyUsageEnum e in Enum.GetValues(t))
                ret.Add(KeyUsageEnumToString(e));
            return ret;
        }
        
        #endregion
        #endregion

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}

namespace Mx.Xml.CertValidator 
{
    internal class Registration
    {
        private static int RegisterLicense()
        {
            LiquidTechnologies.Runtime.Standard20.XmlObjectBase.Register("Trial 31/07/2018", "certvalidator.xsd", "N321NPKX8HMPTW9A000000AA");

// ##HAND_CODED_BLOCK_START ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
// Add Additional namespace declarations here...
            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.SchemaType = LiquidTechnologies.Runtime.Standard20.SchemaType.XSD;
//            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.DefaultNamespaceURI = "http://www.fpml.org/2003/FpML-4-0";
//            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("dsig", "http://www.w3.org/2000/09/xmldsig#");

            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("xs", "http://www.w3.org/2001/XMLSchema-instance");

// ##HAND_CODED_BLOCK_END ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return 1;
        }
        static public int iRegistrationIndicator = RegisterLicense();
    }
}


