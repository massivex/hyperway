using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Icd.Api
{
    using Mx.Peppol.Common.Model;

    public interface Icd
    {

        String getIdentifier();

        String getCode();

        Scheme getScheme();

        String getIssuingAgency();
    }
}
