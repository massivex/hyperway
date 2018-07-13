namespace Mx.Oxalis.Statistics.Model
{
    using System;

    using Mx.Oxalis.Api.Model;
    using Mx.Peppol.Common.Model;

    internal interface IAbstractBuilder
    {
        AccessPointIdentifier GetAccessPointIdentifier();

        DateTime GetDate();

        Direction GetDirection();

        DocumentTypeIdentifier GetPeppolDocumentTypeId();

        ProcessIdentifier GetPeppolProcessTypeId();

        ChannelId GetChannelId();
    }
}