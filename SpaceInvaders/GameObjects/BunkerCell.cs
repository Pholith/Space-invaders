using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class BunkerCell : LivingEntity
    {
        BunkerCell(Vecteur2D v1) : base(v1, 1)
        {

        }
        
        public override Tag GetTag()
        {
            return Tag.Player;
        }
    }
}
