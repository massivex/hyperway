using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using System.Collections;
    using System.Linq;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Lang;
    using Mx.Tools;

    public class ProcessMetadata<T>
        where T : SimpleEndpoint
    {

        private static readonly long serialVersionUID = -8684282659539348955L;

        private readonly IList<ProcessIdentifier> processIdentifier;

        private readonly Dictionary<TransportProfile, T> endpoints = new Dictionary<TransportProfile, T>();

        public static ProcessMetadata<T> of(ProcessIdentifier processIdentifier, params T[] endpoints)
        {
            return of(new SingletonList<ProcessIdentifier>(processIdentifier), endpoints);
        }

        public static ProcessMetadata<T> of(IList<ProcessIdentifier> processIdentifier, params T[] endpoints)
        {
            return of(processIdentifier, endpoints);
        }

        public static ProcessMetadata<T> of(ProcessIdentifier processIdentifier, IList<T> endpoints)
        {
            return new ProcessMetadata<T>(new SingletonList<ProcessIdentifier>(processIdentifier), endpoints);
        }

        public static ProcessMetadata<T> of(IList<ProcessIdentifier> processIdentifier, List<T> endpoints)
        {
            return new ProcessMetadata<T>(processIdentifier, endpoints);
        }

        private ProcessMetadata(IList<ProcessIdentifier> processIdentifiers, IList<T> endpoints)
        {
            this.processIdentifier = processIdentifiers;

            foreach (T endpoint in endpoints)
            {
                this.endpoints.Add(endpoint.getTransportProfile(), endpoint);
            }

        }

        public IList<ProcessIdentifier> getProcessIdentifier()
        {
            return this.processIdentifier;
        }

        public IList<TransportProfile> getTransportProfiles()
        {
            return this.endpoints.Keys.ToList();
        }

        public IList<T> getEndpoints()
        {
            return new List<T>(this.endpoints.Values);
        }

        public T getEndpoint(params TransportProfile[] transportProfiles) // throws EndpointNotFoundException
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

            if (!this.processIdentifier.Equals(that.processIdentifier)) return false;
            return this.endpoints.Equals(that.endpoints);
        }


        public override int GetHashCode()
        {
            int result = this.processIdentifier.GetHashCode();
            result = 31 * result + this.endpoints.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "ProcessMetadata{" + "processIdentifier=" + this.processIdentifier + ", endpoints=" + this.endpoints
                   + '}';
        }
    }
}
