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

        /// <summary> 
        /// Resize the image to the specified width and height. 
        /// </summary> 
        /// <param name="image">The image to resize.</param> 
        /// <param name="width">The width to resize to.</param> 
        /// <param name="height">The height to resize to.</param> 
        /// <returns>The resized image.</returns> 
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            //a holder for the result 
            Bitmap result = new Bitmap(width, height);

            //use a graphics object to draw the resized image into the bitmap 
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality 
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //draw the image into the target bitmap 
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap 
            return result;
        }

    }
}
