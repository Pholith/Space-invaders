using SpaceInvaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class LaserBall : Laser
    {
        public LaserBall(Vecteur2D v1, Vecteur2D speed) : base(v1, speed)
        {
        }

        public override Bitmap GetImage()
        {
            return Resources.shootball;
        }

    }
}
