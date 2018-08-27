﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Commons.Persist
{
    using System.IO;

    using Autofac.Features.AttributeFilters;

    using log4net;

    using Mx.Hyperway.Api.Evidence;
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Persist;
    using Mx.Hyperway.Commons.FileSystem;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    public class DefaultPersister : IPayloadPersister, IReceiptPersister
    {

        public static readonly ILog LOGGER = LogManager.GetLogger(typeof(DefaultPersister));

        private readonly IEvidenceFactory evidenceFactory;

        private readonly DirectoryInfo inboundFolder;

        public DefaultPersister([KeyFilter("inbound")] DirectoryInfo inboundFolder, IEvidenceFactory evidenceFactory)
        {
            this.inboundFolder = inboundFolder;
            this.evidenceFactory = evidenceFactory;
        }

        public FileInfo Persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream)
        {
            var identifier = FileUtils.filterString(transmissionIdentifier.getIdentifier());
            var targetFolder = PersisterUtils.createArtifactFolders(inboundFolder, header).FullName;
            string targetFile = Path.Combine(targetFolder, $"{identifier}.doc.xml");

            var xmlData = inputStream.ToBuffer();
            File.WriteAllBytes(targetFile, xmlData);


            LOGGER.DebugFormat("Payload persisted to: {0}", targetFile);

            return new FileInfo(targetFile);
        }


        public void Persist(IInboundMetadata inboundMetadata, FileInfo payloadPath)
        {
            var targetFolder = PersisterUtils.createArtifactFolders(inboundFolder, inboundMetadata.GetHeader()).FullName;
            var identifier = FileUtils.filterString(inboundMetadata.GetTransmissionIdentifier().getIdentifier());
            string targetFile = Path.Combine(targetFolder, $"{identifier}.receipt.dat");

            using (var fs = File.Create(targetFile))
            {
                this.evidenceFactory.Write(fs, inboundMetadata);
            }

            LOGGER.DebugFormat("Receipt persisted to: {0}", targetFile);
        }
    }
}
