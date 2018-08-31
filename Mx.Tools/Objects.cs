namespace Mx.Tools
{
    public class Objects
    {
        public static int HashSet(object[] a)
        {

            if (a == null)
                return 0;

            int result = 1;

            foreach (object element in a)
            {
                result = 31 * result + (element?.GetHashCode() ?? 0);
            }

            return result;
        }

        public static int HashAll(params object[] a)
        {
            return Objects.HashSet(a);
        }

    }
}
