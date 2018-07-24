namespace Mx.Hyperway.Statistics.Model
{
    using System;

    using Mx.Hyperway.Api.Model;
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