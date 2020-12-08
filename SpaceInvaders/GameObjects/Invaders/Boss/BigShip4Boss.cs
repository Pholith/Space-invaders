using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders.Boss
{
    public class BigShip4Boss : AutoInvader
    {
        public BigShip4Boss(Vector2 v1, Vector2 speed = null) : base(v1, 0, 300, speed)
        {

        }
        public override int GetNumberOfParticles()
        {
            return base.GetNumberOfParticles() * 3;
        }
        public override Bitmap GetImage()
        {
            Image img = Resources.ship4;
            return Utils.Utils.ResizeImage(img, img.Width * 3, img.Height * 3);
        }

        public override void Init(Game gameInstance)
        {
            AddNewAction(new TimedAction(8, () => new AutoInvader(Position, 4, 2, new Vector2(baseSpeed * 5, 10)), true, false));

            AddNewAction(new TimedAction(5, () =>
            {
                AddNewAction(new TimedAction(0.3, () =>
                {
                    new Laser(new Vector2(GetAnchorX(), GetAnchorY() + Size.Y), new Vector2(0, baseBulletSpeed));
                    new Laser(new Vector2(GetAnchorX() + Size.X, GetAnchorY() + Size.Y), new Vector2(0, baseBulletSpeed));

                    new LaserBall(Position + new Vector2(Size.X / 2, Size.Y), new Vector2(20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vector2(Math.Cos(inc % Math.PI), 0));

                    new LaserBall(Position + new Vector2(-Size.X / 2, Size.Y), new Vector2(-20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vector2(-Math.Cos(inc % Math.PI), 0));

                }, true, false, 12));
            }, true));
        }

        protected override int DestroyPixelSize()
        {
            return base.DestroyPixelSize() / 3;
        }
    }
}
