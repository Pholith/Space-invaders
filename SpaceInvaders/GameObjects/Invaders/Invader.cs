﻿using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Invader : LivingEntity, IImage, IHitable
    {

        public Invader(Vecteur2D v1, int invaderType = 0, int hp = 1) : base(v1, hp)
        {
            this.invaderType = invaderType;
            Speed = new Vecteur2D(speedMax, 0);
        }

        public override void Init(Game gameInstance)
        {
            Random r = new Random();

            base.Init(gameInstance);
            AddNewAction(new TimedAction(r.Next(8, 15), () =>
            {
                new Laser(Position + new Vecteur2D(0, Size.Y), new Vecteur2D(0, 200));
            }, true));

        }

        int speedMax = 50;
        private int invaderType;

        public virtual Bitmap GetImage()
        {
            if (invaderType > 0)
            {
                object obj = Resources.ResourceManager.GetObject("ship" + invaderType);
                return (Bitmap)obj;
            }
            return new List<Bitmap>() {Resources.ship2,
                Resources.ship3,
                //Resources.ship4,
                Resources.ship5,
                Resources.ship6,
                Resources.ship7,
                Resources.ship8,
                Resources.ship9 }.GetRandom();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            if (GetAnchorX() < 0 && Speed.X < 0)
            {
                Position = new Vecteur2D(Position.X, GetAnchorY() + Size.Y + 20);
                Speed = new Vecteur2D(speedMax + Position.Y, 0);
            }
            if (GetAnchorX() + Size.X > Game.game.gameSize.Width && Speed.X > 0)
            {
                Position = new Vecteur2D(Position.X, GetAnchorY() + Size.Y + 20);
                Speed = new Vecteur2D(-speedMax - Position.Y, 0);
            }
        }

    }
}
