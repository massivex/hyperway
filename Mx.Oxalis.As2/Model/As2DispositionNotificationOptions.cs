using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Model
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    using log4net;

    using Microsoft.Extensions.FileSystemGlobbing;

    using Mx.Oxalis.As2.Util;
    using Mx.Tools;

    public class As2DispositionNotificationOptions
    {

        private readonly List<Parameter> parameterList;

        public static readonly ILog log = LogManager.GetLogger(typeof(As2DispositionNotificationOptions));

        public As2DispositionNotificationOptions(List<Parameter> parameterList)
        {
            this.parameterList = parameterList;
        }

        public List<Parameter> getParameterList()
        {
            return parameterList;
        }

        public static As2DispositionNotificationOptions getDefault(SMimeDigestMethod digestMethod)
        {
            return valueOf(
                "signed-receipt-protocol=required,pkcs7-signature; signed-receipt-micalg=required,"
                + digestMethod.getIdentifier());
        }

        public static As2DispositionNotificationOptions valueOf(String s)
        {

            if (s == null)
            {
                throw new ArgumentException("Can not parse Multipart empty disposition-notification-options");
            }

            Regex pattern = new Regex(
                "(signed-receipt-protocol|signed-receipt-micalg)\\s*=\\s*(required|optional)\\s*,\\s*([^;]*)",
                RegexOptions.Compiled);

            List<Parameter> parameterList = new List<Parameter>();

            log.Debug("Inspecting " + s);
            MatchCollection matches = pattern.Matches(s);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 3)
                {
                    throw new InvalidOperationException(
                        "Internal error: Invalid group count in RegEx for parameter match in disposition-notification-options.");
                }

                String attributeName = match.Groups[1].Value;
                String importanceName = match.Groups[2].Value;
                String value = match.Groups[3].Value;

                Attribute attribute = Attribute.fromString(attributeName);
                Importance importance = Importance.valueOf(importanceName.Trim().ToUpperInvariant());

                Parameter parameter = new Parameter(attribute, importance, value);
                parameterList.Add(parameter);

            }

            if (parameterList.Count == 0)
            {
                throw new ArgumentException(
                    "Unable to create " + typeof(As2DispositionNotificationOptions).Name + " from '" + s + "'");
            }

            return new As2DispositionNotificationOptions(parameterList);
        }


        Parameter getParameterFor(Attribute attribute)
        {
            foreach (Parameter parameter in parameterList)
            {
                if (parameter.getAttribute() == attribute)
                {
                    return parameter;
                }
            }
            return null;
        }

        /**
         * From the official AS2 spec page 22 :
         * The "signed-receipt-micalg" parameter is a list of MIC algorithms
         * preferred by the requester for use in signing the returned receipt.
         * The list of MIC algorithms SHOULD be honored by the recipient from left to right.
         */
        public Parameter getSignedReceiptMicalg()
        {
            return getParameterFor(Attribute.SIGNED_RECEIPT_MICALG);
        }

        /**
         * @return Use the preferred mic algorithm for signed receipt, for PEPPOL AS2 this should be "sha1"
         */
        public String getPreferredSignedReceiptMicAlgorithmName()
        {
            String preferredAlgorithm = "" + getSignedReceiptMicalg().getTextValue(); // text value could be "sha1, md5"
            return preferredAlgorithm.Split(',')[0].Trim();
        }

        public Parameter getSignedReceiptProtocol()
        {
            return getParameterFor(Attribute.SIGNED_RECEIPT_PROTOCOL);
        }

        // @Override
        public String toString()
        {
            return String.Format("{0}; {1}", getSignedReceiptProtocol(), getSignedReceiptMicalg());
        }

        public class Parameter
        {

            Attribute attribute;

            Importance importance;

            String textValue;

            public Attribute getAttribute()
            {
                return attribute;
            }

            public Importance getImportance()
            {
                return importance;
            }

            public String getTextValue()
            {
                return textValue;
            }

            public Parameter(Attribute attribute, Importance importance, String textValue)
            {
                this.attribute = attribute;
                this.importance = importance;
                this.textValue = textValue;
            }

            // @Override

            public override String ToString()
            {
                StringBuilder sb = new StringBuilder("").Append(attribute).Append("=").Append(importance)
                    .Append(",").Append(textValue);

                return sb.ToString();
            }
        }

        public class Attribute
        {
            public static readonly Attribute SIGNED_RECEIPT_PROTOCOL = new Attribute("signed-receipt-protocol");

            public static readonly Attribute SIGNED_RECEIPT_MICALG = new Attribute("signed-receipt-micalg");

            private readonly string text;

            Attribute(String text)
            {

                this.text = text;
            }


            public static IEnumerable<Attribute> values()
            {
                return new[] { SIGNED_RECEIPT_MICALG, SIGNED_RECEIPT_PROTOCOL };
            }

            /**
             * This is needed as the textual representation of each enum value, contains dashes
             */
            public static Attribute fromString(String s)
            {
                if (s == null)
                {
                    throw new ArgumentException("String value required");
                }

                foreach (Attribute attribute in values())
                {
                    if (attribute.text.EqualsIgnoreCase(s))
                    {
                        return attribute;
                    }
                }

                throw new ArgumentException(s + " not recognized as an attribute of As2DispositionNotificationOptions");
            }

            // @Override
            public override string ToString()
            {
                return text;
            }
        }

        public class Importance
        {
            public static readonly Importance REQUIRED = new Importance("required");

            public static readonly Importance OPTIONAL = new Importance("optional");

            public static Importance valueOf(string name)
            {
                return values().First(x => x.Name == name);
            }

            public static IEnumerable<Importance> values()
            {
                return new[] { REQUIRED, OPTIONAL };
            }

            Importance(string value)
            {
                this.Name = value;
            }

            public string Name { get; }

            public override string ToString()
            {
                return this.Name.ToLowerInvariant();
            }
        }
    }

}
