namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface MessageSender
    {

        /**
         * Protocol specific transmission of transmission requested. (Without tracing.)
         */
        TransmissionResponse send(ITransmissionRequest transmissionRequest);

        /**
        * Protocol specific transmission of transmission requested. (With tracing.)
        */
        TransmissionResponse send(ITransmissionRequest transmissionRequest, Trace root);
    }
}