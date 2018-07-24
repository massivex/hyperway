namespace Mx.Hyperway.Api.Transformer
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface ContentDetector
    {
        Header parse(Stream inputStream);
    }
}
