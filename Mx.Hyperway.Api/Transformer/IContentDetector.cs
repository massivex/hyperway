namespace Mx.Hyperway.Api.Transformer
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface IContentDetector
    {
        Header Parse(Stream inputStream);
    }
}
