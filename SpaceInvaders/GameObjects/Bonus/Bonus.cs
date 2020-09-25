using GameObjects;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Bonus
{
    abstract class Bonus : GameObject, IImage
    {
        public Bonus(Vecteur2D position, Vecteur2D speed = null) : base(position)
        {
            if (speed is null) Speed = new Vecteur2D(0, 100);
            else Speed = speed;
        }


        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            foreach (var item in gameInstance.gameObjects)
            {
                if (item is Ship && item.AreSquareSuperposing(this))
                {
                    Ship ship = item as Ship;
                    if (!ApplyBonus(ship)) Game.game.AddScore(50);
                    Kill();
                    break;
                }
            }
        }


        /// <summary>
        /// Applies the bonus on the ship
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns> True if the bonus changed something on the ship </returns>
        protected abstract bool ApplyBonus(Ship ship);

        public override Tag GetTag()
        {
            return Tag.Invincible;
        }

        public abstract Bitmap GetImage();

    }
}
