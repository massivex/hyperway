using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Interop
{
    public interface ICallable<out TResult>
    {
        TResult Call();
    }
}
