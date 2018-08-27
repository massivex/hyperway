namespace Mx.Hyperway.Api.Outbound
{
    using zipkin4net;

    public interface IMessageSender
    {
        /// <summary>
        /// Protocol specific transmission of transmission requested. (Without tracing.) 
        /// </summary>
        /// <param name="transmissionRequest">request</param>
        /// <returns></returns>
        ITransmissionResponse Send(ITransmissionRequest transmissionRequest);

        /// <summary>
        /// Protocol specific transmission of transmission requested. (With tracing.)
        /// </summary>
        /// <param name="transmissionRequest">request</param>
        /// <param name="root">trace span</param>
        /// <returns></returns>
        ITransmissionResponse Send(ITransmissionRequest transmissionRequest, Trace root);
    }
}