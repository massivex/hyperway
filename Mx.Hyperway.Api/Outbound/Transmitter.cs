namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface ITransmitter
    {
        /// <summary>
        /// Transmit content of transmission request. (No tracing.)
        /// </summary>
        TransmissionResponse Transmit(ITransmissionMessage transmissionMessage);

        /// <summary>
        /// Transmit content of transmission request. (With tracing.)
        /// </summary>
        TransmissionResponse Transmit(ITransmissionMessage transmissionMessage, Trace root);
    }
}
