﻿using System;

namespace Mx.Oxalis.DocumentSniffer.Parsers
{
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Peppol.Common.Model;

    /**
     * Parser to retrieves information from PEPPOL Catalogue scenarios.
     * Should be able to decode Catalogue (for catalogue response see ApplicationResponse)
     *
     * @author thore
     */
    public class CatalogueDocumentParser : AbstractDocumentParser
    {

    public CatalogueDocumentParser(PlainUBLParser parser) : base(parser)
    {
    }

    public override ParticipantIdentifier getSender()
    {
        String catalogue = "//cac:ProviderParty/cbc:EndpointID";
        return participantId(catalogue);
    }

    public override ParticipantIdentifier getReceiver()
    {
        String catalogue = "//cac:ReceiverParty/cbc:EndpointID";
        return this.participantId(catalogue);
    }
    }
}