namespace Mx.Hyperway.Commons.IO
{
    using System.IO;

    using Mx.Tools;

    /**
     * Caching InputStream to be used when reading the beginning of a stream is needed before the stream is "reset" when
     * the exact amount of data is unknown and support for marking of is irrelevant.
     */
    public class PeekingInputStream : MemoryStream
    {

        private readonly byte[] content;

        private readonly Stream internlaInputStream;

        public PeekingInputStream(Stream sourceInputStream) // throws IOException
        {
            this.content = sourceInputStream.ToBuffer();
            this.internlaInputStream = new MemoryStream(this.content);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Return byte
            return this.internlaInputStream.Read(buffer, offset, count);
        }

        public byte[] getContent()
        {
            return this.content;
        }

        public Stream newInputStream() // throws IOException {
        {
            return new MemoryStream(this.content);
        }
    }
}


