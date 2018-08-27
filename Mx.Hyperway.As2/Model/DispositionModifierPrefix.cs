namespace Mx.Hyperway.As2.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class DispositionModifierPrefix
    {
        public static IEnumerable<DispositionModifierPrefix> Values()
        {
            return new[] { Error, Failure, Warning };
        }

        public static DispositionModifierPrefix ValueOf(string value)
        {
            return Values().First(x => x.value == value);
        }
        private readonly string value;

        DispositionModifierPrefix(string value)
        {
            this.value = value;
        }

        public static readonly DispositionModifierPrefix Error = new DispositionModifierPrefix("ERROR");
        public static readonly DispositionModifierPrefix Warning = new DispositionModifierPrefix("WARNING");
        public static readonly DispositionModifierPrefix Failure = new DispositionModifierPrefix("FAILURE");
    }
}