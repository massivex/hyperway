namespace Mx.Hyperway.As2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using log4net;

    using Mx.Hyperway.As2.Util;
    using Mx.Tools;

    public class As2DispositionNotificationOptions
    {

        private readonly List<Parameter> parameterList;

        public static readonly ILog Log = LogManager.GetLogger(typeof(As2DispositionNotificationOptions));

        public As2DispositionNotificationOptions(List<Parameter> parameterList)
        {
            this.parameterList = parameterList;
        }

        public List<Parameter> GetParameterList()
        {
            return this.parameterList;
        }

        public static As2DispositionNotificationOptions GetDefault(SMimeDigestMethod digestMethod)
        {
            return ValueOf(
                "signed-receipt-protocol=required,pkcs7-signature; signed-receipt-micalg=required,"
                + digestMethod.GetIdentifier());
        }

        public static As2DispositionNotificationOptions ValueOf(string s)
        {

            if (s == null)
            {
                throw new ArgumentException("Can not parse Multipart empty disposition-notification-options");
            }

            Regex pattern = new Regex(
                "(signed-receipt-protocol|signed-receipt-micalg)\\s*=\\s*(required|optional)\\s*,\\s*([^;]*)",
                RegexOptions.Compiled);

            List<Parameter> parameterList = new List<Parameter>();

            Log.Debug("Inspecting " + s);
            MatchCollection matches = pattern.Matches(s);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 4)
                {
                    throw new InvalidOperationException(
                        "Internal error: Invalid group count in RegEx for parameter match in disposition-notification-options.");
                }

                string attributeName = match.Groups[1].Value;
                string importanceName = match.Groups[2].Value;
                string value = match.Groups[3].Value;

                Attribute attribute = Attribute.FromString(attributeName);
                Importance importance = Importance.ValueOf(importanceName.Trim().ToUpperInvariant());

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


        Parameter GetParameterFor(Attribute attribute)
        {
            foreach (Parameter parameter in this.parameterList)
            {
                if (parameter.Attribute == attribute)
                {
                    return parameter;
                }
            }
            return null;
        }

        /// <summary>
        /// From the official AS2 spec page 22 :
        /// The "signed-receipt-micalg" parameter is a list of MIC algorithms
        /// preferred by the requester for use in signing the returned receipt.
        /// The list of MIC algorithms SHOULD be honored by the recipient from left to right.
        /// </summary>
        /// <returns></returns>
        public Parameter GetSignedReceiptMicalg()
        {
            return this.GetParameterFor(Attribute.SignedReceiptMicalg);
        }

        /// <summary>
        /// Preferred MIC algorithm
        /// </summary>
        /// <returns>Use the preferred mic algorithm for signed receipt, for PEPPOL AS2 this should be "sha1"</returns>
        public string GetPreferredSignedReceiptMicAlgorithmName()
        {
            string preferredAlgorithm = "" + this.GetSignedReceiptMicalg().TextValue; // text value could be "sha1, md5"
            return preferredAlgorithm.Split(',')[0].Trim();
        }

        public Parameter GetSignedReceiptProtocol()
        {
            return this.GetParameterFor(Attribute.SignedReceiptProtocol);
        }

        public override string ToString()
        {
            return $"{this.GetSignedReceiptProtocol()}; {this.GetSignedReceiptMicalg()}";
        }

        public class Parameter
        {
            public Attribute Attribute { get; }

            public Importance Importance { get; }

            public string TextValue { get; }

            public Parameter(Attribute attribute, Importance importance, string textValue)
            {
                this.Attribute = attribute;
                this.Importance = importance;
                this.TextValue = textValue;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder("").Append(this.Attribute).Append("=").Append(this.Importance)
                    .Append(",").Append(this.TextValue);

                return sb.ToString();
            }
        }

        public class Attribute
        {
            public static readonly Attribute SignedReceiptProtocol = new Attribute("signed-receipt-protocol");

            public static readonly Attribute SignedReceiptMicalg = new Attribute("signed-receipt-micalg");

            private readonly string text;

            Attribute(string text)
            {

                this.text = text;
            }


            public static IEnumerable<Attribute> Values()
            {
                return new[] { SignedReceiptMicalg, SignedReceiptProtocol };
            }

            /// <summary>
            /// This is needed as the textual representation of each enum value, contains dashes 
            /// </summary>
            public static Attribute FromString(string s)
            {
                if (s == null)
                {
                    throw new ArgumentException("String value required");
                }

                foreach (Attribute attribute in Values())
                {
                    if (attribute.text.EqualsIgnoreCase(s))
                    {
                        return attribute;
                    }
                }

                throw new ArgumentException(s + " not recognized as an attribute of As2DispositionNotificationOptions");
            }

            public override string ToString()
            {
                return this.text;
            }
        }
    }

    public class Importance
    {
        public static readonly Importance Required = new Importance("REQUIRED");

        public static readonly Importance Optional = new Importance("OPTIONAL");

        internal static Importance ValueOf(string name)
        {
            return Values().First(x => x.Name == name);
        }

        public static IEnumerable<Importance> Values()
        {
            return new[] { Required, Optional };
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
