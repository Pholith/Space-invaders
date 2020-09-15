using SpaceInvaders;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Drawing;

namespace GameObjects
{
    class Ship : GameObject, IImage
    {

        public Ship(Vecteur2D v1) : base(v1)
        {
            attack = AddNewAction(new TimedAction(0.6, () =>
            {
                new Laser(Position + new Vecteur2D(0, -(Size.Y / 2)), Tag.Player);
            }));
        }

        int speedMax = 300;
        TimedAction attack; 


        protected void MoveLeft()
        {
            Speed = new Vecteur2D(-speedMax, 0);
        }
        protected void MoveRight()
        {
            Speed = new Vecteur2D(speedMax, 0);
        }

        protected void Shoot()
        {
            attack.DoIfPossible();
        }

        public Bitmap GetImage()
        {
            return Resources.playership;
        }
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            if (GetAnchorX() < 0)
                Position = new Vecteur2D(Size.X / 2, Position.Y);

            if (GetAnchorX() + Size.X > Game.game.gameSize.Width)
                Position = new Vecteur2D(Game.game.gameSize.Width - Size.X / 2, Position.Y);
        }

        public override Tag GetTag()
        {
            return Tag.Player;
        }

    }
}
