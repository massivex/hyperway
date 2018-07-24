namespace Mx.Hyperway.Api.Evidence
{
    using System.IO;

    using Mx.Hyperway.Api.Transmission;

    // @FunctionalInterface
    // TODO: FunctionalInterface
    public interface EvidenceFactory
    {

        void write(Stream outputStream, TransmissionResult transmissionResult); // throws EvidenceException;
    }

}
