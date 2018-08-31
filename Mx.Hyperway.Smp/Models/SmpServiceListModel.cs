using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Models
{
    public class SmpServiceListModel
    {
        public string ParticipantIdentifier { get; set; }
        public List<SmpServiceModel> Services { get; set; }
    }
}
