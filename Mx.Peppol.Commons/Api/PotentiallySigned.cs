using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Api
{
    public interface PotentiallySigned<T>
    {

        T Content { get; }

        PotentiallySigned<S> ofSubset<S>(S s);
    }

}
