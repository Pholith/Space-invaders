using GameObjects;
using SpaceInvaders.Properties;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class AttackSpeedBonus : Bonus
    {
        public AttackSpeedBonus(Vecteur2D position) : base(position)
        {

        }

        public override Bitmap GetImage()
        {
            return Resources.bonus2;
        }

        protected override bool ApplyBonus(Ship ship)
        {
            if (ship.Attack.Couldown / 2 == 0.1) return false;
            ship.Attack.Couldown = Math.Max(0.1, ship.Attack.Couldown / 2);
            return true;
        }

    }
}
