using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Models
{
    using Mx.Hyperway.Smp.Data;

    public class SmpServiceModel
    {
        public int Id { get; set; }

        public string ParticipantIdentifier { get;set; }

        public string DocumentIdentifier{ get; set; }

        public string ProcessIdentifier { get; set; }

        public string ServiceMetadataUrl { get; set; }

        public List<string> Endpoints { get; set; }
    }
}
