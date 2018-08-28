using System.Collections.Generic;

namespace Mx.Peppol.Common.Model
{
    public class ServiceMetadata : AbstractServiceMetadata<Endpoint>
    {
        public static ServiceMetadata Of(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            List<ProcessMetadata<Endpoint>> processes)
        {
            return new ServiceMetadata(participantIdentifier, documentTypeIdentifier, processes);
        }

        private ServiceMetadata(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            List<ProcessMetadata<Endpoint>> processes)
            : base(participantIdentifier, documentTypeIdentifier, processes)
        {

        }
    }

}
