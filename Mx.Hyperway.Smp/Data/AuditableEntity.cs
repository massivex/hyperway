using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
