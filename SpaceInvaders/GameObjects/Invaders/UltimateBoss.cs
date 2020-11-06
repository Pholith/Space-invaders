using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders
{
    /// <summary>
    /// The Ultimate boss is a big invader in the manic shooter
    /// </summary>
    /// <seealso cref="AutoInvader" />
    class UltimateBoss : AutoInvader
    {
        public UltimateBoss(Vecteur2D v1) : base(v1, 0, 350, new Vecteur2D(baseSpeed / 2, 0))
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.ship2;
            return Utils.Utils.ResizeImage(img, img.Width * 6, img.Height * 6);
        }

        public override void Init(Game gameInstance)
        {
            
            AddNewAction(new TimedAction(8, () =>
            {
                AddNewAction(new TimedAction(0.8, () =>
                {
                    new Laser(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(0, baseBulletSpeed));

                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(80, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(70, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(60, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(50, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(40, baseBulletSpeed));

                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-80, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-70, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-60, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-50, baseBulletSpeed));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-40, baseBulletSpeed));


                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(50, baseBulletSpeed), new Vecteur2D(-20, -30));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-50, baseBulletSpeed), new Vecteur2D(-20, -30));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(50, baseBulletSpeed), new Vecteur2D(20, -30));
                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-50, baseBulletSpeed), new Vecteur2D(20, -30));

                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(50, baseBulletSpeed),
                        del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI) * 2, 0));

                    new LaserBall(Position + new Vecteur2D(0, 0), new Vecteur2D(-50, baseBulletSpeed),
                        del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(-Math.Cos(inc % Math.PI) * 2, 0));


                }, true, false, 6));
            }, true));
        }

    }
}
