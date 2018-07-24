namespace Mx.Hyperway.Commons.Interop
{
    public interface ICallable<out TResult>
    {
        TResult Call();
    }
}
