using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class MegaShoot : Bonus
    {
        public MegaShoot(Vecteur2D position) : base(position, new Vecteur2D(0, 80))
        {
        }

        public override Bitmap GetImage()
        {
            return Resources.bonusMegaShoot;
        }

        protected override bool ApplyBonus(Ship ship)
        {
            ship.AddPower();
            return true;
        }
    }
}
