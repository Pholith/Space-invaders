using GameObjects;
using SpaceInvaders.GameObjects;
using System.Drawing;

namespace SpaceInvaders.GameModes
{

    public abstract class GameMode
    {
        private Ship player;
        public bool Win { get; protected set; }
        public bool Ended { get; protected set; }

        protected double SecFromStart { get; private set; } = 0;

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

        public virtual void Update(double deltaT)
        {
            SecFromStart += deltaT;
            if (!player.IsAlive())
            {
                Ended = true;
                Win = false;
            }

        }


        public void Lose()
        {
            Ended = true;
            Win = false;
        }

        public virtual void Draw(Graphics g)
        {
            string strHP = "HP: " + player.HP;
            string score = "Score: " + Game.game.Score;
            
            SizeF sizeHP = g.MeasureString(strHP, Game.defaultFont);
            g.DrawString(strHP, Game.defaultFont, new SolidBrush(Game.foregroundColor), 0, Game.game.gameSize.Height - sizeHP.Height);

            SizeF sizeScore = g.MeasureString(score, Game.defaultFont);
            g.DrawString(score, Game.defaultFont, new SolidBrush(Game.foregroundColor), 0, Game.game.gameSize.Height - sizeHP.Height - sizeScore.Height);


        }
    }
}
