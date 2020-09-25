﻿using SpaceInvaders;
using SpaceInvaders.GameModes;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Drawing;

namespace GameObjects
{
    class Ship : LivingEntity, IImage
    {

        public Ship(Vecteur2D v1) : base(v1, 3)
        {
            Attack = AddNewAction(new TimedAction(0.8, () =>
            {
                new Laser(Position + new Vecteur2D(0, -(Size.Y / 2)), Tag.Player);
            }, true));
        }

        int speedMax = 200;

        public void MegaShoot()
        {
            AddNewAction(new TimedAction(0.5, () =>
            {
                new Laser(Position + new Vecteur2D(-2, -(Size.Y / 2)), Tag.Player);
                new Laser(Position + new Vecteur2D(0, -(Size.Y / 2)), Tag.Player);
                new Laser(Position + new Vecteur2D(2, -(Size.Y / 2)), Tag.Player);

                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(60, -200), tag: Tag.Player);
                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(40, -200), tag: Tag.Player);
                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(10, -200), tag: Tag.Player);

                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-60, -200), tag: Tag.Player);
                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-40, -200), tag: Tag.Player);
                new LaserBall(Position + new Vecteur2D(0, Size.Y / 2), new Vecteur2D(-20, -200), tag: Tag.Player);
            }, true, true, 2));
        }

        public bool ApplyHealBonus()
        {
            if (HP == 3) return false;
            HP = 3;
            LoadSprite();
            return true;
        }


        int bullet = 1;
        public TimedAction Attack { get; set; }

        /// <summary>
        /// Adds a bullet from a bonus.
        /// </summary>
        /// <returns> True if a bullet was added. </returns>
        public bool AddBullet()
        {
            if (bullet == 2) bullet++;
            int bulletFixed = bullet;
            if (bullet > 4) return false;

            else if (bullet > 3) Attack.Action += () => {
                new LaserBall(Position, new Vecteur2D(0, -80), tag: Tag.Player, del: (ball, deltaT, inc)
                    => new Vecteur2D(Math.Cos(inc), 0) + ball.Position
                );;
            };
            else Attack.Action += () => { new LaserBall(Position, new Vecteur2D(-40 + bulletFixed * 20, -100), tag:Tag.Player); };
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
            
            if (GetAnchorY() + Size.Y > Game.game.gameSize.Height)
                Position = new Vecteur2D(Position.X, Game.game.gameSize.Height - Size.Y/2);

        }

        public override Tag GetTag()
        {
            return Tag.Player;
        }

    }
}
