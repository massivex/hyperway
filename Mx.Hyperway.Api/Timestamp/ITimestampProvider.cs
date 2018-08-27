namespace Mx.Hyperway.Api.Timestamp
{
    using Mx.Hyperway.Api.Model;

    using zipkin4net;

    public interface ITimestampProvider
    {

        Timestamp Generate(byte[] content, Direction direction);

        Timestamp Generate(byte[] content, Direction direction, Trace span);
    }
}
