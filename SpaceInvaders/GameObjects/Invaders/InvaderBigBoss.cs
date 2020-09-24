using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class InvaderBigBoss : AutoInvader
    {
        public InvaderBigBoss(Vecteur2D v1) : base(v1, 0, 20)
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.ship4;
            return (Bitmap)img.GetThumbnailImage(img.Width * 2, img.Height * 2, null, IntPtr.Zero);
        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);

            AddNewAction(new TimedAction(4, () =>
            {
                AddNewAction(new TimedAction(0.3, () =>
                {
                    new Laser(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(0, 200));
                    new Laser(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(0, 200));

                    new Laser(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(0, 200));
                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(50, 200));
                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-50, 200));

                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI), 0));

                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(-Math.Cos(inc % Math.PI), 0));


                }, true, false, 5));
            }, true));
        }


    }
}
