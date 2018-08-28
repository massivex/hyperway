using System.Collections.Generic;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Lang;

    public abstract class AbstractServiceMetadata<T> where T: ISimpleEndpoint
    {
        private readonly List<ProcessMetadata<T>> processes;

        protected AbstractServiceMetadata(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            List<ProcessMetadata<T>> processes)
        {
            this.ParticipantIdentifier = participantIdentifier;
            this.DocumentTypeIdentifier = documentTypeIdentifier;
            this.processes = processes;
        }

        public ParticipantIdentifier ParticipantIdentifier { get; }

        public DocumentTypeIdentifier DocumentTypeIdentifier { get; }

        public T GetEndpoint(ProcessIdentifier processIdentifier, params TransportProfile[] transportProfiles)
        {
            foreach (ProcessMetadata<T> processMetadata in this.processes)
            {
                if (processMetadata.ProcessIdentifier.Contains(processIdentifier))
                {
                    return processMetadata.GetEndpoint(transportProfiles);
                }
            }

            throw new EndpointNotFoundException(
                $"Combination of '{processIdentifier}' and transport profile(s) not found.");
        }
    }
}
