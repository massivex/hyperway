namespace Mx.Peppol.Icd.Api
{
    using Mx.Peppol.Common.Model;

    public interface IIcd
    {

        string Identifier { get; }

        string Code { get; }

        Scheme Scheme { get; }

        string IssuingAgency { get; }
    }
}
