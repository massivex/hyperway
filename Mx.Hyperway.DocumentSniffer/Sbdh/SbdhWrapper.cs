namespace Mx.Hyperway.DocumentSniffer.Sbdh
{
    using System;
    using System.IO;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Tools;

    /// <summary>
    /// Takes a document and wraps it together with headers into a StandardBusinessDocument.
    /// The SBDH part of the document is constructed from the headers.
    /// The document will be the payload (xs:any) following the SBDH.
    /// </summary>
    public class SbdhWrapper
    {
        /// <summary>
        /// Wraps payload + headers into a StandardBusinessDocument 
        /// </summary>
        /// <param name="inputStream">the input stream to be wrapped</param>
        /// <param name="headers">the headers to use for sbdh</param>
        /// <returns>byte buffer with the resulting output in utf-8</returns>
        public byte[] Wrap(Stream inputStream, Header headers)
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
