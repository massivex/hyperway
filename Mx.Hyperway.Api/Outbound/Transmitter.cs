namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface Transmitter
    {

        /**
         * Transmit content of transmission request. (No tracing.)
         */
        TransmissionResponse transmit(TransmissionMessage transmissionMessage);

        /**
        * Transmit content of transmission request. (With tracing.)
        */
        TransmissionResponse transmit(TransmissionMessage transmissionMessage, Trace root);
    }
}
