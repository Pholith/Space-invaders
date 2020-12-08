using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders.Boss
{
    public class SmartBoss : Invader
    {
        public SmartBoss(Vector2 position) : base(position, 0, 200)
        {

        }
        public override Bitmap GetImage()
        {
            return Resources.smartBoss;
        }
        protected override int DestroyPixelSize()
        {
            return 1;
        }
        public override int GetScore()
        {
            return base.GetScore() * 10;
        }

        public override int GetNumberOfParticles()
        {
            return base.GetNumberOfParticles() * 3;
        }

        private int offsetZone = 30;
        private int multiplierSizeYZone = 5;
        private int speed = baseSpeed * 3;

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            // Dodge the player's bullets
            if (IsEnemyLaserInFrontOfMe())
            {
                double newSpeedX = 0;
                if (Speed == Vector2.zero)
                {
                    if (IsLeftFromPlayer()) newSpeedX = -speed;
                    else newSpeedX = speed;
                }
                if (IsTooMuchOnLeft()) newSpeedX = speed * 1.5;
                if (IsTooMuchOnRight()) newSpeedX = -speed * 1.5;

                // Only change direction if boss is on the limits or if the boss was immobile
                if (newSpeedX != 0) Speed = new Vector2(newSpeedX, 0);
            }
            else Speed = Vector2.zero;

        }
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            // Debug view zone 
            //Rectangle rect = new Rectangle((int) GetAnchorX() - offsetZone, (int) GetAnchorY(), (int) Size.X + offsetZone*2, (int) Size.Y * MultiplierSizeYZone);
            //graphics.DrawRectangle(new Pen(Brushes.Red), rect);
        }
        public override void Init(Game gameInstance)
        {
            Speed = Vector2.zero; // This boss don't move like a normal invader

            AddNewAction(new TimedAction(10, () =>
            {
                int rand = Game.game.random.Next(1, 4);
                switch (rand)
                {
                    case 1:
                        AttackPatternRockets();
                        break;
                    case 2:
                        AttackPatternSafeZone();
                        break;
                    case 3:
                        AttackPatternIntersect();
                        break;
                    default:
                        Debug.Print("Undefined Action ! " + rand.ToString());
                        break;
                }

            }, true, true));
        }
        private bool IsEnemyLaserInFrontOfMe()
        {
            foreach (var go in Game.game.gameObjects)
            {
                if (go is Laser)
                {
                    Laser laser = (Laser)go;
                    if (!laser.CanHit(this)) continue;

                    // If the laser is in the warning zone of the boss
                    if (!(laser.GetAnchorX() > GetAnchorX() + Size.X + offsetZone ||
                        laser.GetAnchorY() > GetAnchorY() + Size.Y * multiplierSizeYZone ||
                        GetAnchorX() - offsetZone > laser.GetAnchorX() + laser.Size.X ||
                        GetAnchorY() > laser.GetAnchorY() + laser.Size.Y)) return true;
                }
            }

            return false;
        }
        private bool IsLeftFromPlayer()
        {
            return Position.X < Game.game.Mode.Player.Position.X;
        }
        private bool IsTooMuchOnLeft()
        {
            return Position.X < Size.X;
        }
        private bool IsTooMuchOnRight()
        {
            return Position.X > Game.game.gameSize.Width - Size.X;
        }

        #region Attack patterns
        private void AttackPatternIntersect()
        {
            AddNewAction(new TimedAction(0.2, () =>
            {
                new LaserBall(Position, new Vector2(0, Laser.baseSpeed), del: (LaserBall ball, double deltaT, double increment)
                   => new Vector2(Math.Cos(increment), 0) + ball.Position
                );
                new LaserBall(Position, new Vector2(0, Laser.baseSpeed), del: (LaserBall ball, double deltaT, double increment)
                   => new Vector2(-Math.Cos(increment), 0) + ball.Position
                );
            }, true, false, 20));

        }

        private void AttackPatternSafeZone()
        {
            // Angle 0 = Right
            int startAngle = 30;
            int endAngle = 180 - 30;

            int safeZoneLarge = 15;
            int degreeSafeZone = Game.game.random.Next(50, 180 - 50);
            int moving = Game.game.random.NextBool() ? 3 : -3;

            AddNewAction(new TimedAction(0.5, () =>
            {
                degreeSafeZone += moving;
                for (int i = startAngle; i <= endAngle; i+= 5)
                {
                    if (i > degreeSafeZone - safeZoneLarge && i < degreeSafeZone + safeZoneLarge) continue;
                    new LaserBall(Position, Vector2.FromAngle(i, baseBulletSpeed));
                }
            }, true, false, 10));

        }


        private void AttackPatternRockets()
        {
            AddNewAction(new TimedAction(0.8, () =>
            {
                new LaserBall(Position, Vector2.zero, del: (LaserBall ball, double deltaT, double increment)
                    => ball.Position + Vector2.FromTargetObject(ball.Position, Game.game.Mode.Player.Position, baseBulletSpeed) * deltaT, ttl: 6
                ).SetCustomColor(Color.DarkOrange);
            }, true, false, 6));
        }
        #endregion
    }
}
