namespace Mx.Hyperway.Commons.IO
{
    using System.IO;

    using Mx.Tools;

    /// <summary>
    /// Caching InputStream to be used when reading the beginning of a stream is needed before the stream is "reset" when
    /// the exact amount of data is unknown and support for marking of is irrelevant.
    /// </summary>
    public class PeekingInputStream : MemoryStream
    {

        private readonly byte[] content;

        private readonly Stream internlaInputStream;

        public PeekingInputStream(Stream sourceInputStream)
        {
            this.content = sourceInputStream.ToBuffer();
            this.internlaInputStream = new MemoryStream(this.content);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Return byte
            return this.internlaInputStream.Read(buffer, offset, count);
        }

        public byte[] GetContent()
        {
            return this.content;
        }

        public Stream NewInputStream() // throws IOException {
        {
            return new MemoryStream(this.content);
        }
    }
}


