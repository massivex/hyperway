namespace Mx.Hyperway.Api.Evidence
{
    using System.IO;

    using Mx.Hyperway.Api.Transmission;

    public interface IEvidenceFactory
    {

        void Write(Stream outputStream, ITransmissionResult transmissionResult);
    }
}
