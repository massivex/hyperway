using System;
using System.Collections.Generic;
using System.Linq;

namespace Mx.Hyperway.Smp
{
    using Mx.Hyperway.Smp.Data;

    public class TestData
    {
        public static void SetupDevDatabase(SmpContext context)
        {
            if (context.SmpHosts.Any())
            {
                // If db contains data, nothing will change
                return;
            }

            var transaction = context.Database.BeginTransaction();
            try
            {
                var host = new SmpHost { Hostname = "localhost" };
                context.SmpHosts.Add(host);
                context.PeppolParticipants.Add(new PeppolParticipant { Identifier = "iso6523-actorid-upis::9908:810418052", SmpHost = host });
                context.SaveChanges(true);
                var services = new Tuple<string, string>[]
                                   {
                                       new Tuple<string, string>(
                                           "cenbii-procid-ubl::urn:www.cenbii.eu:profile:bii05:ver2.0",
                                           "busdox-docid-qns::urn:oasis:names:specification:ubl:schema:xsd:Invoice-2::Invoice##urn:www.cenbii.eu:transaction:biitrns010:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0:extended:urn:www.ubl-italia.org:spec:fatturapa:ver2.0::2.1"),
                                       new Tuple<string, string>(
                                           "cenbii-procid-ubl::urn:www.cenbii.eu:profile:bii05:ver2.0",
                                           "busdox-docid-qns::urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2::CreditNote##urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0:extended:urn:www.ubl-italia.org:spec:fatturapa:ver2.0::2.1"),
                                       new Tuple<string, string>(
                                           "cenbii-procid-ubl::urn:www.cenbii.eu:profile:bii03:ver2.0",
                                           "busdox-docid-qns::urn:oasis:names:specification:ubl:schema:xsd:Order-2::Order##urn:www.cenbii.eu:transaction:biitrns001:ver2.0:extended:urn:www.peppol.eu:bis:peppol3a:ver2.0:extended:urn:www.ubl-italia.org:spec:ordine:ver2.1::2.1"),
                                       new Tuple<string, string>(
                                           "cenbii-procid-ubl::urn:www.cenbii.eu:profile:bii30:ver2.0",
                                           "busdox-docid-qns::urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2::DespatchAdvice##urn:www.cenbii.eu:transaction:biitrns016:ver1.0:extended:urn:www.peppol.eu:bis:peppol30a:ver1.0:extended:urn:www.ubl-italia.org:spec:ddt:ver2.1::2.1"),
                                   };
                var identifiers = services.Select(x => x.Item1).Distinct().ToList();
                var processes = new List<PeppolProcess>();
                foreach (var identifier in identifiers)
                {
                    processes.Add(new PeppolProcess { Identifier = identifier });
                }

                var documents = new List<PeppolDocument>();
                foreach (var service in services)
                {
                    documents.Add(new PeppolDocument { Identifier = service.Item2 });
                }


                var difiTestCert =
                    "MIIEgzCCA2ugAwIBAgIQQZ6YC/+Xh24jnO8I7rWBXjANBgkqhkiG9w0BAQsFADB9MQswCQYDVQQGEwJESzEnMCUGA1UEChMeTkFUSU9OQUwgSVQgQU5EIFRFTEVDT00gQUdFTkNZMR8wHQYDVQQLExZGT1IgVEVTVCBQVVJQT1NFUyBPTkxZMSQwIgYDVQQDExtQRVBQT0wgQUNDRVNTIFBPSU5UIFRFU1QgQ0EwHhcNMTgwNjI2MDAwMDAwWhcNMTkwNjI2MjM1OTU5WjBEMQswCQYDVQQGEwJJVDEcMBoGA1UECgwTUy5FLkQuSS5WLkEuIFMuci5sLjEXMBUGA1UEAwwOQVBQXzEwMDAwMDA0MzIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDpvQPqHjUObubj5Xi12ZllkFHixYOUbjL3S/fhqE14LX3b+3o27eyHNkZMaVNyzzWZof2aqIxtfrPmMRZyE4d6Tm9/QpUXeN/N1n2WgW86FipSZWvatW13cGShKsKW4CEJc8UzLUaVqF6VvOgX+uIRHSdRBJ964yaD/5tZXzslNl2RLyLcyaJtwg9kpXWJVc7BnxPNHOMtxHaJ/au0mzf4NcjPSi578JEY2T1rubPrHtxMut5ZG/fy4Ha7enSLGHME2utyq78DYo7YWyi0svI6vgE7SWdJ/taVsVwdZ2v5CgbHVqFwW/KiphYuQuOFkjx90FiMgekGBiM9hW9Hro0LAgMBAAGjggE2MIIBMjAJBgNVHRMEAjAAMAsGA1UdDwQEAwIDuDB8BgNVHR8EdTBzMHGgb6BthmtodHRwOi8vcGlsb3Qtb25zaXRlLWNybC5wa2kuZGlnaWNlcnQuY29tL0RpZ2l0YWxpc2VyaW5nc3N0eXJlbHNlblBpbG90T3BlblBFUFBPTEFDQ0VTU1BPSU5UQ0EvTGF0ZXN0Q1JMLmNybDAfBgNVHSMEGDAWgBT3losZTK7iViEAvob9ekesncoFdTAdBgNVHQ4EFgQUzAwakxkw5eJ2NapdRhwJejHHbbQwRQYIKwYBBQUHAQEEOTA3MDUGCCsGAQUFBzABhilodHRwOi8vcGlsb3Qtb25zaXRlLW9jc3AucGtpLmRpZ2ljZXJ0LmNvbTATBgNVHSUEDDAKBggrBgEFBQcDAjANBgkqhkiG9w0BAQsFAAOCAQEAjMjdzy7BbzfACAbNs1Iaj6XMWxP8A1EFrruGnCI1lT2b+f+mZAUYatQWcqDpuAgiLoT6opKA114oYTH1vz/8Ub0XnQThLcQfpW4HGbplZhwJPVL+WbGLrCxkk2SO89Nq42E9m6PgMzl0amoJhE2fYBvYJwEHRCppLlZv8ES6OpFqhXMOYersjmp8TYTVA4knRVa9gmGP2wsoVhhJz1ykzVy4vvVvRXhQQNljMXnVPCxpeSlHWwcJ+nDPQ84o5goskUHmr3g3oltmd37psTGxP3vLel2K/sywmKUHoqK30ad6pF8a+ZN6rnLQgjOguwT/Ixf91oa4u7qOofNUVU87Xg==";
                foreach (var service in services)
                {
                    var processId = processes.First(x => x.Identifier == service.Item1);
                    var documentId = documents.First(x => x.Identifier == service.Item2);
                    var participantId = context.PeppolParticipants.First();
                    var endpoint = new SmpServiceEndpoint
                                       {
                                           Certificate = difiTestCert,
                                           Endpoint = "http://localhost:26672/api/as2",
                                           RequireBusinessLevelSignature = true,
                                           ServiceActivationDate = DateTime.Now.Date.AddYears(-1),
                                           ServiceExpirationDate = DateTime.Now.Date.AddYears(1),
                                           ServiceDescription = "Access point for testing",
                                           TechnicalContactUrl = "support@xx.it",
                                           MinimumAuthenticationLevel = "1"
                                       };
                    var newService = new SmpService
                                         {
                                             PeppolDocument = documentId,
                                             PeppolProcess = processId,
                                             PeppolParticipant = participantId,
                                             Endpoints = new List<SmpServiceEndpoint> { endpoint }
                                         };
                    context.SmpServices.Add(newService);
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

    }
}
