namespace Mx.Hyperway.Outbound.Transmission
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Lookup;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.Commons;
    using Mx.Hyperway.DocumentSniffer;
    using Mx.Hyperway.DocumentSniffer.Identifier;
    using Mx.Hyperway.DocumentSniffer.Sbdh;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    using zipkin4net;

    public class TransmissionRequestBuilder
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TransmissionRequestBuilder));
        // private static final Logger log = LoggerFactory.getLogger(TransmissionRequestBuilder.class);

        private readonly ContentDetector contentDetector;

        private readonly LookupService lookupService;

        private bool allowOverride;

        /**
         * Will contain the payload PEPPOL document
         */
        private byte[] payload;

        /**
         * The address of the endpoint either supplied by the caller or looked up in the SMP
         */
        private Endpoint endpoint;

        /**
         * The header fields supplied by the caller as opposed to the header fields parsed from the payload
         */
        private PeppolStandardBusinessHeader suppliedHeaderFields = new PeppolStandardBusinessHeader();

        /**
         * The header fields in effect, i.e. merge the parsed header fields with the supplied ones,
         * giving precedence to the supplied ones.
         */
        private PeppolStandardBusinessHeader effectiveStandardBusinessHeader;

        public TransmissionRequestBuilder(ContentDetector contentDetector, LookupService lookupService)
        {
            this.contentDetector = contentDetector;
            this.lookupService = lookupService;
        }

        public void reset()
        {
            this.suppliedHeaderFields = new PeppolStandardBusinessHeader();
            this.effectiveStandardBusinessHeader = null;
        }

        /**
         * Supplies the  builder with the contents of the message to be sent.
         */
        public TransmissionRequestBuilder payLoad(Stream inputStream)
        {
            this.savePayLoad(inputStream);
            return this;
        }

        /**
         * Overrides the endpoint URL and the AS2 System identifier for the AS2 protocol.
         * You had better know what you are doing :-)
         */
        public TransmissionRequestBuilder overrideAs2Endpoint(Endpoint endpoint)
        {
            this.endpoint = endpoint;
            return this;
        }

        public TransmissionRequestBuilder receiver(ParticipantIdentifier receiverId)
        {
            this.suppliedHeaderFields.setRecipientId(receiverId);
            return this;
        }

        public TransmissionRequestBuilder sender(ParticipantIdentifier senderId)
        {
            this.suppliedHeaderFields.setSenderId(senderId);
            return this;
        }

        public TransmissionRequestBuilder documentType(DocumentTypeIdentifier documentTypeIdentifier)
        {
            this.suppliedHeaderFields.setDocumentTypeIdentifier(documentTypeIdentifier);
            return this;
        }

        public TransmissionRequestBuilder processType(ProcessIdentifier processTypeId)
        {
            this.suppliedHeaderFields.setProfileTypeIdentifier(processTypeId);
            return this;
        }

        public TransmissionRequestBuilder instanceId(InstanceId instanceId)
        {
            this.suppliedHeaderFields.setInstanceId(instanceId);
            return this;
        }

        public TransmissionRequest build(Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("build"));
            span.Record(Annotations.ClientSend());
            //Span span = tracer.newChild(root.context()).name("build").start();
            try
            {
                return this.build();
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        /**
         * Builds the actual {@link TransmissionRequest}.
         * <p>
         * The  {@link PeppolStandardBusinessHeader} is built as following:
         *
         * <ol>
         * <li>If the payload contains an SBHD, allow override if global "overrideAllowed" flag is set,
         * otherwise use the one parsed</li>
         * <li>If the payload does not contain an SBDH, parse payload to determine some of the SBDH attributes
         * and allow override if global "overrideAllowed" flag is set.</li>
         * </ol>
         *
         * @return Prepared transmission request.
         */
        public TransmissionRequest build()
        {
            if (this.payload.Length < 2)
                throw new HyperwayTransmissionException("You have forgotten to provide payload");

            PeppolStandardBusinessHeader optionalParsedSbdh = null;
            try
            {
                optionalParsedSbdh = new PeppolStandardBusinessHeader(SbdhParser.parse(this.payload.ToStream()));
            }
            catch (InvalidOperationException e)
            {
                // No action.
            }

            // Calculates the effectiveStandardBusinessHeader to be used
            this.effectiveStandardBusinessHeader = this.makeEffectiveSbdh(optionalParsedSbdh, this.suppliedHeaderFields);

            // If the endpoint has not been overridden by the caller, look up the endpoint address in
            // the SMP using the data supplied in the payload
            if (this.isEndpointSuppliedByCaller() && this.isOverrideAllowed())
            {
                log.Warn("Endpoint was set by caller not retrieved from SMP, make sure this is intended behaviour.");
            }
            else
            {
                Endpoint endpoint = this.lookupService.lookup(this.effectiveStandardBusinessHeader.toVefa(), null);

                if (this.isEndpointSuppliedByCaller() && !this.endpoint.Equals(endpoint))
                {
                    throw new InvalidOperationException("You are not allowed to override the EndpointAddress from SMP.");
                }

                this.endpoint = endpoint;
            }

            // make sure payload is encapsulated in SBDH
            if (optionalParsedSbdh == null)
            {
                // Wraps the payload with an SBDH, as this is required for AS2
                this.payload = this.wrapPayLoadWithSBDH(this.payload.ToStream(), this.effectiveStandardBusinessHeader);
            }

            // Transfers all the properties of this object into the newly created TransmissionRequest
            return new DefaultTransmissionRequest(
                this.getEffectiveStandardBusinessHeader().toVefa(),
                this.getPayload(),
                this.getEndpoint());
        }

        /**
         * Merges the SBDH parsed from the payload with the SBDH data supplied by the caller, i.e. the caller wishes to
         * override the contents of the SBDH parsed. That is, if the payload contains an SBDH
         *
         * @param optionalParsedSbdh         the SBDH as parsed (extracted) from the payload.
         * @param peppolSbdhSuppliedByCaller the SBDH data supplied by the caller in order to override data from the payload
         * @return the merged, effective SBDH created by combining the two data sets
         */
        PeppolStandardBusinessHeader makeEffectiveSbdh(
            PeppolStandardBusinessHeader optionalParsedSbdh,
            PeppolStandardBusinessHeader peppolSbdhSuppliedByCaller)

        {
            PeppolStandardBusinessHeader effectiveSbdh;

            if (this.isOverrideAllowed())
            {
                if (peppolSbdhSuppliedByCaller.isComplete())
                {
                    // we have sufficient meta data (set explicitly by the caller using API functions)
                    effectiveSbdh = peppolSbdhSuppliedByCaller;
                }
                else
                {
                    // missing meta data, parse payload, which does not contain SBHD, in order to deduce missing fields
                    PeppolStandardBusinessHeader parsedPeppolStandardBusinessHeader = this.parsePayLoadAndDeduceSbdh(optionalParsedSbdh);
                    effectiveSbdh = this.createEffectiveHeader(parsedPeppolStandardBusinessHeader, peppolSbdhSuppliedByCaller);
                }
            }
            else
            {
                // override is not allowed, make sure we do not override any restricted headers
                PeppolStandardBusinessHeader parsedPeppolStandardBusinessHeader = this.parsePayLoadAndDeduceSbdh(optionalParsedSbdh);
                List<String> overriddenHeaders = this.findRestricedHeadersThatWillBeOverridden(
                    parsedPeppolStandardBusinessHeader,
                    peppolSbdhSuppliedByCaller);
                if (overriddenHeaders.Count == 0)
                {
                    effectiveSbdh = this.createEffectiveHeader(
                        parsedPeppolStandardBusinessHeader,
                        peppolSbdhSuppliedByCaller);
                }
                else
                {
                    var txtValues = overriddenHeaders.ToArray().ToStringValues();
                    throw new InvalidOperationException(
                        "Your are not allowed to override " + txtValues + " in production mode, makes sure headers match the ones in the document.");
                }
            }

            if (!effectiveSbdh.isComplete())
            {
                var txtValues = effectiveSbdh.listMissingProperties().ToArray().ToStringValues();
                throw new InvalidOperationException(
                    $"TransmissionRequest can not be built, missing {txtValues} metadata.");
            }

            return effectiveSbdh;
        }


        private PeppolStandardBusinessHeader parsePayLoadAndDeduceSbdh(
            PeppolStandardBusinessHeader optionallyParsedSbdh)
        {
            if (optionallyParsedSbdh != null)
            {
                return optionallyParsedSbdh;
            }

            return new PeppolStandardBusinessHeader(this.contentDetector.parse(this.payload.ToStream()));
        }

        /**
         * Merges the supplied header fields with the SBDH parsed or derived from the payload thus allowing the caller
         * to explicitly override whatever has been supplied in the payload.
         *
         * @param parsed   the PeppolStandardBusinessHeader parsed from the payload
         * @param supplied the header fields supplied by the caller
         * @return the merged and effective headers
         */
        protected PeppolStandardBusinessHeader createEffectiveHeader(PeppolStandardBusinessHeader parsed, PeppolStandardBusinessHeader supplied)
        {

            // Creates a copy of the original business headers
            PeppolStandardBusinessHeader mergedHeaders = new PeppolStandardBusinessHeader(parsed);

            if (supplied.getSenderId() != null)
            {
                mergedHeaders.setSenderId(supplied.getSenderId());
            }

            if (supplied.getRecipientId() != null)
            {
                mergedHeaders.setRecipientId(supplied.getRecipientId());
            }

            if (supplied.getDocumentTypeIdentifier() != null)
            {
                mergedHeaders.setDocumentTypeIdentifier(supplied.getDocumentTypeIdentifier());
            }

            if (supplied.getProfileTypeIdentifier() != null)
            {
                mergedHeaders.setProfileTypeIdentifier(supplied.getProfileTypeIdentifier());
            }

            // If instanceId was supplied by caller, use it otherwise, create new
            if (supplied.getInstanceId() != null)
            {
                mergedHeaders.setInstanceId(supplied.getInstanceId());
            }
            else
            {
                mergedHeaders.setInstanceId(new InstanceId());
            }

            if (supplied.getCreationDateAndTime() != null)
            {
                mergedHeaders.setCreationDateAndTime(supplied.getCreationDateAndTime());
            }

            return mergedHeaders;

        }

        /**
         * Returns a list of "restricted" header names that will be overridden when calling #createEffectiveHeader
         * The restricted header names are SenderId, RecipientId, DocumentTypeIdentifier and ProfileTypeIdentifier
         * Compares values that exist both as parsed and supplied headers.
         * Ignores values that only exists in one of them (that allows for sending new and unknown document types)
         */
        protected List<String> findRestricedHeadersThatWillBeOverridden(
            PeppolStandardBusinessHeader parsed,
            PeppolStandardBusinessHeader supplied)
        {
            List<String> headers = new List<String>();
            if ((parsed.getSenderId() != null) && (supplied.getSenderId() != null)
                                               && (!supplied.getSenderId().Equals(parsed.getSenderId())))
            {
                headers.Add("SenderId");
            }

            if ((parsed.getRecipientId() != null) && (supplied.getRecipientId() != null)
                                                  && (!supplied.getRecipientId().Equals(parsed.getRecipientId())))
            {
                headers.Add("RecipientId");
            }

            if ((parsed.getDocumentTypeIdentifier() != null) && (supplied.getDocumentTypeIdentifier() != null)
                                                             && (!supplied.getDocumentTypeIdentifier().Equals(
                                                                     parsed.getDocumentTypeIdentifier())))
            {
                headers.Add("DocumentTypeIdentifier");
            }


            if ((parsed.getProfileTypeIdentifier() != null) && (supplied.getProfileTypeIdentifier() != null)
                                                            && (!supplied.getProfileTypeIdentifier()
                                                                    .Equals(parsed.getProfileTypeIdentifier())))
            {
                headers.Add("ProfileTypeIdentifier");
            }
                
            return headers;
        }

        protected PeppolStandardBusinessHeader getEffectiveStandardBusinessHeader()
        {
            return this.effectiveStandardBusinessHeader;
        }

        protected void savePayLoad(Stream inputStream)
        {
            try
            {
                this.payload = inputStream.ToBuffer();
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("Unable to save the payload: " + e.Message, e);
            }
        }

        protected Stream getPayload()
        {
            return this.payload.ToStream();
        }

        public Endpoint getEndpoint()
        {
            return this.endpoint;
        }

        public bool isOverrideAllowed()
        {
            return this.allowOverride;
        }

        private bool isEndpointSuppliedByCaller()
        {
            return this.endpoint != null;
        }

        private byte[] wrapPayLoadWithSBDH(
            Stream byteArrayInputStream,
            PeppolStandardBusinessHeader effectiveStandardBusinessHeader)
        {
            SbdhWrapper sbdhWrapper = new SbdhWrapper();
            return sbdhWrapper.wrap(byteArrayInputStream, effectiveStandardBusinessHeader.toVefa());
        }

        /**
         * For testing purposes only
         */
        public void setTransmissionBuilderOverride(bool transmissionBuilderOverride)
        {
            this.allowOverride = transmissionBuilderOverride;
        }
    }
}
