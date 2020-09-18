using GameObjects;
using SpaceInvaders.GameObjects;
using System;

namespace SpaceInvaders.GameModes
{
    abstract class GameMode
    {
        private Ship player;
        private bool win;
        private bool ended;

        protected GameMode()
        {
        }

        public virtual void Init()
        {
            player = new PlayerShip(new Vecteur2D(Game.game.gameSize.Width / 2, Game.game.gameSize.Height - 50));
            
            new Bunker(new Vecteur2D(Game.game.gameSize.Width / 3, Game.game.gameSize.Height - 100));
            new Bunker(new Vecteur2D(Game.game.gameSize.Width / 1.5, Game.game.gameSize.Height - 100));

            win = false;
            ended = false;
        }

        /// <summary>
        /// Return true if the game is win.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsWin()
        {
            return win;
        }

        /// <summary>
        /// Return true if the game was end at last check
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEnd()
        {
            return ended;
        }

        /// <summary>
        /// Check if the game is end.
        /// </summary>
        /// <returns> Return true if the game is end </returns>
        public virtual bool CheckEnd()
        {
            if (!player.IsAlive())
            {
                ended = true;
                return true;
            }
            return false;
        }
    }
}
