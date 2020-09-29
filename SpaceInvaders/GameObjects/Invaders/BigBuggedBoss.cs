using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class BigBuggedBoss : AutoInvader
    {
        public BigBuggedBoss(Vecteur2D v1) : base(v1, 0, 100)
        {

        }

        public override Bitmap GetImage()
        {
            Image img = new List<Bitmap>() {Resources.ship2,
                Resources.ship3,
                Resources.ship5,
                Resources.ship6,
                Resources.ship7,
                Resources.ship8,
                Resources.ship9 }.GetRandom().InvertColor();
            return Utils.Utils.ResizeImage(img, (int) Size.X, (int) Size.Y);
        }

        public override void Init(Game gameInstance)
        {
            Size = new Vecteur2D(80, 80);
            AddNewAction(new TimedAction(9, () =>
            {
                AddNewAction(new TimedAction(0.2, () =>
                {

                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(-Math.Cos(inc % Math.PI), 0));

                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(-20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vecteur2D(Math.Cos(inc % Math.PI), 0));


                    new LaserBall(Position + new Vecteur2D(-Size.X / 2, Size.Y / 2), new Vecteur2D(-120, 200), 
                        null, (obj, deltaT, inc) => new Vecteur2D(Math.Cos(inc), 0) + obj.Position);

                    new LaserBall(Position + new Vecteur2D(Size.X / 2, Size.Y / 2), new Vecteur2D(120, 200),
                        null, (obj, deltaT, inc) => new Vecteur2D(Math.Cos(inc), 0) + obj.Position);

                }, true, false, 6));
            }, true));
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            Image sprite = GetImage();
            Size = new Vecteur2D(sprite.Width, sprite.Height);
            graphics.DrawImage(sprite, GetAnchorX(), GetAnchorY(), sprite.Width, sprite.Height);
        }

        public override bool IsPointOnPixel(Vecteur2D position)
        {
            return IsPointSuperposingSquare(position);
        }

        public override void DestroyPixel(Vecteur2D position)
        {
            return;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);
        }

    }
}
