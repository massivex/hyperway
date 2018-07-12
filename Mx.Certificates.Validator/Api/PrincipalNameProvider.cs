using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{

    /**
     * Used by PrincipalNameValidator to implement validation logic.
     */
    public interface PrincipalNameProvider<T>
    {
        bool validate(T value);
    }

}
