﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    public class SmpHost : AuditableEntity
    {
        public int Id { get; set; }
        public string Hostname { get; set; }
    }
}
