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

        public Ship(Vecteur2D v1) : base(v1, 3)
        {
            Attack = AddNewAction(new TimedAction(0.8, () =>
            {
                new Laser(Position + new Vecteur2D(0, -(Size.Y / 2)), Tag.Player);
            }, true));

            megaShoot = AddNewAction(new TimedAction(1.5, () =>
            {
                new Laser(Position + new Vecteur2D(-10, 2), Tag.Player);
                new Laser(Position + new Vecteur2D(-10, 2), Tag.Player);
                new Laser(Position + new Vecteur2D(0, 0), Tag.Player);
                new Laser(Position + new Vecteur2D(10, 2), Tag.Player);
                new Laser(Position + new Vecteur2D(10, 2), Tag.Player);
                
                int angleMegaShoot = 40;
                for (int i = -angleMegaShoot; i <= angleMegaShoot; i += 3)
                {
                    new LaserBall(Position, Vecteur2D.FromAngle(-90 + i, Laser.baseSpeed), tag: Tag.Player);
                }
            }, false, true));
        }

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
                new LaserBall(Position, new Vecteur2D(0, - Laser.baseSpeed / 2), tag: Tag.Player, del: (ball, deltaT, inc)
                    => new Vecteur2D(Math.Cos(inc), 0) + ball.Position
                ); ;
            };
            else Attack.Action += () => { new LaserBall(Position, new Vecteur2D(-40 + bulletFixed * 20, - Laser.baseSpeed * 0.8), tag: Tag.Player); };
            bullet++;
            return true;
        }
        protected void MoveLeft()
        {
            Speed = new Vecteur2D(-speedMax, Speed.Y);
        }
        protected void MoveRight()
        {
            Speed = new Vecteur2D(speedMax, Speed.Y);
        }
        protected void MoveUp()
        {
            if (!(Game.game.Mode is NormalMode)) Speed = new Vecteur2D(Speed.X, -speedMax);
        }
        protected void MoveDown()
        {
            if (!(Game.game.Mode is NormalMode)) Speed = new Vecteur2D(Speed.X, speedMax);
        }

        public override void OnHit(Laser laser)
        {
            if (invicible) return;
            base.OnHit(laser);
            if (!invicible) ToggleInvicibility();            
            AddNewAction(new TimedAction(1, () => { ToggleInvicibility(); }, true, false, 1));
        }
        public override void DestroyPixel(Vecteur2D position)
        {
            if (invicible) return;
            base.DestroyPixel(position);
        }
        protected void Shoot()
        {
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
                Position = new Vecteur2D(Size.X / 2, Position.Y);

            if (GetAnchorX() + Size.X > Game.game.gameSize.Width)
                Position = new Vecteur2D(Game.game.gameSize.Width - Size.X / 2, Position.Y);

            if (GetAnchorY() < 0)
                Position = new Vecteur2D(Position.X, Size.Y / 2);

            if (GetAnchorY() + Size.Y > Game.game.gameSize.Height)
                Position = new Vecteur2D(Position.X, Game.game.gameSize.Height - Size.Y / 2);

        }

        public override Tag GetTag()
        {
            return Tag.Player;
        }

    }
}
