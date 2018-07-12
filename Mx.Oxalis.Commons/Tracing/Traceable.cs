using System;
using System.Collections.Generic;
using System.Text;
using zipkin4net;

namespace Mx.Oxalis.Commons.Tracing
{
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
