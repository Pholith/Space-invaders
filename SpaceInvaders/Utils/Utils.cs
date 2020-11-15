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
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Game.game.random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T GetRandom<T>(this List<T> lst)
        {
            Random r = new Random();
            return lst[r.Next(lst.Count)];
        }

        public static bool NextBool(this Random r)
        {
            return r.NextDouble() < 0.5;
        }
        public static double NextDouble(this Random r, double min, double max)
        {
            return r.NextDouble() * (max - min) + min;
        }

        public static double DegToRad(int degree)
        {
            return degree * (Math.PI / 180);
        }


        public static Bitmap RecolorImage(Bitmap img, Color color)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    if (img.GetPixel(i, j).A == 255) img.SetPixel(i, j, color);
                }
            }
            return img;
        }

        /// <summary>
        /// Inverts the color between the game foreground and the game background.
        /// It makes the gameobjects white from black images.
        /// </summary>
        /// <param name="img">The color inverted img.</param>
        /// <returns></returns>
        public static Bitmap InvertColor(this Bitmap img)
        {
            Color color = Game.foregroundColor;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixelColor = img.GetPixel(i, j);
                    if (pixelColor.A == 255 && 
                        pixelColor.R == Game.backgroundColor.R &&
                        pixelColor.G == Game.backgroundColor.G &&
                        pixelColor.B == Game.backgroundColor.B) img.SetPixel(i, j, color);
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
