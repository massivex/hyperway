using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    public class SmpServiceEndpoint : AuditableEntity
    {
        public int Id { get; set; }
        public int SmpServiceId { get; set; }
        public SmpService SmpService { get; set; }
        public bool RequireBusinessLevelSignature { get; set; }
        public string Endpoint { get; set; }
        public DateTime ServiceActivationDate { get; set; }
        public DateTime ServiceExpirationDate { get; set; }
        public string Certificate { get; set; }
        public string ServiceDescription { get; set; }
        public string TechnicalContactUrl { get; set; }
        public string TransportProfile { get; set; }
        public string MinimumAuthenticationLevel { get; set; }
    }
}
