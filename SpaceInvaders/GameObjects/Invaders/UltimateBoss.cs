using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders
{
    class UltimateBoss : AutoInvader
    {
        public UltimateBoss(Vecteur2D v1) : base(v1, 50, 400, new Vecteur2D(baseSpeed / 2, 0))
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.ship2;
            return Utils.Utils.ResizeImage(img, img.Width * 6, img.Height * 6);
        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);
           
            AddNewAction(new TimedAction(14, () => new InvaderBigBoss(Position, new Vecteur2D(0, 10)), true, false));


            AddNewAction(new TimedAction(10, () =>
            {
                AddNewAction(new TimedAction(0.8, () =>
                {
                    new Laser(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(0, 200));

                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(60, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(50, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(40, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(30, 200));
                    
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-60, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-50, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-40, 200));
                    new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-30, 200));


                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(50, 200), new Vecteur2D(30, -100));
                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-50, 200), new Vecteur2D(30, -100));
                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(50, 200), new Vecteur2D(-30, -100));
                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-50, 200), new Vecteur2D(-30, -100));

                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-50, 200),
                        del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI) * 2, 0));

                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-50, 200),
                        del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(-inc % Math.PI) * 2, 0));


                }, true, false, 6));
            }, true));
        }

    }
}
