using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    class HealBonus : Bonus
    {
        public HealBonus(Vector2 position) : base(position)
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
