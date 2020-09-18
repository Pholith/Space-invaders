using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class Bunker : LivingEntity, IImage
    {
        public Bunker(Vecteur2D v1) : base(v1, -1)
        {

        }
        
        public override Tag GetTag()
        {
            return Tag.Neutral;
        }

        public Bitmap GetImage()
        {
            return Resources.bunker;
        }

        
    }
}
