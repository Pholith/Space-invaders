using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders
{
    class BulletSpammerBoss : Invader
    {
        public BulletSpammerBoss(Vecteur2D position) : base(position, 0, 800)
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

        public override void Init(Game gameInstance)
        {
            Speed = Vecteur2D.zero; // This boss don't move


            AddNewAction(new TimedAction(13, () =>
            {
                int rand = Game.game.random.Next(1, 4);
                switch (rand)
                {
                    case 1:
                        // Fire a round of bullets
                        int angleIncrement = 4;
                        int angleToFire = 400; // A little more than a round
                        int angleCounter = 0; // in degree
                        AddNewAction(new TimedAction(0.1, () =>
                        {
                            angleCounter += angleIncrement;
                            new LaserBall(Position, Vecteur2D.FromAngle(angleCounter, baseBulletSpeed));
                            new LaserBall(Position, Vecteur2D.FromAngle(angleCounter + 180, baseBulletSpeed));


                        }, true, false, angleToFire / angleIncrement));
                        break;

                    case 2:
                        // Fire target bullet from the canon
                        AddNewAction(new TimedAction(0.15, () =>
                        {
                            Random r = Game.game.random;
                            int randRange = 30;
                            Vecteur2D newBulletPosition = Position + new Vecteur2D(0, Size.Y / 2);
                            Vecteur2D newBulletSpeed = (Game.game.Mode.Player.Position + new Vecteur2D(r.Next(-randRange, randRange), r.Next(-randRange, randRange)) - newBulletPosition).SetNewMagnitude(baseBulletSpeed);
                            new LaserBall(newBulletPosition, newBulletSpeed);

                        }, true, false, 50));
                        break;

                    case 3:
                        AddNewAction(new TimedAction(2, () => // Repeat the pattern some times
                        {
                            int middleAngle = 90;
                            int randAngle = Game.game.random.Next(middleAngle - 50, middleAngle + 50);

                            LaserBall l = new LaserBall(Position + new Vecteur2D(0, Size.Y /2), Vecteur2D.FromAngle(randAngle, baseBulletSpeed));
                            l.SetCustomColor(Color.Red);

                            AddNewAction(new TimedAction(1.5, () => // Wait some seconds after the warning shoot
                            {
                                AddNewAction(new TimedAction(0.2, () =>  // Bullet hell at the warned angle
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), Vecteur2D.FromAngle(randAngle + (-10 + i * 2), baseBulletSpeed * 3));
                                    }

                                }, true, false, 5));
                            }, true, false, 1));
                        }, true, true, 6));
                        break;



                    default:
                        Debug.WriteLine("Undefined Action ! " + rand.ToString());
                        break;
                }



            }, true, true));
        }
    }
}
