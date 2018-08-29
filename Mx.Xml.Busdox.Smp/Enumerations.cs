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
 * Generation  :  by Liquid XML Data Binder 16.1.11.8608
 * Using Schema: C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
 **********************************************************************************************/

namespace Mx.Xml.Busdox.Smp 
{
    public class Enumerations
    {
        // All the enumerations used within the Schema

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}
namespace Mx.Xml.Busdox.wsa 
{
    public class Enumerations
    {
        // All the enumerations used within the Schema

        #region Enumeration 'RelationshipType'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum RelationshipType
        {
            HttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingSlashreply
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a RelationshipType enumeration
        /// </summary>
        public static String RelationshipTypeToString(Mx.Xml.Busdox.wsa.Enumerations.RelationshipType enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.Busdox.wsa.Enumerations.RelationshipType.HttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingSlashreply:
                return "http://www.w3.org/2005/08/addressing/reply";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.Busdox.wsa.Enumerations.RelationshipType [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a RelationshipType enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.Busdox.wsa.Enumerations.RelationshipType RelationshipTypeFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "http://www.w3.org/2005/08/addressing/reply":
                return Mx.Xml.Busdox.wsa.Enumerations.RelationshipType.HttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingSlashreply;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.Busdox.wsa.Enumerations.RelationshipType"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.Busdox.wsa.Enumerations.RelationshipType [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.Busdox.wsa.Enumerations.RelationshipType"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection RelationshipTypeNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(RelationshipType);
            foreach (RelationshipType e in Enum.GetValues(t))
                ret.Add(RelationshipTypeToString(e));
            return ret;
        }
        
        #endregion
        #endregion

        #region Enumeration 'FaultCodesType'
        #region Enumeration Declaration
        /// <summary>
        /// </summary>
        public enum FaultCodesType
        {
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh08SlashaddressingRBraceInvalidAddressingHeader,
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceInvalidAddress,
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingRBraceInvalidEPR,
            LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceInvalidCardinality,
            LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceMissingAddressInEPR,
            LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceDuplicateMessageID,
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceActionMismatch,
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporhaddressingRBraceMessageAddressingHeaderRequired,
            LBracehttpColonSlashSlashwwwFullStopw3FullStoporash08SlashaddressingRBraceDestinationUnreachable,
            LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceActionNotSupported,
            LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceEndpointUnavailable
        }
        #endregion

        #region Conversion functions
        /// <summary>
        /// Converts a string to a FaultCodesType enumeration
        /// </summary>
        public static String FaultCodesTypeToString(Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType enumValue)
        {
            switch(enumValue)
            {
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh08SlashaddressingRBraceInvalidAddressingHeader:
                return "{http://www.w3.org/2005/08/addressing}InvalidAddressingHeader";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceInvalidAddress:
                return "{http://www.w3.org/2005/08/addressing}InvalidAddress";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingRBraceInvalidEPR:
                return "{http://www.w3.org/2005/08/addressing}InvalidEPR";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceInvalidCardinality:
                return "{http://www.w3.org/2005/08/addressing}InvalidCardinality";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceMissingAddressInEPR:
                return "{http://www.w3.org/2005/08/addressing}MissingAddressInEPR";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceDuplicateMessageID:
                return "{http://www.w3.org/2005/08/addressing}DuplicateMessageID";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceActionMismatch:
                return "{http://www.w3.org/2005/08/addressing}ActionMismatch";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporhaddressingRBraceMessageAddressingHeaderRequired:
                return "{http://www.w3.org/2005/08/addressing}MessageAddressingHeaderRequired";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporash08SlashaddressingRBraceDestinationUnreachable:
                return "{http://www.w3.org/2005/08/addressing}DestinationUnreachable";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceActionNotSupported:
                return "{http://www.w3.org/2005/08/addressing}ActionNotSupported";
            case Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceEndpointUnavailable:
                return "{http://www.w3.org/2005/08/addressing}EndpointUnavailable";
            default:
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType [" + enumValue.ToString() + "]");
            }
        }

        /// <summary>
        /// Converts a FaultCodesType enumeration to a string (suitable for the XML document)
        /// </summary>
        public static Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType FaultCodesTypeFromString(String enumValue)
        {
            switch(LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(enumValue))
            {
            case "{http://www.w3.org/2005/08/addressing}InvalidAddressingHeader":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh08SlashaddressingRBraceInvalidAddressingHeader;
            case "{http://www.w3.org/2005/08/addressing}InvalidAddress":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceInvalidAddress;
            case "{http://www.w3.org/2005/08/addressing}InvalidEPR":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporgSlash2005Slash08SlashaddressingRBraceInvalidEPR;
            case "{http://www.w3.org/2005/08/addressing}InvalidCardinality":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceInvalidCardinality;
            case "{http://www.w3.org/2005/08/addressing}MissingAddressInEPR":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceMissingAddressInEPR;
            case "{http://www.w3.org/2005/08/addressing}DuplicateMessageID":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceDuplicateMessageID;
            case "{http://www.w3.org/2005/08/addressing}ActionMismatch":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporsh2005Slash08SlashaddressingRBraceActionMismatch;
            case "{http://www.w3.org/2005/08/addressing}MessageAddressingHeaderRequired":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporhaddressingRBraceMessageAddressingHeaderRequired;
            case "{http://www.w3.org/2005/08/addressing}DestinationUnreachable":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStoporash08SlashaddressingRBraceDestinationUnreachable;
            case "{http://www.w3.org/2005/08/addressing}ActionNotSupported":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor05Slash08SlashaddressingRBraceActionNotSupported;
            case "{http://www.w3.org/2005/08/addressing}EndpointUnavailable":
                return Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType.LBracehttpColonSlashSlashwwwFullStopw3FullStopor5Slash08SlashaddressingRBraceEndpointUnavailable;
            default:
                // ##HAND_CODED_BLOCK_START ID="Default Enum Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
                throw new LiquidTechnologies.Runtime.Standard20.LtInvalidValueException("Unknown enumeration value for Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType [" + enumValue + "]");
                // ##HAND_CODED_BLOCK_END ID="Default Enum Mx.Xml.Busdox.wsa.Enumerations.FaultCodesType"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            }
        }

        public static StringCollection FaultCodesTypeNames()
        {
            StringCollection ret = new StringCollection();
            System.Type t = typeof(FaultCodesType);
            foreach (FaultCodesType e in Enum.GetValues(t))
                ret.Add(FaultCodesTypeToString(e));
            return ret;
        }
        
        #endregion
        #endregion

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}

namespace Mx.Xml.Busdox.Smp 
{
    internal class Registration
    {
        private static int RegisterLicense()
        {
            LiquidTechnologies.Runtime.Standard20.XmlObjectBase.Register("MassiveX ", "ServiceMetadataPublishingTypes-1.0.xsd", "9FB9HP7GU9TDE6RA000000AA");

// ##HAND_CODED_BLOCK_START ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
// Add Additional namespace declarations here...
            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.SchemaType = LiquidTechnologies.Runtime.Standard20.SchemaType.XSD;
            //            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.DefaultNamespaceURI = "http://www.fpml.org/2003/FpML-4-0";
            //            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("dsig", "http://www.w3.org/2000/09/xmldsig#");

            var aliases = LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases;
            if (aliases.ContainsKey("ns3"))
            {
                aliases.Add("ns3", "http://busdox.org/serviceMetadata/publishing/1.0/");
            }
            if (aliases.ContainsKey("xs"))
            {
                aliases.Add("xs", "http://www.w3.org/2001/XMLSchema-instance");
            }
            if (aliases.ContainsKey("ids"))
            {
                aliases.Add("ids", "http://busdox.org/transport/identifiers/1.0/");
            }
            if (aliases.ContainsKey("ds"))
            {
                aliases.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            }
            if (aliases.ContainsKey("wsa"))
            {
                aliases.Add("wsa", "http://www.w3.org/2005/08/addressing");
            }
// ##HAND_CODED_BLOCK_END ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return 1;
        }
        static public int iRegistrationIndicator = RegisterLicense();
    }
}


