namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// A small set of common information we should be able to retrieve from any PEPPOL UBL/EHF document. 
    /// </summary>
    public interface IPeppolDocumentParser
    {
        /// <summary>
        /// Identify and return the PEPPOL participant sending the document. 
        /// </summary>
        /// <returns></returns>
        ParticipantIdentifier Sender { get; }

        /// <summary>
        /// Identify and return the PEPPOL participant receiving the document. 
        /// </summary>
        /// <returns></returns>
        ParticipantIdentifier Receiver { get; }
    }
}
