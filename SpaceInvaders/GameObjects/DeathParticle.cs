using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A deathParticle is a particle instanciated at the death of an object. 
    /// </summary>
    /// <seealso cref="GameObject" />
    class DeathParticle : GameObject
    {

        public DeathParticle(Vector2 position) : base(position)
        {
            Speed = new Vector2(Game.game.random.NextDouble(-1, 1) * 70, Game.game.random.NextDouble(-1, 1) * 70);
            Size = new Vector2(Game.game.random.Next(3, 5), Game.game.random.Next(3, 5));

            double rand = Game.game.random.NextDouble();
            if (rand < 0.3) color = new SolidBrush(Game.foregroundColor);
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

        private double liveTime = 1;
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
