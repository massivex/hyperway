namespace Mx.Hyperway.Commons.Tracing
{
    using zipkin4net;

    public abstract class Traceable
    {
        /// <summary>
        /// Zipkin tracer implementation. 
        /// </summary>
        protected readonly Trace Tracer;

        /// <summary>
        /// Default constructor accepting a tracer. 
        /// </summary>
        /// <param name="tracer">Tracer from application context.</param>
        protected Traceable(Trace tracer)
        {
            this.Tracer = tracer;
        }
    }

}
