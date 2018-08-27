namespace Mx.Hyperway.Api.Outbound
{
    using System;

    using Mx.Hyperway.Api.Transmission;
    using Mx.Peppol.Common.Model;

    public interface ITransmissionResponse : ITransmissionResult
    {
        Endpoint GetEndpoint();

        /// <summary>
        /// Provides access to the native transmission evidence like for instance the MDN for AS2 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        byte[] GetNativeEvidenceBytes();
    }
}
