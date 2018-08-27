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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TransmissionRequestBuilder));

        private readonly IContentDetector contentDetector;

        private readonly ILookupService lookupService;

        private bool allowOverride;

        /// <summary>
        /// Will contain the payload PEPPOL document
        /// </summary>
        private byte[] payload;

        /// <summary>
        /// The address of the endpoint either supplied by the caller or looked up in the SMP
        /// </summary>
        private Endpoint endpoint;

        /// <summary>
        /// The header fields supplied by the caller as opposed to the header fields parsed from the payload
        /// </summary>
        private PeppolStandardBusinessHeader suppliedHeaderFields = new PeppolStandardBusinessHeader();

        /// <summary>
        /// The header fields in effect, i.e. merge the parsed header fields with the supplied ones,
        /// giving precedence to the supplied ones.
        /// </summary>
        private PeppolStandardBusinessHeader effectiveStandardBusinessHeader;

        public TransmissionRequestBuilder(IContentDetector contentDetector, ILookupService lookupService)
        {
            this.contentDetector = contentDetector;
            this.lookupService = lookupService;
        }

        public void Reset()
        {
            this.suppliedHeaderFields = new PeppolStandardBusinessHeader();
            this.effectiveStandardBusinessHeader = null;
        }

        /// <summary>
        /// Supplies the  builder with the contents of the message to be sent.
        /// </summary>
        public TransmissionRequestBuilder PayLoad(Stream inputStream)
        {
            this.SavePayLoad(inputStream);
            return this;
        }

        /// <summary>
        /// Overrides the endpoint URL and the AS2 System identifier for the AS2 protocol.
        /// You had better know what you are doing :-)
        /// </summary>

        public TransmissionRequestBuilder OverrideAs2Endpoint(Endpoint newEndpoint)
        {
            this.endpoint = newEndpoint;
            return this;
        }

        public TransmissionRequestBuilder Receiver(ParticipantIdentifier receiverId)
        {
            this.suppliedHeaderFields.RecipientId = receiverId;
            return this;
        }

        public TransmissionRequestBuilder Sender(ParticipantIdentifier senderId)
        {
            this.suppliedHeaderFields.SenderId = senderId;
            return this;
        }

        public TransmissionRequestBuilder DocumentType(DocumentTypeIdentifier documentTypeIdentifier)
        {
            this.suppliedHeaderFields.DocumentTypeIdentifier = documentTypeIdentifier;
            return this;
        }

        public TransmissionRequestBuilder ProcessType(ProcessIdentifier processTypeId)
        {
            this.suppliedHeaderFields.ProfileTypeIdentifier = processTypeId;
            return this;
        }

        public TransmissionRequestBuilder InstanceId(InstanceId instanceId)
        {
            this.suppliedHeaderFields.InstanceId = instanceId;
            return this;
        }

        public ITransmissionRequest Build(Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("build"));
            span.Record(Annotations.ClientSend());
            try
            {
                return this.Build();
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        /// <summary>
        /// Builds the actual <see cref="ITransmissionRequest"/>.
        ///
        /// The <see cref="PeppolStandardBusinessHeader"/> is build as following
        ///
        /// <ol>
        /// <li>If the payload contains an SBHD, allow override if global "overrideAllowed" flag is set, otherwise use the one parsed</li>
        /// <li>If the payload does not contain an SBDH, parse payload to determine some of the SBDH attributes and allow override if global "overrideAllowed" flag is set.</li>
        /// </ol>
        /// </summary>
        /// <returns>Prepared transmission request</returns>
        public ITransmissionRequest Build()
        {
            if (this.payload.Length < 2)
                throw new HyperwayTransmissionException("You have forgotten to provide payload");

            PeppolStandardBusinessHeader optionalParsedSbdh = null;
            try
            {
                optionalParsedSbdh = new PeppolStandardBusinessHeader(SbdhParser.Parse(this.payload.ToStream()));
            }
            catch (InvalidOperationException)
            {
                // No action.
            }

            // Calculates the effectiveStandardBusinessHeader to be used
            this.effectiveStandardBusinessHeader = this.MakeEffectiveSbdh(optionalParsedSbdh, this.suppliedHeaderFields);

            // If the endpoint has not been overridden by the caller, look up the endpoint address in
            // the SMP using the data supplied in the payload
            if (this.IsEndpointSuppliedByCaller() && this.IsOverrideAllowed())
            {
                Log.Warn("Endpoint was set by caller not retrieved from SMP, make sure this is intended behaviour.");
            }
            else
            {
                Endpoint endpointLookedup = this.lookupService.Lookup(this.effectiveStandardBusinessHeader.ToVefa(), null);

                if (this.IsEndpointSuppliedByCaller() && !this.endpoint.Equals(endpointLookedup))
                {
                    throw new InvalidOperationException("You are not allowed to override the EndpointAddress from SMP.");
                }

                this.endpoint = endpointLookedup;
            }

            // make sure payload is encapsulated in SBDH
            if (optionalParsedSbdh == null)
            {
                // Wraps the payload with an SBDH, as this is required for AS2
                this.payload = this.WrapPayLoadWithSbdh(this.payload.ToStream(), this.effectiveStandardBusinessHeader);
            }

            // Transfers all the properties of this object into the newly created TransmissionRequest
            return new DefaultTransmissionRequest(
                this.GetEffectiveStandardBusinessHeader().ToVefa(),
                this.GetPayload(),
                this.GetEndpoint());
        }

        /// <summary>
        /// Merges the SBDH parsed from the payload with the SBDH data supplied by the caller, i.e. the caller wishes to
        /// override the contents of the SBDH parsed. That is, if the payload contains an SBDH
        /// </summary>
        /// <param name="optionalParsedSbdh">the SBDH as parsed (extracted) from the payload.</param>
        /// <param name="peppolSbdhSuppliedByCaller">the SBDH data supplied by the caller in order to override data from the payload</param>
        /// <returns>the merged, effective SBDH created by combining the two data sets</returns>
        private PeppolStandardBusinessHeader MakeEffectiveSbdh(
            PeppolStandardBusinessHeader optionalParsedSbdh,
            PeppolStandardBusinessHeader peppolSbdhSuppliedByCaller)

        {
            PeppolStandardBusinessHeader effectiveSbdh;

            if (this.IsOverrideAllowed())
            {
                if (peppolSbdhSuppliedByCaller.IsComplete())
                {
                    // we have sufficient meta data (set explicitly by the caller using API functions)
                    effectiveSbdh = peppolSbdhSuppliedByCaller;
                }
                else
                {
                    // missing meta data, parse payload, which does not contain SBHD, in order to deduce missing fields
                    PeppolStandardBusinessHeader parsedPeppolStandardBusinessHeader = this.ParsePayLoadAndDeduceSbdh(optionalParsedSbdh);
                    effectiveSbdh = this.CreateEffectiveHeader(parsedPeppolStandardBusinessHeader, peppolSbdhSuppliedByCaller);
                }
            }
            else
            {
                // override is not allowed, make sure we do not override any restricted headers
                PeppolStandardBusinessHeader parsedPeppolStandardBusinessHeader = this.ParsePayLoadAndDeduceSbdh(optionalParsedSbdh);
                List<String> overriddenHeaders = this.FindRestricedHeadersThatWillBeOverridden(
                    parsedPeppolStandardBusinessHeader,
                    peppolSbdhSuppliedByCaller);
                if (overriddenHeaders.Count == 0)
                {
                    effectiveSbdh = this.CreateEffectiveHeader(
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

            if (!effectiveSbdh.IsComplete())
            {
                var txtValues = effectiveSbdh.ListMissingProperties().ToArray().ToStringValues();
                throw new InvalidOperationException(
                    $"TransmissionRequest can not be built, missing {txtValues} metadata.");
            }

            return effectiveSbdh;
        }


        private PeppolStandardBusinessHeader ParsePayLoadAndDeduceSbdh(
            PeppolStandardBusinessHeader optionallyParsedSbdh)
        {
            if (optionallyParsedSbdh != null)
            {
                return optionallyParsedSbdh;
            }

            return new PeppolStandardBusinessHeader(this.contentDetector.Parse(this.payload.ToStream()));
        }

        /// <summary>
        /// Merges the supplied header fields with the SBDH parsed or derived from the payload thus allowing the caller
        /// to explicitly override whatever has been supplied in the payload.
        /// </summary>
        /// <param name="parsed">the PeppolStandardBusinessHeader parsed from the payload</param>
        /// <param name="supplied">the header fields supplied by the caller</param>
        /// <returns>the merged and effective headers</returns>
        protected PeppolStandardBusinessHeader CreateEffectiveHeader(PeppolStandardBusinessHeader parsed, PeppolStandardBusinessHeader supplied)
        {

            // Creates a copy of the original business headers
            PeppolStandardBusinessHeader mergedHeaders = new PeppolStandardBusinessHeader(parsed);

            if (supplied.SenderId!= null)
            {
                mergedHeaders.SenderId = supplied.SenderId;
            }

            if (supplied.RecipientId!= null)
            {
                mergedHeaders.RecipientId = supplied.RecipientId;
            }

            if (supplied.DocumentTypeIdentifier != null)
            {
                mergedHeaders.DocumentTypeIdentifier = supplied.DocumentTypeIdentifier;
            }

            if (supplied.ProfileTypeIdentifier != null)
            {
                mergedHeaders.ProfileTypeIdentifier = supplied.ProfileTypeIdentifier;
            }

            // If instanceId was supplied by caller, use it otherwise, create new
            if (supplied.InstanceId!= null)
            {
                mergedHeaders.InstanceId = supplied.InstanceId;
            }
            else
            {
                mergedHeaders.InstanceId = new InstanceId();
            }

            if (supplied.CreationDateAndTime != null)
            {
                mergedHeaders.CreationDateAndTime = supplied.CreationDateAndTime;
            }

            return mergedHeaders;

        }

        /// <summary>
        /// Returns a list of "restricted" header names that will be overridden when calling #createEffectiveHeader
        /// The restricted header names are SenderId, RecipientId, DocumentTypeIdentifier and ProfileTypeIdentifier
        /// Compares values that exist both as parsed and supplied headers.
        /// Ignores values that only exists in one of them (that allows for sending new and unknown document types)
        /// </summary>
        protected List<String> FindRestricedHeadersThatWillBeOverridden(
            PeppolStandardBusinessHeader parsed,
            PeppolStandardBusinessHeader supplied)
        {
            List<String> headers = new List<String>();
            if ((parsed.SenderId!= null) && (supplied.SenderId!= null)
                                               && (!supplied.SenderId.Equals(parsed.SenderId)))
            {
                headers.Add("SenderId");
            }

            if ((parsed.RecipientId!= null) && (supplied.RecipientId!= null)
                                                  && (!supplied.RecipientId.Equals(parsed.RecipientId)))
            {
                headers.Add("RecipientId");
            }

            if ((parsed.DocumentTypeIdentifier != null) && (supplied.DocumentTypeIdentifier != null)
                                                             && (!supplied.DocumentTypeIdentifier.Equals(
                                                                     parsed.DocumentTypeIdentifier)))
            {
                headers.Add("DocumentTypeIdentifier");
            }


            if ((parsed.ProfileTypeIdentifier != null) && (supplied.ProfileTypeIdentifier != null)
                                                            && (!supplied.ProfileTypeIdentifier
                                                                    .Equals(parsed.ProfileTypeIdentifier)))
            {
                headers.Add("ProfileTypeIdentifier");
            }
                
            return headers;
        }

        protected PeppolStandardBusinessHeader GetEffectiveStandardBusinessHeader()
        {
            return this.effectiveStandardBusinessHeader;
        }

        protected void SavePayLoad(Stream inputStream)
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

        protected Stream GetPayload()
        {
            return this.payload.ToStream();
        }

        public Endpoint GetEndpoint()
        {
            return this.endpoint;
        }

        public bool IsOverrideAllowed()
        {
            return this.allowOverride;
        }

        private bool IsEndpointSuppliedByCaller()
        {
            return this.endpoint != null;
        }

        private byte[] WrapPayLoadWithSbdh(
            Stream byteArrayInputStream,
            PeppolStandardBusinessHeader effectivesbh)
        {
            SbdhWrapper sbdhWrapper = new SbdhWrapper();
            return sbdhWrapper.Wrap(byteArrayInputStream, effectivesbh.ToVefa());
        }

        /// <summary>
        /// For testing purposes only
        /// </summary>
        public void SetTransmissionBuilderOverride(bool transmissionBuilderOverride)
        {
            this.allowOverride = transmissionBuilderOverride;
        }
    }
}
