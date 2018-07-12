using System;

namespace Mx.Oxalis.Standalone
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;

    using Autofac;

    using CommandLine;

    using Mx.Oxalis.Api.Model;
    using Mx.Oxalis.Api.Transmission;
    using Mx.Oxalis.Outbound;
    using Mx.Peppol.Common.Model;

    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(RunOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);
        }

        private static void printErrorMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            IoC.Setup();
            using (var scope = IoC.Container.BeginLifetimeScope())
            {
                RunInScope(scope, options);
                Console.ReadKey();
            }
        }

        public static object TransmissionParameters { get; set; }

        private static void HandleParseError(IEnumerable<Error> errors)
        {

        }

        private static List<FileInfo> LocateFiles(IEnumerable<string> files)
        {
            return files.Select(x => new FileInfo(x)).Where(x => x.Exists).ToList();
        }

        private static void RunInScope(ILifetimeScope scope, Options options)
        {
            // bootstraps the Oxalis outbound module
            OxalisOutboundComponent oxalisOutboundComponent = scope.Resolve<OxalisOutboundComponent>();
            TransmissionParameters parameters = new TransmissionParameters(oxalisOutboundComponent);

            // Verifies the existence of a directory in which transmission evidence is stored.
            DirectoryInfo evidencePath = options.Evidence == null ? null : new DirectoryInfo(options.Evidence);
            if (evidencePath == null)
            {
                evidencePath = new DirectoryInfo("."); // Default is current directory
            }

            if (!evidencePath.Exists)
            {
                printErrorMessage(evidencePath + " does not exist or is not a directory");
            }

            parameters.EvidencePath = evidencePath;

            // --- Use Factory
            parameters.UseFactory = options.UseRequestFactory;

            // --- Recipient
            String recipientId = options.Recipient;
            if (recipientId != null)
            {
                parameters.Receiver = ParticipantIdentifier.of(recipientId);
            }

            // --- Sender
            String senderId = options.Sender;
            if (senderId != null)
            {
                parameters.Sender = ParticipantIdentifier.of(senderId);
            }

            // --- Document type
            var docType = options.DocumentType;
            if (docType != null)
            {
                String value = options.DocumentType;
                parameters.DocType = DocumentTypeIdentifier.of(value);
            }

            // --- Process type
            var profileType = options.ProfileType;
            if (profileType != null)
            {
                String value = options.ProfileType; // profileType.value(optionSet);
                parameters.ProcessIdentifier = ProcessIdentifier.of(value);
            }

            // --- Probe
            if (options.Probe)
            {
                WebClient c = new WebClient();
                var url = options.DestinationUrl;
                var output = c.DownloadData(url);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(output));

            }
            else
            {

                // --- Destination URL, protocl and system identifier
                if (!string.IsNullOrEmpty(options.DestinationUrl) && !options.Probe)
                {
                    String destinationString = options.DestinationUrl;

                    X509Certificate certificate = X509Certificate.CreateFromCertFile(options.DestinationCertificate);
                    //try (InputStream inputStream = new FileInputStream(destinationCertificate.value(optionSet))) {
                    //    certificate = Validator.getCertificate(inputStream);
                    //}
                    parameters.Endpoint = Endpoint.of(
                        TransportProfile.AS2_1_0,
                        new Uri(destinationString),
                        certificate);
                }

                // Retrieves the name of the file to be transmitted
                IEnumerable<string> payloadFileSpec = options.InputFiles;

                List<FileInfo> files = LocateFiles(payloadFileSpec);

                Console.WriteLine("");
                Console.WriteLine("");

                int maxThreads = options.ThreadCount;
                log.Info("Using " + maxThreads + " threads");

                int repeats = options.RepeatCount;
                int maximumTransmissions = options.MaxTransmissions;

                // ExecutorService exec = Executors.newFixedThreadPool(maxThreads);
                // ExecutorCompletionService<TransmissionResult> ecs = new ExecutorCompletionService<>(exec);

                // long start = DateTime.Now;
                var watch = new Stopwatch();
                int submittedTaskCount = 0;
                foreach (FileInfo file in files)
                {

                    //if (!file.isFile() || !file.canRead()) {
                    //    log.error("File " + file + " is not a file or can not be read, skipping...");
                    //    continue;
                    //}

                    for (int i = 0; i < repeats; i++)
                    {
                        TransmissionTask transmissionTask = new TransmissionTask(parameters, file);
                        transmissionTask.Call();
                        //Future<TransmissionResult> submit = ecs.submit(transmissionTask);
                        //submittedTaskCount++;
                        //if (submittedTaskCount > maximumTransmissions) {
                        //    log.Info("Stopped submitting tasks at {} " + submittedTaskCount);
                        //    break;
                        //}
                    }

                    if (submittedTaskCount > maximumTransmissions)
                    {
                        break;
                    }
                }

                // Wait for the results to be available
                //List<TransmissionResult> results = new List<TransmissionResult>();
                //int failed = 0;
                //for (int i = 0; i<submittedTaskCount; i++) {
                //    try {
                //        Future<TransmissionResult> future = ecs.take();
                //        TransmissionResult transmissionResult = future.get();
                //        results.add(transmissionResult);
                //    } catch (InterruptedException e) {
                //        System.err.println(e.getMessage());
                //    } catch (ExecutionException e) {
                //        log.error("Execution failed: {}", e.getMessage(), e);
                //        failed++;
                //    }
                //}   

                //long elapsed = System.nanoTime() - start;
                //exec.shutdownNow(); // Shuts down the executor service

                //foreach (TransmissionResult transmissionResult in results) {
                //    TransmissionIdentifier transmissionIdentifier = transmissionResult.getTransmissionIdentifier();
                //    System.out.println(transmissionIdentifier + " transmission took " + transmissionResult.getDuration() + "ms");
                //}


                //OptionalDouble average = results.stream().mapToLong(TransmissionResult::getDuration).average();

                //if (average.isPresent()) {
                //    System.out.println("Average transmission time was " + average.getAsDouble() + "ms");
                //}
                //long elapsedInSeconds = TimeUnit.SECONDS.convert(elapsed, TimeUnit.NANOSECONDS);
                //Console.WriteLine("Total time spent: " + elapsedInSeconds + "s");
                //Console.WriteLine("Attempted to send " + results.size() + " files");
                //Console.WriteLine("Failed transmissions: " + failed);
                //if (results.size() > 0 && elapsedInSeconds > 0) {
                //    System.out.println("Transmission speed " + results.size() / elapsedInSeconds + " documents per second");
                //}

                //Thread.sleep(2000);
            }
        }
    }
}