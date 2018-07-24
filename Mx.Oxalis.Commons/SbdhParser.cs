namespace Mx.Hyperway.Commons
{
    using System;
    using System.IO;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Sbdh.Lang;

    /**
     * An implementation of SBDH parser, which is optimized for speed on large files.
     * <p>
     * It will first use a SAX parser to extract the <code>StandardBusinessDocumentHeader</code> only and
     * create a W3C DOM object.
     * <p>
     * The W3C Document is then fed into JAXB, which saves us all the hassle of using Xpath to extract the data.
     * <p>
     * This class is not thread safe.
     *
     */
    public class SbdhParser
    {

        /**
         * Simple wrapper around peppol-sbdh module.
         *
         * @param inputStream the inputstream containing the XML
         * @return an instance of Header if found, otherwise null.
         */
        public static Header parse(Stream inputStream)
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
