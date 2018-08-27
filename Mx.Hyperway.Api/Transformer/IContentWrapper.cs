namespace Mx.Hyperway.Api.Transformer
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface IContentWrapper
    {

        Stream Wrap(Stream inputStream, Header header);

    }
}
