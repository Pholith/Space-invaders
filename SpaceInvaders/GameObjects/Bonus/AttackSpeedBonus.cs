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

        protected override void ApplyBonus(Ship ship)
        {
            ship.Attack.Couldown = Math.Max(0.2, ship.Attack.Couldown / 2);
        }

    }
}
