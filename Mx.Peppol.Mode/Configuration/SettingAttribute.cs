namespace Mx.Peppol.Mode.Configuration
{
    using System;

    public class SettingAttribute : Attribute
    {
        public SettingAttribute(string path): this(path, null) { }

        public SettingAttribute(string path, string defaultValue)
        {
            this.Path = path;
            this.DefaultValue = defaultValue;
        }
        public string Path { get; set; }
        public string DefaultValue { get; set; }
    }
}
