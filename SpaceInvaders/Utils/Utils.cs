using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Utils
{
    public static class UtilsExtensions
    {
        public static string ToReadableList<T>(this IEnumerable<T> lst)
        {
            StringBuilder b = new StringBuilder("[");
            foreach (T item in lst)
            {
                b.Append(item.ToString()).Append(", ");
            }
            if (lst.Count() > 1) b = b.Remove(b.Length - 2, 2);
            b.Append("]");
            return b.ToString();
        }
    }
}
