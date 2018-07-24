namespace Mx.Hyperway.Commons.Tracing
{
    using zipkin4net;

    public abstract class Traceable
    {

        /**
         * Zipkin tracer implementation.
         */
        protected readonly Trace tracer;

        /**
         * Default constructor accepting a tracer.
         *
         * @param tracer Tracer from application context.
         */
        protected Traceable(Trace tracer)
        {
            this.tracer = tracer;
        }
    }

}
