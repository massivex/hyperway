namespace Mx.Hyperway.Commons.IO
{
    using System.IO;

    /**
     * Simple wrapper of an InputStream making sure the close method on the encapsulated InputStream is never called.
     */
    public class UnclosableInputStream : MemoryStream
    {

        private readonly MemoryStream inputStream;

        public UnclosableInputStream(MemoryStream inputStream)
        {
            this.inputStream = inputStream;
        }

        public override void Close() // throws IOException
        {
            // No action.
        }
    }
}
