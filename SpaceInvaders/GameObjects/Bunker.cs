using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class Bunker : GameObject
    {
        Bunker(Vecteur2D v1) : base(v1)
        {

        }

        private List<BunkerCell> cells = new List<BunkerCell>();
    }
}
