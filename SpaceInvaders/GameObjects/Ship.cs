using SpaceInvaders;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace GameObjects
{
    class Ship : GameObject
    {

        private double radius = 10;
        private static Pen pen = new Pen(Color.Black);

        public Ship(Vecteur2D v1) : base(v1)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            TextReader inputStream = new StreamReader(assembly.GetManifestResourceStream(resourceName));
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            graphics.DrawImage(Image.FromFile("ship1.png"), 0, 0);
            /*float xmin = (float)(Position.X - radius);
            float ymin = (float)(Position.Y - radius);
            float width = (float)(2 * radius);
            float height = (float)(2 * radius);
            graphics.DrawEllipse(pen, xmin, ymin, width, height);*/
        }
    }
}
