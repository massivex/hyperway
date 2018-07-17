namespace Mx.Peppol.Mode.Configuration
{
    public interface ISetting<TKeys>
    {
        string Get(TKeys key);
    }
}