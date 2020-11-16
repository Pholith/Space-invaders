using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders.Boss
{
    /// <summary>
    /// The Ultimate boss is a big invader in the manic shooter
    /// </summary>
    /// <seealso cref="AutoInvader" />
    public class UltimateBoss : AutoInvader
    {
        public UltimateBoss(Vecteur2D v1) : base(v1, 0, 700, new Vecteur2D(baseSpeed / 2, 0))
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.ship2;
            return Utils.Utils.ResizeImage(img, img.Width * 6, img.Height * 6);
        }
        public override int GetNumberOfParticles()
        {
            return base.GetNumberOfParticles() * 4;
        }
        protected override int DestroyPixelSize()
        {
            return base.DestroyPixelSize() / 3;
        }
        public override void Init(Game gameInstance)
        {
            AddNewAction(new TimedAction(8, () =>
            {
                int rand = Game.game.random.Next(1, 3);
                switch (rand)
                {
                    case 1:
                        AttackPatternA();
                        break;
                    case 2:
                        AttackPatternSlowWaves();
                        break;
                    default:
                        Debug.WriteLine("Undefined Action ! " + rand.ToString());
                        break;
                }

            }, true, false));
        }

        #region Attack Patterns
        private void AttackPatternA()
        {
            // The bullets do a shape of A
            AddNewAction(new TimedAction(0.8, () =>
            {
                new LaserBall(Position, new Vecteur2D(70, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(60, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(50, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(40, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(30, baseBulletSpeed));

                new LaserBall(Position, new Vecteur2D(-70, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(-60, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(-50, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(-40, baseBulletSpeed));
                new LaserBall(Position, new Vecteur2D(-30, baseBulletSpeed));

                new LaserBall(Position, new Vecteur2D(50, baseBulletSpeed), new Vecteur2D(-20, -30));
                new LaserBall(Position, new Vecteur2D(-50, baseBulletSpeed), new Vecteur2D(-20, -30));
                new LaserBall(Position, new Vecteur2D(50, baseBulletSpeed), new Vecteur2D(20, -30));
                new LaserBall(Position, new Vecteur2D(-50, baseBulletSpeed), new Vecteur2D(20, -30));

                new LaserBall(Position, new Vecteur2D(70, baseBulletSpeed),
                    del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI) * 2, 0));

                new LaserBall(Position, new Vecteur2D(-70, baseBulletSpeed),
                    del: (obj, deltaT, inc) => obj.Position + new Vecteur2D(-Math.Cos(inc % Math.PI) * 2, 0));

            }, true, false, 6));
        }
        private void AttackPatternSlowWaves()
        {
            AddNewAction(new TimedAction(0.2, () => // Repeat the pattern some times
            {
                int middleAngle = 90;
                int hitLarger = 60;
                int randAngle = Game.game.random.Next(middleAngle - hitLarger, middleAngle + hitLarger);

                for (int i = 0; i < 3; i++)
                {
                    new LaserBall(Position, Vecteur2D.FromAngle(randAngle + (-10 + i * 2), baseBulletSpeed));
                }
                // Create another wave far from the first
                int angle2 = randAngle + hitLarger;
                if (angle2 > middleAngle + hitLarger) angle2 = randAngle - hitLarger;

                for (int i = 0; i < 3; i++)
                {
                    new LaserBall(Position, Vecteur2D.FromAngle((angle2) + (-10 + i * 2), baseBulletSpeed));
                }


            }, true, false, 30));
        }

    }
    #endregion
}

