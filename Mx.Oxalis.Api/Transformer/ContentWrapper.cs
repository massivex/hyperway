namespace Mx.Hyperway.Api.Transformer
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface ContentWrapper
    {

        Stream wrap(Stream inputStream, Header header);

    }
}
