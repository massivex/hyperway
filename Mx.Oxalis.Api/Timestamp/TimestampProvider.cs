using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Timestamp
{
    using Mx.Oxalis.Api.Model;

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
