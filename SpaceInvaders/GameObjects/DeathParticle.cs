using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class DeathParticle : GameObject
    {

        public DeathParticle(Vecteur2D position) : base(position)
        {
            Speed = new Vecteur2D(Game.game.random.NextDouble(-1, 1) * 50, Game.game.random.NextDouble(-1, 1) * 50);
            Size = new Vecteur2D(Game.game.random.Next(3, 5), Game.game.random.Next(3, 5));

            double rand = Game.game.random.NextDouble();
            if (rand < 0.3) color = new SolidBrush(Color.Black);
            else if (rand < 0.6) color = new SolidBrush(Color.OrangeRed);
            else if (rand < 0.9) color = new SolidBrush(Color.Red);
            else color = new SolidBrush(Color.DarkOrange);

        }

        private readonly Brush color;

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            graphics.FillRectangle(color, GetAnchorX(), GetAnchorY(), (float) Size.X, (float) Size.Y);
        }

        private double liveTime = 1.5;
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);
            liveTime -= deltaT;
            if (liveTime <= 0) Kill();
        }
        public override Tag GetTag()
        {
            return Tag.Invincible;
        }
    }
}
