namespace Mx.Hyperway.Commons.Timestamp
{
    using System;

    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Timestamp;

    using zipkin4net;

    public class SystemTimestampProvider : ITimestampProvider
    {
        // @Override
        public Timestamp Generate(byte[] content, Direction direction)
        {
            return new Timestamp(DateTime.Now, null);
        }

        public Timestamp Generate(byte[] content, Direction direction, Trace span)
        {
            return this.Generate(content, direction);
        }
    }
}
