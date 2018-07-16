using System;
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
    /// <summary>
    /// This is the base class for all the generated object collection classes. 
    /// </summary>
    /// <remarks>
    /// You can extend this class to provide common functionality to all
    /// generated collection classes. Be sure to place all you additions inside the
    /// hand coded blocks (or they will be lost when you re-generate the code.
    /// </remarks>
    public partial class XmlObjectCollection<T> : LiquidTechnologies.Runtime.Standard20.XmlCollection<T> where T : LiquidTechnologies.Runtime.Standard20.XmlObjectBase, new()
    {
        public XmlObjectCollection(string elementName, string targetNamespace, int minOccurs, int maxOccurs, bool isPseudoElement)
            : base(elementName, targetNamespace, minOccurs, maxOccurs, isPseudoElement)
        {
// ##HAND_CODED_BLOCK_START ID="XmlObjectCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Constructor Code here...

// ##HAND_CODED_BLOCK_END ID="XmlObjectCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }

        /*
         * You can use the following hand coded section to override the XmlObjectBase FromXml... and ToXml...
         * virtual methods to provide custom pre or post processing for all your generated classes,
         * or to add your own common methods.
         *
         * For Example you can add the following here:
         *
         *    public override String ToXml()
         *    {
         *        // Perform Custom Pre Processing
         *
         *        string xml = base.ToXml();
         *
         *        // Perform Custom Post Processing
         *
         *        return xml;
         *    }
         *
         */

// ##HAND_CODED_BLOCK_START ID="XmlObjectCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Custom Base Methods here...

// ##HAND_CODED_BLOCK_END ID="XmlObjectCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

    }

    /// <summary>
    /// This is the base class for all the generated abstract object collection classes. 
    /// </summary>
    /// <remarks>
    /// You can extend this class to provide common functionality to all
    /// generated collection classes. Be sure to place all you additions inside the
    /// hand coded blocks (or they will be lost when you re-generate the code.
    /// </remarks>
    public partial class XmlAbstractObjectCollection<T> : LiquidTechnologies.Runtime.Standard20.XmlAbstractCollection<T> where T : LiquidTechnologies.Runtime.Standard20.XmlObjectInterface
    {
        public XmlAbstractObjectCollection(string elementName, string targetNamespace, int minOccurs, int maxOccurs, System.Type classFactoryType, string createObjectMethod)
            : base(elementName, targetNamespace, minOccurs, maxOccurs, classFactoryType, createObjectMethod)
        {
// ##HAND_CODED_BLOCK_START ID="XmlAbstractObjectCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Constructor Code here...

// ##HAND_CODED_BLOCK_END ID="XmlAbstractObjectCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }

        /*
         * You can use the following hand coded section to override the XmlObjectBase FromXml... and ToXml...
         * virtual methods to provide custom pre or post processing for all your generated classes,
         * or to add your own common methods.
         *
         * For Example you can add the following here:
         *
         *    public override String ToXml()
         *    {
         *        // Perform Custom Pre Processing
         *
         *        string xml = base.ToXml();
         *
         *        // Perform Custom Post Processing
         *
         *        return xml;
         *    }
         *
         */

// ##HAND_CODED_BLOCK_START ID="XmlAbstractObjectCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Custom Base Methods here...

// ##HAND_CODED_BLOCK_END ID="XmlAbstractObjectCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

    }

    /// <summary>
    /// This is the base class for all the generated enum type collection classes. 
    /// </summary>
    /// <remarks>
    /// You can extend this class to provide common functionality to all
    /// generated collection classes. Be sure to place all you additions inside the
    /// hand coded blocks (or they will be lost when you re-generate the code.
    /// </remarks>
    public partial class XmlEnumTypeCollection<T> : LiquidTechnologies.Runtime.Standard20.XmlEnumCollection<T>
    {
        public XmlEnumTypeCollection(string elementName, string targetNamespace, int minOccurs, int maxOccurs, System.Type enumClass, string fromStringMethod, string toStringMethod)
            : base(elementName, targetNamespace, minOccurs, maxOccurs, enumClass, fromStringMethod, toStringMethod)
        {
// ##HAND_CODED_BLOCK_START ID="XmlEnumTypeCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Constructor Code here...

// ##HAND_CODED_BLOCK_END ID="XmlEnumTypeCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }

        /*
         * You can use the following hand coded section to override the XmlObjectBase FromXml... and ToXml...
         * virtual methods to provide custom pre or post processing for all your generated classes,
         * or to add your own common methods.
         *
         * For Example you can add the following here:
         *
         *    public override String ToXml()
         *    {
         *        // Perform Custom Pre Processing
         *
         *        string xml = base.ToXml();
         *
         *        // Perform Custom Post Processing
         *
         *        return xml;
         *    }
         *
         */

// ##HAND_CODED_BLOCK_START ID="XmlEnumTypeCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Custom Base Methods here...

// ##HAND_CODED_BLOCK_END ID="XmlEnumTypeCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

    }

    /// <summary>
    /// This is the base class for all the generated simple type collection classes. 
    /// </summary>
    /// <remarks>
    /// You can extend this class to provide common functionality to all
    /// generated collection classes. Be sure to place all you additions inside the
    /// hand coded blocks (or they will be lost when you re-generate the code.
    /// </remarks>
    public partial class XmlSimpleTypeCollection<T> : LiquidTechnologies.Runtime.Standard20.XmlTypeCollection<T>
    {
        public XmlSimpleTypeCollection(string elementName, string targetNamespace, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType dataType, int minOccurs, int maxOccurs, string formatOverride, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule wsRule, LiquidTechnologies.Runtime.Standard20.PrimitiveRestrictions primValues)
            : base(elementName, targetNamespace, dataType, minOccurs, maxOccurs, formatOverride, wsRule, primValues)
        {
// ##HAND_CODED_BLOCK_START ID="XmlSimpleTypeCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Constructor Code here...

// ##HAND_CODED_BLOCK_END ID="XmlSimpleTypeCollection Constructor"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }

        /*
         * You can use the following hand coded section to override the XmlObjectBase FromXml... and ToXml...
         * virtual methods to provide custom pre or post processing for all your generated classes,
         * or to add your own common methods.
         *
         * For Example you can add the following here:
         *
         *    public override String ToXml()
         *    {
         *        // Perform Custom Pre Processing
         *
         *        string xml = base.ToXml();
         *
         *        // Perform Custom Post Processing
         *
         *        return xml;
         *    }
         *
         */
            
// ##HAND_CODED_BLOCK_START ID="XmlSimpleTypeCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Custom Base Methods here...

// ##HAND_CODED_BLOCK_END ID="XmlSimpleTypeCollection Custom Base Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

    }
}

