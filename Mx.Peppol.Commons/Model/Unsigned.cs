using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    public class Unsigned<T> : PotentiallySigned<T> {

        private static readonly long serialVersionUID = 2731552303222094156L;

        public static Unsigned<T> of(T content)
        {
            return new Unsigned<T>(content);
        }

        private Unsigned(T content)
        {
            this.Content = content;
        }

        
        public T Content { get; }

        
        public PotentiallySigned<S> ofSubset<S>(S s)
        {
            return new Unsigned<S>(s);
        }

        
        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Unsigned<T>)) return false;

            Unsigned <T> unsigned = (Unsigned <T>) o;

            return this.Content.Equals(unsigned.Content);

        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }

        
        public override string ToString()
        {
            return "Unsigned{" + "content=" + this.Content + '}';
        }
    }

}
