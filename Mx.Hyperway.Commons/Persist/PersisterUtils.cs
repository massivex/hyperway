namespace Mx.Hyperway.Commons.Persist
{
    using System.IO;

    using Mx.Hyperway.Commons.FileSystem;
    using Mx.Peppol.Common.Model;

    public class PersisterUtils
    {
        /// <summary>
        /// Computes the Path for a directory into which your file artifacts associated with
        /// the supplied header may be written. Any intermediate directories are created for you.
        /// </summary>
        /// <param name="baseFolder"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateArtifactFolders(DirectoryInfo baseFolder, Header header)
        {
            string folder = Path.Combine(
                baseFolder.FullName,
                FileUtils.FilterString(header.Receiver.Identifier),
                FileUtils.FilterString(header.Sender.Identifier));

            return Directory.CreateDirectory(folder);
        }
    }
}
