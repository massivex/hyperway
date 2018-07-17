namespace Mx.Peppol.Mode.Configuration
{
    using System.Collections.Generic;
    using System.Reflection;

    public class Setting<TKeys> : ISetting<TKeys>
    {
        private readonly Mode mode;
        private readonly Dictionary<TKeys, SettingAttribute> propertyMap;
        private bool hasMap;

        public Setting(Mode mode)
        {
            this.mode = mode;
            this.hasMap = false;
            this.propertyMap = new Dictionary<TKeys, SettingAttribute>();
        }

        public string Get(TKeys key)
        {
            this.CreateMap();
            var path = this.propertyMap[key].Path;
            return this.mode.GetValue(path);
        }

        private void CreateMap()
        {
            if (this.hasMap)
            {
                return;
            }

            this.propertyMap.Clear();
            var enumType = typeof(TKeys);
            foreach (var member in enumType.GetMembers(BindingFlags.Public | BindingFlags.Static))
            {
                var settings = member.GetCustomAttribute<SettingAttribute>();
                if (settings == null)
                {
                    continue;
                }

                var value = (TKeys)enumType.InvokeMember(member.Name, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField, null, null, null);
                this.propertyMap.Add(value, settings);
            }

            this.hasMap = true;
        }
    }
}
