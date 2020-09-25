﻿using GameObjects;
using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class HealBonus : Bonus
    {
        public HealBonus(Vecteur2D position) : base(position)
        {
        }

        public override Bitmap GetImage()
        {
            return Resources.bonusHealth;
        }

        protected override bool ApplyBonus(Ship ship)
        {
            return ship.ApplyHealBonus();
        }

    }
}
