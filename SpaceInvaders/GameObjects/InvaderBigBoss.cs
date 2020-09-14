using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class InvaderBigBoss : Invader
    {
        public InvaderBigBoss(Vecteur2D v1) : base(v1, 0, 15)
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
                    new Laser(Position + new Vecteur2D(0, Size.Y), new Vecteur2D(0, 200));
                    new LaserBall(Position + new Vecteur2D(40, Size.Y / 1.5), new Vecteur2D(50, 200));
                    new LaserBall(Position + new Vecteur2D(-40, Size.Y / 1.5), new Vecteur2D(-50, 200));

                }, true, false, 5));
            }, true));
        }



    }
}
