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
            return ship.ApplyAttackSpeedBonus();
        }

    }
}
