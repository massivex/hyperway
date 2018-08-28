namespace Mx.Hyperway.Outbound.Lookup
{
    using System;
    using System.Collections.Generic;

    using Autofac.Features.AttributeFilters;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Lookup;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup;
    using Mx.Tools.Cache;

    using zipkin4net;

    public class CachedLookupService : CacheLoader<CachedLookupService.HeaderStub, Endpoint>, ILookupService
    {

        private readonly LookupClient lookupClient;

        private readonly TransportProfile[] transportProfiles;

        public CachedLookupService(
            LookupClient lookupClient,
            [KeyFilter("prioritized")] List<TransportProfile> transportProfiles)
        {
            this.lookupClient = lookupClient;
            this.transportProfiles = transportProfiles.ToArray();
        }

        public Endpoint Lookup(Header header)
        {
            try
            {
                return this[new HeaderStub(header)];
            }
            catch (Exception e)
            {
                throw new HyperwayTransmissionException(e.Message, e);
            }
        }

        public Endpoint Lookup(Header header, Trace root)
        {
            return this.Lookup(header);
        }

        public Endpoint Load(HeaderStub header) // throws Exception
        {
            return this.lookupClient.getEndpoint(
                header.GetReceiver(),
                header.GetDocumentType(),
                header.GetProcess(),
                this.transportProfiles);
        }

        public class HeaderStub
        {

            private readonly ParticipantIdentifier receiver;

            private readonly DocumentTypeIdentifier documentType;

            private readonly ProcessIdentifier process;

            public HeaderStub(Header header)
            {
                this.receiver = header.Receiver;
                this.documentType = header.DocumentType;
                this.process = header.Process;
            }

            public ParticipantIdentifier GetReceiver()
            {
                return this.receiver;
            }

            public DocumentTypeIdentifier GetDocumentType()
            {
                return this.documentType;
            }

            public ProcessIdentifier GetProcess()
            {
                return this.process;
            }

            public override bool Equals(Object o)
            {
                if (this == o) return true;
                if (!(o is HeaderStub)) return false;

                HeaderStub that = (HeaderStub)o;

                if (!this.receiver.Equals(that.receiver)) return false;
                if (!this.documentType.Equals(that.documentType)) return false;
                return this.process.Equals(that.process);

            }

            public override int GetHashCode()
            {
                int result = this.receiver.GetHashCode();
                result = 31 * result + this.documentType.GetHashCode();
                result = 31 * result + this.process.GetHashCode();
                return result;
            }
        }
    }

}
