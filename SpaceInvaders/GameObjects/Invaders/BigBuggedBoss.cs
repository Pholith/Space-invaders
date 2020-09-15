using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class BigBuggedBoss : Invader
    {
        public BigBuggedBoss(Vecteur2D v1) : base(v1, 0, 15)
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
                Resources.ship9 }.GetRandom();
            return (Bitmap)img.GetThumbnailImage(img.Width * 2, img.Height * 2, null, IntPtr.Zero);
        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);

            AddNewAction(new TimedAction(8, () =>
            {
                AddNewAction(new TimedAction(0.1, () =>
                {
                    new LaserBall(Position + new Vecteur2D(40, Size.Y / 2), new Vecteur2D(30, 200));
                    new LaserBall(Position + new Vecteur2D(-40, Size.Y / 2), new Vecteur2D(-30, 200));
                    new LaserBall(Position + new Vecteur2D(-50, Size.Y / 2), new Vecteur2D(-120, 200));
                    new LaserBall(Position + new Vecteur2D(50, Size.Y / 2), new Vecteur2D(120, 200));

                }, true, false, 6));
            }, true));
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
  
            Image sprite = GetImage();
            Size = new Vecteur2D(sprite.Width, sprite.Height);
            graphics.DrawImage(sprite, GetAnchorX(), GetAnchorY(), sprite.Width, sprite.Height);
        }

    }
}
