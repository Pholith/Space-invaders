using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Utils
{
    public static class Utils
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

        public static double NextDouble(this Random r, double min, double max)
        {
            return r.NextDouble() * (max - min) + min;
        }

        public static Bitmap RecolorImage(Bitmap img, Color color)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    img.SetPixel(i, j, color);
                }
            }
            return img;
        }

    }
}
