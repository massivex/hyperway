namespace Mx.Hyperway.Commons.Timestamp
{
    using System;

    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Timestamp;

    using zipkin4net;

    public class SystemTimestampProvider : TimestampProvider
    {
        // @Override
        public Timestamp generate(byte[] content, Direction direction)
        {
            return new Timestamp(DateTime.Now, null);
        }

        public Timestamp generate(byte[] content, Direction direction, Trace span)
        {
            return this.generate(content, direction);
        }
    }
}
