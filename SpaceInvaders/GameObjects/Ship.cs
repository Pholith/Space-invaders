using SpaceInvaders;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;

namespace GameObjects
{
    class Ship : GameObject, IImage
    {

        public Ship(Vecteur2D v1) : base(v1)
        {
            actions.Add("attack", new TimedAction(0.6, () => {
                new Laser(Position);
            }));
        }
        int speedMax = 300;
        int size = 45;

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
            actions["attack"].DoIfPossible();
        }

        public Bitmap GetImage()
        {
            return Resources.ship1;
        }
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            if (Position.X < 0)
                Position = new Vecteur2D(0, Position.Y);

            if (Position.X > Game.game.gameSize.Width - size)
                Position = new Vecteur2D(Game.game.gameSize.Width - size, Position.Y);
        }
    }
}
