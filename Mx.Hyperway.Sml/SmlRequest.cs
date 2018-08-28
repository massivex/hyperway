namespace Mx.Hyperway.Sml
{
    public class SmlRequest
    {
        public string Prefix { get; private set; }
        public string Hash { get; private set; }
        public string Scheme { get; private set; }

        public static SmlRequest Parse(string value)
        {
            string[] values = value.Split('.');
            var result = new SmlRequest();
            result.Prefix = null;
            if (values[0].StartsWith("B-"))
            {
                result.Prefix = "B-";
                values[0] = values[0].Substring(2);
            }
            result.Hash = values[0];
            result.Scheme = values[1];
            return result;
        }
    }
}
