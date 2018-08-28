namespace Mx.Peppol.Common.Api
{
    public interface IPerformResult<out TResult>
    {
        TResult Action();
    }
}