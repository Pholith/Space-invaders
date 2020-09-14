using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class Invader : GameObject, IImage
    {

        public Invader(Vecteur2D v1) : base(v1)
        {
            /*actions.Add("attack", new TimedAction(0.6, () => {
                new Laser(Position + new Vecteur2D(size / 2, 0));
            }));*/
            Speed = new Vecteur2D(speedMax, 0);

        }
        int speedMax = 60;

        public static readonly int size = 40;

        protected void Shoot()
        {
            //actions["attack"].DoIfPossible();
        }

        public Bitmap GetImage()
        {
            Random r = new Random();

            return new List<Bitmap>() {Resources.ship2,
                Resources.ship3,
                //Resources.ship4,
                Resources.ship5,
                Resources.ship6,
                Resources.ship7,
                Resources.ship8,
                Resources.ship9 }.GetRandom();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            if (Position.X < 0 && Speed.X < 0)
            {
                Position = new Vecteur2D(Position.X, Position.Y + size);
                Speed = new Vecteur2D(speedMax, 0);
            }
            if (Position.X > Game.game.gameSize.Width - size && Speed.X > 0)
            {
                Position = new Vecteur2D(Game.game.gameSize.Width - size, Position.Y + size);
                Speed = new Vecteur2D(-speedMax, 0);
            }
        }
    }
}
