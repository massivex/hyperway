using System;
using System.Collections.Generic;

namespace Mx.Peppol.Common.Model
{
    using System.Linq;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Lang;
    using Mx.Tools;

    public class ProcessMetadata<T> where T : ISimpleEndpoint
    {
        private readonly Dictionary<TransportProfile, T> endpoints = new Dictionary<TransportProfile, T>();

        public static ProcessMetadata<T> Of(ProcessIdentifier processIdentifier, params T[] endpoints)
        {
            return Of(new SingletonList<ProcessIdentifier>(processIdentifier), endpoints);
        }

        public static ProcessMetadata<T> Of(IList<ProcessIdentifier> processIdentifier, params T[] endpoints)
        {
            return Of(processIdentifier, endpoints.ToList());
        }

        public static ProcessMetadata<T> Of(ProcessIdentifier processIdentifier, IList<T> endpoints)
        {
            return new ProcessMetadata<T>(new SingletonList<ProcessIdentifier>(processIdentifier), endpoints);
        }

        public static ProcessMetadata<T> Of(IList<ProcessIdentifier> processIdentifier, List<T> endpoints)
        {
            return new ProcessMetadata<T>(processIdentifier, endpoints);
        }

        private ProcessMetadata(IList<ProcessIdentifier> processIdentifiers, IList<T> endpoints)
        {
            this.ProcessIdentifier = processIdentifiers;

            foreach (T endpoint in endpoints)
            {
                this.endpoints.Add(endpoint.TransportProfile, endpoint);
            }

        }

        public IList<ProcessIdentifier> ProcessIdentifier { get; }

        public IList<TransportProfile> TransportProfiles => this.endpoints.Keys.ToList();

        public IList<T> Endpoints => new List<T>(this.endpoints.Values);

        public T GetEndpoint(params TransportProfile[] transportProfiles) // throws EndpointNotFoundException
        {
            foreach (TransportProfile transportProfile in transportProfiles)
            {
                if (this.endpoints.ContainsKey(transportProfile))
                {
                    return this.endpoints[transportProfile];
                }
            }

            throw new EndpointNotFoundException("Unable to find endpoint information for given transport profile(s).");
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ProcessMetadata<T>)) return false;

            ProcessMetadata<T> that = (ProcessMetadata<T>)o;

            if (!this.ProcessIdentifier.Equals(that.ProcessIdentifier)) return false;
            return this.endpoints.Equals(that.endpoints);
        }


        public override int GetHashCode()
        {
            int result = this.ProcessIdentifier.GetHashCode();
            result = 31 * result + this.endpoints.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "ProcessMetadata{" + "processIdentifier=" + this.ProcessIdentifier + ", endpoints=" + this.endpoints
                   + '}';
        }
    }
}
