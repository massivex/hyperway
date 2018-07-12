using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Lang;

    public abstract class AbstractServiceMetadata<T> where T: SimpleEndpoint
    {

        private static readonly long serialVersionUID = -7523336374349545534L;

        private readonly ParticipantIdentifier participantIdentifier;

        private readonly DocumentTypeIdentifier documentTypeIdentifier;

        private readonly List<ProcessMetadata<T>> processes;

        protected AbstractServiceMetadata(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            List<ProcessMetadata<T>> processes)
        {
            this.participantIdentifier = participantIdentifier;
            this.documentTypeIdentifier = documentTypeIdentifier;
            this.processes = processes;
        }

        public ParticipantIdentifier getParticipantIdentifier()
        {
            return participantIdentifier;
        }

        public DocumentTypeIdentifier getDocumentTypeIdentifier()
        {
            return documentTypeIdentifier;
        }

        public IList<ProcessMetadata<T>> getProcesses()
        {
            return this.processes.AsReadOnly();
        }

        public T getEndpoint(
            ProcessIdentifier processIdentifier,
            params TransportProfile[] transportProfiles) // throws EndpointNotFoundException
        {
            foreach (ProcessMetadata<T> processMetadata in this.processes)
            {
                if (processMetadata.getProcessIdentifier().Contains(processIdentifier))
                {
                    return processMetadata.getEndpoint(transportProfiles);
                }
            }

            throw new EndpointNotFoundException(
                $"Combination of '{processIdentifier}' and transport profile(s) not found.");
        }
    }
}
