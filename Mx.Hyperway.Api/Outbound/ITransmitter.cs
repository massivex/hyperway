namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface ITransmitter
    {
        /// <summary>
        /// Transmit content of transmission request. (No tracing.)
        /// </summary>
        ITransmissionResponse Transmit(ITransmissionMessage transmissionMessage);

        /// <summary>
        /// Transmit content of transmission request. (With tracing.)
        /// </summary>
        ITransmissionResponse Transmit(ITransmissionMessage transmissionMessage, Trace root);
    }
}
