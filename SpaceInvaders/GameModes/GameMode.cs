using GameObjects;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.GameModes
{

    public abstract class GameMode
    {
        private Ship player;
        public bool Win { get; protected set; }
        public bool Ended { get; protected set; }

        protected GameMode()
        {
        }

        public virtual void Init()
        {
            player = new PlayerShip(new Vecteur2D(Game.game.gameSize.Width / 2, Game.game.gameSize.Height - 50));

            new Bunker(new Vecteur2D(Game.game.gameSize.Width / 3, Game.game.gameSize.Height - 100));
            new Bunker(new Vecteur2D(Game.game.gameSize.Width / 1.5, Game.game.gameSize.Height - 100));

            Win = false;
            Ended = false;
        }

        /// <summary>
        /// Check if the game is end.
        /// </summary>
        /// <returns> Return true if the game is end </returns>
        public virtual bool CheckEnd()
        {
            if (!player.IsAlive())
            {
                Ended = true;
                Win = false;
                return true;
            }
            return false;
        }

        public void Lose()
        {
            Ended = true;
            Win = false;
        }
    }
}
