using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class StarParticle : GameObject
    {
        public StarParticle(Vecteur2D position) : base(position)
        {
            Speed = new Vecteur2D(0, 30);
            int randSize = Game.game.random.Next(2, 4);
            Size = new Vecteur2D(randSize, randSize);
        }

        private readonly Brush color = new SolidBrush(Color.DarkGray);

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            graphics.FillRectangle(color, GetAnchorX(), GetAnchorY(), (float)Size.X, (float)Size.Y);
        }

        public override Tag GetTag()
        {
            return Tag.Invincible;
        }
    }
}
