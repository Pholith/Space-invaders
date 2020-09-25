using GameObjects;
using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class BulletsBonus : Bonus
    {
        public BulletsBonus(Vecteur2D position) : base(position)
        {

        }

        public override Bitmap GetImage()
        {
            return Resources.bonus;
        }

        protected override bool ApplyBonus(Ship ship)
        {
            return ship.AddBullet();
        }
        
    }
}
