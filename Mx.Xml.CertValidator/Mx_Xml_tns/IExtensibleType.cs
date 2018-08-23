using System;
using System.Xml;

/**********************************************************************************************
 * Copyright (c) 2001-2018 Liquid Technologies Limited. All rights reserved.
 * See www.liquid-technologies.com for product details.
 *
 * Please see products End User License Agreement for distribution permissions.
 *
 * WARNING: THIS FILE IS GENERATED
 * Changes made outside of ##HAND_CODED_BLOCK_START blocks will be overwritten
 *
 * Generation  :  by Liquid XML Data Binder 16.1.9.8572
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.tns
{
    /// <summary>
    /// </summary>
    public interface IExtensibleType : LiquidTechnologies.Runtime.Standard20.XmlObjectInterface
    {
        #region Member variables
        #region Attribute - ExtensibleTypeData
        /// <summary>
        /// Holds all the information contained within the element
        /// </summary>
        Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type> ExtensibleTypeData {
            get;
        }
        #endregion
    
        #endregion

// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}

