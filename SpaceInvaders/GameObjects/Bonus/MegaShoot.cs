using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class MegaShoot : Bonus
    {
        public MegaShoot(Vector2 position) : base(position, new Vector2(0, 80))
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
