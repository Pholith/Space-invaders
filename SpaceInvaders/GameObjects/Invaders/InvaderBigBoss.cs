using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class InvaderBigBoss : AutoInvader
    {
        public InvaderBigBoss(Vecteur2D v1, Vecteur2D speed = null) : base(v1, 0, 30, speed)
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.ship4;
            return Utils.Utils.ResizeImage(img, img.Width * 2, img.Height * 2);
        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);

            AddNewAction(new TimedAction(3, () => new AutoInvader(Position, 4, 2, new Vecteur2D(baseSpeed * 5, 10)), true, false));

            AddNewAction(new TimedAction(9, () =>
            {
                AddNewAction(new TimedAction(0.5, () =>
                {
                    new Laser(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(0, 200));
                    new Laser(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(0, 200));

                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI), 0));

                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(-Math.Cos(inc % Math.PI), 0));


                }, true, false, 5));
            }, true));
        }


    }
}
