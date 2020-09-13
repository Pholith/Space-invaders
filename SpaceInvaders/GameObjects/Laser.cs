using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Laser : GameObject, IImage
    {
        public Laser(Vecteur2D v1) : base(v1)
        {
            Speed = new Vecteur2D(0, -200);
        }

        public Bitmap getImage()
        {
            return Resources.shoot1;
        }
    }
}
