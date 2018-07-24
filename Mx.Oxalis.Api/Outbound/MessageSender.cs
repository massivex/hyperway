namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface MessageSender
    {

        /**
         * Protocol specific transmission of transmission requested. (Without tracing.)
         */
        TransmissionResponse send(TransmissionRequest transmissionRequest);

        /**
        * Protocol specific transmission of transmission requested. (With tracing.)
        */
        TransmissionResponse send(TransmissionRequest transmissionRequest, Trace root);
    }
}