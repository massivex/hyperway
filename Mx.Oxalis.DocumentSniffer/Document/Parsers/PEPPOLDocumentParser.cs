namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using Mx.Peppol.Common.Model;

    /**
     * A small set of common information we should be able to retrieve from any PEPPOL UBL/EHF document.
     *
     * @author thore
     */
    public interface PEPPOLDocumentParser
    {

        /**
         * Identify and return the PEPPOL participant sending the document.
         */
        ParticipantIdentifier getSender();

        /**
         * Identify and return the PEPPOL participant receiving the document.
         */
        ParticipantIdentifier getReceiver();

    }

}
