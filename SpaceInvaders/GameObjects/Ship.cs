using SpaceInvaders.GameModes;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A ship is the game ship. 
    /// </summary>
    /// <seealso cref="PlayerShip" />
    class Ship : LivingEntity, IImage
    {

        public Ship(Vector2 v1) : base(v1, 3)
        {
            Attack = AddNewAction(new TimedAction(1, () =>
            {
                lastLaserFired = new Laser(Position + new Vector2(0, -(Size.Y / 2)), Tag.Player);
            }));

            megaShoot = AddNewAction(new TimedAction(1.5, () =>
            {
                new Laser(Position + new Vector2(-10, 2), Tag.Player);
                new Laser(Position + new Vector2(-10, 2), Tag.Player);
                new Laser(Position + new Vector2(0, 0), Tag.Player);
                new Laser(Position + new Vector2(10, 2), Tag.Player);
                new Laser(Position + new Vector2(10, 2), Tag.Player);
                
                int angleMegaShoot = 40;
                for (int i = -angleMegaShoot; i <= angleMegaShoot; i += 3)
                {
                    new LaserBall(Position, Vector2.FromAngle(-90 + i, Laser.baseSpeed), tag: Tag.Player);
                }
            }, false, true));
        }

        private Laser lastLaserFired = null;// Used only in space-invaders mode
        private int speedMax = 150;
        private bool invicible = false;

        private TimedAction megaShoot { get; set; }

        public void AddPower()
        {
            Power += 1;
        }
        public void UseMegaShoot()
        {
#if DEBUG
            megaShoot.DoIfPossible();
#endif
            if (Power < 1) return;
            if (megaShoot.DoIfPossible()) Power--;
        }

        public void ToggleInvicibility()
        {
            invicible = !invicible;
            if (invicible) SetCustomColor(Color.DeepSkyBlue);
            else SetCustomColor(Game.foregroundColor);
            LoadSprite();
        }
        public bool ApplyHealBonus()
        {
            if (HP == 3) return false;
            HP = 3;
            LoadSprite();
            return true;
        }
        public bool ApplyAttackSpeedBonus()
        {
            if (Attack.Couldown / 2 == 0.1) return false;
            Attack.Couldown = Math.Max(0.1, Attack.Couldown / 2);
            return true;
        }

        int bullet = 1;
        private TimedAction Attack { get; set; }

        /// <summary>
        /// Adds a bullet from a bonus.
        /// </summary>
        /// <returns> True if a bullet was added. </returns>
        public bool AddBullet()
        {
            if (bullet == 2) bullet++;
            int bulletFixed = bullet;
            if (bullet > 4) return false;

            else if (bullet > 3) Attack.Action += () =>
            {
                new LaserBall(Position, new Vector2(0, - Laser.baseSpeed / 2), tag: Tag.Player, del: (ball, deltaT, inc)
                    => new Vector2(Math.Cos(inc), 0) + ball.Position
                ); ;
            };
            else Attack.Action += () => { new LaserBall(Position, new Vector2(-40 + bulletFixed * 20, - Laser.baseSpeed * 0.8), tag: Tag.Player); };
            bullet++;
            return true;
        }
        protected void MoveLeft()
        {
            Speed = new Vector2(-speedMax, Speed.Y);
        }
        protected void MoveRight()
        {
            Speed = new Vector2(speedMax, Speed.Y);
        }
        protected void MoveUp()
        {
            if (!(Game.game.Mode is NormalMode)) Speed = new Vector2(Speed.X, -speedMax);
        }
        protected void MoveDown()
        {
            if (!(Game.game.Mode is NormalMode)) Speed = new Vector2(Speed.X, speedMax);
        }

        public override void OnHit(Laser laser)
        {
            if (invicible) return;
            base.OnHit(laser);
            if (!invicible) ToggleInvicibility();            
            AddNewAction(new TimedAction(1, () => { ToggleInvicibility(); }, true, false, 1));
        }
        public override void DestroyPixel(Vector2 position)
        {
            if (invicible) return;
            base.DestroyPixel(position);
        }
        /// <summary>
        /// The ship shoot a laser
        /// </summary>
        protected void Shoot()
        {
            if (Game.game.Mode.IsNormalMode() && lastLaserFired != null && lastLaserFired.IsAlive()) return; // Don't Shoot in normal mode if missile is still here
            Attack.DoIfPossible();
        }

        public Bitmap GetImage()
        {
            return Resources.playership;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            if (GetAnchorX() < 0)
                Position = new Vector2(Size.X / 2, Position.Y);

            if (GetAnchorX() + Size.X > Game.game.gameSize.Width)
                Position = new Vector2(Game.game.gameSize.Width - Size.X / 2, Position.Y);

            if (GetAnchorY() < 0)
                Position = new Vector2(Position.X, Size.Y / 2);

            if (GetAnchorY() + Size.Y > Game.game.gameSize.Height)
                Position = new Vector2(Position.X, Game.game.gameSize.Height - Size.Y / 2);

        }

        public override Tag GetTag()
        {
            return Tag.Player;
        }

    }
}
