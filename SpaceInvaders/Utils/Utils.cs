using System;
using System.Collections.Generic;
using System.Data;
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

        public static T GetRandom<T>(this List<T> lst)
        {
            Random r = new Random();
            return lst[r.Next(lst.Count)];
        }
    }   
}
