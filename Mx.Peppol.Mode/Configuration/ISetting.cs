namespace Mx.Peppol.Mode.Configuration
{
    public interface ISetting<in TKeys>
    {
        string Get(TKeys key);
    }
}