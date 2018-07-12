using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Api
{
    using Mx.Peppol.Common.Model;

    public interface QualifiedIdentifier
    {

        Scheme getScheme();

        /**
         * Identifier of participant.
         *
         * @return Identifier.
         */
        String getIdentifier();

        String urlencoded();

    }

}
