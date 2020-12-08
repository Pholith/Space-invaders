using SpaceInvaders.GameObjects;
using System.Drawing;

namespace SpaceInvaders.GameModes
{

    /// <summary>
    /// A Gamemode represent the style of game that was choiced in the menu.
    /// </summary>
    public abstract class GameMode
    {

        internal Ship Player { get; private set; }
        public bool Win { get; protected set; }
        public bool Ended { get; protected set; }

        protected double SecFromStart { get; private set; } = 0;

        protected GameMode()
        {
        }

        public virtual void Init()
        {
            Player = new PlayerShip(new Vector2(Game.game.gameSize.Width / 2, Game.game.gameSize.Height - 50));

            new Bunker(new Vector2(Game.game.gameSize.Width / 3, Game.game.gameSize.Height - 100));
            new Bunker(new Vector2(Game.game.gameSize.Width / 1.5, Game.game.gameSize.Height - 100));

            Win = false;
            Ended = false;
        }

        public virtual void Update(double deltaT)
        {
            SecFromStart += deltaT;
            if (!Player.IsAlive())
            {
                Ended = true;
                Win = false;
            }

        }

        public bool IsNormalMode()
        {
            return this is NormalMode;
        }

        public void Lose()
        {
            Ended = true;
            Win = false;
        }

        public virtual void Draw(Graphics g)
        {
            string strHP = "HP: " + Player.HP;
            string strPower = "Power: " + Player.Power;
            string score = "Score: " + Game.game.Score;

            SizeF sizeHP = g.MeasureString(strHP, Game.defaultFont);
            g.DrawString(strHP, Game.defaultFont, new SolidBrush(Game.foregroundColor), 0, Game.game.gameSize.Height - sizeHP.Height);

            SizeF sizePower = g.MeasureString(strPower, Game.defaultFont);
            g.DrawString(strPower, Game.defaultFont, new SolidBrush(Game.foregroundColor), 0, Game.game.gameSize.Height - sizeHP.Height - sizePower.Height);

            SizeF sizeScore = g.MeasureString(score, Game.defaultFont);
            g.DrawString(score, Game.defaultFont, new SolidBrush(Game.foregroundColor), 0, Game.game.gameSize.Height - sizeHP.Height - sizePower.Height - sizeScore.Height);

        }
    }
}
