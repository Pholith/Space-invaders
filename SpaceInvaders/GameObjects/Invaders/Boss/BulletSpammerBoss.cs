﻿using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders.Boss
{
    public class BulletSpammerBoss : Invader
    {
        public BulletSpammerBoss(Vector2 position) : base(position, 0, 1000)
        {

        }

        public override Bitmap GetImage()
        {
            Image img = Resources.bulletSpammer;
            return Utils.Utils.ResizeImage(img, img.Width * 5, img.Height * 5);
        }
        protected override int DestroyPixelSize()
        {
            return 1;
        }
        public override int GetNumberOfParticles()
        {
            return base.GetNumberOfParticles() * 4;
        }

        double counter = 0;
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);
            counter += deltaT;
            Position += new Vector2(Math.Cos(counter), 0) / 15;
        }

        public override void Init(Game gameInstance)
        {
            Speed = Vector2.zero; // This boss don't move

            AddNewAction(new TimedAction(13, () =>
            {
                int rand = Game.game.random.Next(1, 5);
                switch (rand)
                {
                    case 1:
                        AttackPatternRound();
                        break;
                    case 2:
                        AttackPatternTargeted();
                        break;
                    case 3:
                        AttackPatternWarning();
                        break;
                    case 4:
                        AttackPatternTornado();
                        break;
                    default:
                        Debug.WriteLine("Undefined Action ! " + rand.ToString());
                        break;
                }

            }, true, true));
        }

        #region Attack patterns

        private void AttackPatternRound()
        {
            // Fire a round of bullets
            int angleIncrement = 4;
            int angleToFire = 400; // A little more than a round
            int angleCounter = 0; // in degree
            AddNewAction(new TimedAction(0.1, () =>
            {
                angleCounter += angleIncrement;
                new LaserBall(Position, Vector2.FromAngle(angleCounter, baseBulletSpeed));
                new LaserBall(Position, Vector2.FromAngle(angleCounter + 180, baseBulletSpeed));

            }, true, false, angleToFire / angleIncrement));
        }

        private void AttackPatternTornado()
        {
            AddNewAction(new TimedAction(0.2, () =>
            {
                double roundSizeCoeff = 2.2;
                // tourbillon of balls
                new LaserBall(Position + new Vector2(0, Size.Y / 2), Vector2.FromAngle(90, baseBulletSpeed * 2),
                    del: (LaserBall ball, double deltaT, double increment) =>
                    {
                        return ball.Position + new Vector2(0, 30) * deltaT + ball.Speed.Rotate(increment / roundSizeCoeff) * deltaT;
                    }, overrideMovement: true);

                new LaserBall(Position + new Vector2(0, Size.Y / 2), Vector2.FromAngle(90, baseBulletSpeed * 2),
                    del: (LaserBall ball, double deltaT, double increment) =>
                    {
                        return ball.Position + new Vector2(0, 30) * deltaT + ball.Speed.Rotate(-(increment / roundSizeCoeff)) * deltaT;
                    }, overrideMovement: true);


            }, true, false, 30));

        }
        private void AttackPatternWarning()
        {
            AddNewAction(new TimedAction(1, () => // Repeat the pattern some times
            {
                int middleAngle = 90;
                int randAngle = Game.game.random.Next(middleAngle - 50, middleAngle + 50);

                LaserBall l = new LaserBall(Position + new Vector2(0, Size.Y / 2), Vector2.FromAngle(randAngle, baseBulletSpeed));
                l.SetCustomColor(Color.Red);

                AddNewAction(new TimedAction(1.5, () => // Wait some seconds after the warning shoot
                {
                    AddNewAction(new TimedAction(0.2, () =>  // Bullet hell at the warned angle
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            new LaserBall(Position + new Vector2(0, Size.Y / 2), Vector2.FromAngle(randAngle + (-10 + i * 2), baseBulletSpeed * 3));
                        }

                    }, true, false, 5));
                }, true, false, 1));
            }, true, false, 8));
        }

        private void AttackPatternTargeted()
        {
            // Fire cibled bullet from the canon
            AddNewAction(new TimedAction(0.15, () =>
            {
                Random r = Game.game.random;
                int randRange = 30;
                Vector2 newBulletPosition = Position + new Vector2(0, Size.Y / 2);
                Vector2 newBulletSpeed = (Game.game.Mode.Player.Position + new Vector2(r.Next(-randRange, randRange), r.Next(-randRange, randRange)) - newBulletPosition).SetNewMagnitude(baseBulletSpeed);
                new LaserBall(newBulletPosition, newBulletSpeed);

            }, true, false, 30));
            for (int i = 0; i < (Game.game.gameSize.Width - baseSize) / baseSize; i++)
            {
                new AutoInvader(new Vector2(baseSize + i * baseSize, 0), 0, hp: 1, speed: new Vector2(0, baseSpeed));
            }
        }
        #endregion

    }
}
