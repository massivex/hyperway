using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Util
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using log4net;

    using Microsoft.Extensions.FileSystemGlobbing;

    public class XmlUtils
    {

        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(XmlUtils));

        private static readonly Regex ROOT_TAG_PATTERN = new Regex("<(\\w*:{0,1}[^<?|^<!]*)>", RegexOptions.Multiline);

        private static readonly Regex NAMESPACE_PATTERN = new Regex("xmlns:{0,1}([A-Za-z0-9]*)\\w*=\\w*\"(.+?)\"", RegexOptions.Multiline);

        public static String extractRootNamespace(String xmlContent)
        {
            Match matcher = ROOT_TAG_PATTERN.Match(xmlContent);
            if (matcher.Success)
            {
                String rootElement = matcher.Groups[1].Value.Trim();
                LOGGER.DebugFormat("Root element: {0}", rootElement);
                String rootNs = rootElement.Split(new [] { " " }, 2, StringSplitOptions.None)[0].Contains(":") ?
                                    rootElement.Substring(0, rootElement.IndexOf(":", StringComparison.Ordinal)) : "";
                LOGGER.DebugFormat("Namespace: {0}", rootNs);

                Match nsMatcher = NAMESPACE_PATTERN.Match(rootElement);
                while (nsMatcher.Success)
                {
                    LOGGER.Debug(nsMatcher.Groups[0].Value);

                    if (nsMatcher.Groups[1].Value.Equals(rootNs))
                    {
                        return nsMatcher.Groups[2].Value;
                    }
                }
            }

            return null;
        }

        XmlUtils()
        {

        }
    }

}
