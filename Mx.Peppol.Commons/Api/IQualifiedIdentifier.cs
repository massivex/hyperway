using System;

namespace Mx.Peppol.Common.Api
{
    using Mx.Peppol.Common.Model;

    public interface IQualifiedIdentifier
    {

        Scheme Scheme { get; }

        /// <summary>
        /// Identifier of participant.
        /// </summary>
        /// <returns></returns>
        String Identifier { get; }

        String Urlencoded();

    }

}
