using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    public class SmpService : AuditableEntity
    {
        public int Id { get; set; }

        public int PeppolParticipantId { get; set; }
        public PeppolParticipant PeppolParticipant { get; set; }

        public int PeppolDocumentId { get; set; }
        public PeppolDocument PeppolDocument { get; set; }

        public int PeppolProcessId { get; set; }
        public PeppolProcess PeppolProcess { get; set; }

        public IList<SmpServiceEndpoint> Endpoints { get; set; }
    }
}
