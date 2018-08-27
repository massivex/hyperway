namespace Mx.Hyperway.As2.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class DispositionType
    {
        public static readonly DispositionType Processed = new DispositionType("processed");

        public static readonly DispositionType Failed = new DispositionType("failed");

        private readonly string textValue;

        DispositionType(string textValue)
        {
            this.textValue = textValue;
        }

        public static IEnumerable<DispositionType> Values()
        {
            return new[] { Processed, Failed };
        }
        public static DispositionType ValueOf(string value)
        {
            return Values().First(x => x.textValue == value);
        }

        public string GetTextValue()
        {
            return this.textValue;
        }

    }
}