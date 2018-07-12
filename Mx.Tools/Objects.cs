namespace Mx.Tools
{
    using System;

    public class Objects
    {
        public static int HashSet(Object[] a)
        {

            if (a == null)
                return 0;

            int result = 1;

            foreach (Object element in a)
            {
                result = 31 * result + (element == null ? 0 : element.GetHashCode());
            }

            return result;
        }

        public static int HashAll(params Object[] a)
        {
            return Objects.HashSet(a);
        }

    }
}
