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
 * Using Schema: C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
 **********************************************************************************************/

namespace Mx.Peppol.Lookup.Reader.BdxSmp201605 
{
    public class Enumerations
    {
        // All the enumerations used within the Schema

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}

namespace Mx.Peppol.Lookup.Reader.BdxSmp201605 
{
    internal class Registration
    {
        private static int RegisterLicense()
        {
            LiquidTechnologies.Runtime.Standard20.XmlObjectBase.Register("MassiveX ", "certvalidator.xsd", "0B5LDL72KPW5J7E6000000AA");

            // ##HAND_CODED_BLOCK_START ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
            // Add Additional namespace declarations here...
            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.SchemaType = LiquidTechnologies.Runtime.Standard20.SchemaType.XSD;
//            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.DefaultNamespaceURI = "http://www.fpml.org/2003/FpML-4-0";
//            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("dsig", "http://www.w3.org/2000/09/xmldsig#");

            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("xs", "http://www.w3.org/2001/XMLSchema-instance");
            LiquidTechnologies.Runtime.Standard20.SerializationContext.Default.NamespaceAliases.Add("ds", "http://www.w3.org/2000/09/xmldsig#");

// ##HAND_CODED_BLOCK_END ID="Namespace Declarations"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return 1;
        }
        static public int iRegistrationIndicator = RegisterLicense();
    }
}


