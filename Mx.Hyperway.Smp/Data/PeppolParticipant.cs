using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    public class PeppolParticipant : AuditableEntity
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int SmpHostId { get; set; }
        public SmpHost SmpHost { get; set; }
    }
}
