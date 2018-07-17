using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Security
{
    using Mx.Peppol.Mode.Configuration;
    
    public enum KeyStoreConf
    {
        [Setting("oxalis.keystore.path", "oxalis-keystore.jks")]
        Path,

        [SettingAttribute("oxalis.keystore.password", "changeit")]
        Password,

        [SettingAttribute("oxalis.keystore.key.alias", "ap")]
        KeyAlias,

        [SettingAttribute("oxalis.keystore.key.password", "changeit")]
        KeyPassword
    }
}
