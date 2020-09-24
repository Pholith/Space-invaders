using GameObjects;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    abstract class Bonus : GameObject, IImage
    {
        public Bonus(Vecteur2D position) : base(position)
        {
            Speed = new Vecteur2D(0, 100);
        }


        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            foreach (var item in gameInstance.gameObjects)
            {
                if (item is Ship && item.AreSquareSuperposing(this))
                {
                    Ship ship = item as Ship;
                    ApplyBonus(ship);
                    Kill();
                    break;
                }
            }
        }
        protected abstract void ApplyBonus(Ship ship);

        public override Tag GetTag()
        {
            return Tag.Invincible;
        }

        public abstract Bitmap GetImage();
    }
}
