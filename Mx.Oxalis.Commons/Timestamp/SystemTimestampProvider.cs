namespace Mx.Oxalis.Commons.Timestamp
{
    using System;

    using Mx.Oxalis.Api.Model;
    using Mx.Oxalis.Api.Timestamp;

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
            return generate(content, direction);
        }
    }
}
