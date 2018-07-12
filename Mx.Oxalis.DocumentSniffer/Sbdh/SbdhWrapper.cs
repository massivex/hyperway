using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Sbdh
{
    using System.IO;

    using Mx.Oxalis.Commons.Interop;
    using Mx.Peppol.Common.Interop;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Xml;

    /**
     * Takes a document and wraps it together with headers into a StandardBusinessDocument.
     * <p>
     * The SBDH part of the document is constructed from the headers.
     * The document will be the payload (xs:any) following the SBDH.
     *
     * @author thore
     * @author steinar
     * @author erlend
     */
    public class SbdhWrapper
    {

        /**
         * Wraps payload + headers into a StandardBusinessDocument
         *
         * @param inputStream the input stream to be wrapped
         * @param headers     the headers to use for sbdh
         * @return byte buffer with the resulting output in utf-8
         */
        public byte[] wrap(Stream inputStream, Header headers)
        {
            MemoryStream m = new MemoryStream();

            try
            {
                using (SbdWriter sbdWriter = SbdWriter.newInstance(m, headers))
                {
                    sbdWriter.AddFragment(inputStream);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to wrap document inside SBD (SBDH). " + ex.Message, ex);
            }

            return m.ToBuffer();
        }
    }
}
