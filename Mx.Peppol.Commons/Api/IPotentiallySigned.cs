namespace Mx.Peppol.Common.Api
{
    public interface IPotentiallySigned<out T>
    {

        T Content { get; }

        IPotentiallySigned<TS> OfSubset<TS>(TS s);
    }

}
