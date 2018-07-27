using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Commons.Persist
{
    using System.IO;

    using Mx.Hyperway.Commons.FileSystem;
    using Mx.Peppol.Common.Model;

    public class PersisterUtils
    {

        /**
         * Computes the Path for a directory into which your file artifacts associated with
         * the supplied header may be written. Any intermediate directories are created for you.
         */
        public static DirectoryInfo createArtifactFolders(DirectoryInfo baseFolder, Header header)
        {
            string folder = Path.Combine(
                baseFolder.FullName,
                FileUtils.filterString(header.getReceiver().getIdentifier()),
                FileUtils.filterString(header.getSender().getIdentifier()));

            return Directory.CreateDirectory(folder);
        }
    }
}
