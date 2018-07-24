namespace Mx.Hyperway.Commons.Security
{
    using Mx.Peppol.Mode.Configuration;

    public enum KeyStoreConf
    {
        [Setting("hyperway.keystore.path", "hyperway-keystore.pfx")]
        Path,

        [Setting("hyperway.keystore.password", "changeit")]
        Password,

        [Setting("hyperway.keystore.key.alias", "ap")]
        KeyAlias,

        [Setting("hyperway.keystore.key.password", "changeit")]
        KeyPassword
    }
}
