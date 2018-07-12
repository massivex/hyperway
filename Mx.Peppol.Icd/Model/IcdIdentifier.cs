using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Icd.Model
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class IcdIdentifier
    {

    private static readonly long serialVersionUID = -7908081727801249085L;

    private readonly Icd icd;

    private readonly String identifier;

    public static IcdIdentifier of(Icd icd, String identifier)
    {
        return new IcdIdentifier(icd, identifier);
    }

    private IcdIdentifier(Icd icd, String identifier)
    {
        this.icd = icd;
        this.identifier = identifier;
    }

    public Icd getIcd()
    {
        return this.icd;
    }

    public String getIdentifier()
    {
        return this.identifier;
    }

    public ParticipantIdentifier toParticipantIdentifier()
    {
        return ParticipantIdentifier.of($"{this.icd.getCode()}:{this.identifier}", this.icd.getScheme());
    }

    public String toString()
    {
        return $"{this.icd.getScheme()}::{this.icd.getCode()}:{this.identifier}";
    }
    }


}
