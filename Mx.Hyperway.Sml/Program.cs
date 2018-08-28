using System;

namespace Mx.Hyperway.Sml
{
    using System.IO;
    using System.Xml.Serialization;

    class Program
    {
        static void Main(string[] args)
        {
            var smlDirecotry = LoadDirectory("sml-directory.xml");
            var ip = "10.10.100.179";

            var serverOptions = new SmlServerOptions { IPAddress = ip, Directory = smlDirecotry };
            var server = new SmlServer(serverOptions);
            server.Start();

            Console.WriteLine("SML Started");
            Console.ReadLine();
            server.Stop();
        }

        private static SmlDirectory LoadDirectory(string file)
        {
            SmlDirectory directory;
            XmlSerializer s = new XmlSerializer(typeof(SmlDirectory));
            using (var fs = File.OpenRead(file))
            {
                 directory = (SmlDirectory) s.Deserialize(fs);
            }

            return directory;
        }
    }
}
