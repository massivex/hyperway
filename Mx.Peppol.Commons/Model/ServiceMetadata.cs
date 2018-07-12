using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    public class ServiceMetadata : AbstractServiceMetadata<Endpoint>
    {

        private static readonly long serialVersionUID = -7523336374349545534L;

        public static ServiceMetadata of(
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
