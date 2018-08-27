namespace Mx.Hyperway.Commons
{
    using System;
    using System.IO;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Sbdh.Lang;

    /// <summary>
    /// An implementation of SBDH parser, which is optimized for speed on large files.
    /// <p>
    /// It will first use a SAX parser to extract the <code>StandardBusinessDocumentHeader</code> only and
    /// create a W3C DOM object.</p>
    /// <p>
    /// The W3C Document is then fed into JAXB, which saves us all the hassle of using Xpath to extract the data.</p>
    /// <p>
    /// This class is not thread safe.</p>
    /// </summary>
    public class SbdhParser
    {
        /// <summary>
        /// Simple wrapper around peppol-sbdh module. 
        /// </summary>
        /// <param name="inputStream">the inputstream containing the XML</param>
        /// <returns>an instance of Header if found, otherwise null.</returns>
        public static Header Parse(Stream inputStream)
        {
            try
            {
                using (SbdReader sbdReader = SbdReader.newInstance(inputStream))
                {
                    return sbdReader.getHeader();
                }
            }
            catch (Exception ex) when (ex is SbdhException || ex is IOException)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            finally
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
