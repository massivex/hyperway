namespace Mx.Hyperway.Commons.IO
{
    using System.IO;

    /// <inheritdoc />
    /// <summary>
    /// Simple wrapper of an InputStream making sure the close method on the encapsulated InputStream is never called. 
    /// </summary>
    public class UnclosableInputStream : MemoryStream
    {

        private readonly MemoryStream inputStream;

        public UnclosableInputStream(Stream inputStream)
        {
            this.inputStream = (MemoryStream) inputStream;
        }

        public MemoryStream GetStream()
        {
            return this.inputStream;
        }

        public override void Close()
        {
            // No action.
        }
    }
}
