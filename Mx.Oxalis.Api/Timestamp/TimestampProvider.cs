namespace Mx.Hyperway.Api.Timestamp
{
    using Mx.Hyperway.Api.Model;

    using zipkin4net;

    public interface TimestampProvider
    {

        Timestamp generate(byte[] content, Direction direction); // throws TimestampException;

        Timestamp generate(byte[] content, Direction direction, Trace span);

        //    {
        //        return generate(content, direction);
        //}
    }
}
