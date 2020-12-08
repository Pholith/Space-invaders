using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders.Boss
{
    public class BigBuggedBoss : AutoInvader
    {
        public BigBuggedBoss(Vector2 v1) : base(v1, 0, 300, new Vector2(baseSpeed / 2, 5))
        {

        }

        public override int GetNumberOfParticles()
        {
            return base.GetNumberOfParticles() * 3;
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
            return Utils.Utils.ResizeImage(img, (int)Size.X, (int)Size.Y);
        }

        public override void Init(Game gameInstance)
        {
            Size = new Vector2(80, 80);
            AddNewAction(new TimedAction(5, () =>
            {
                AddNewAction(new TimedAction(0.2, () =>
                {

                    new LaserBall(Position + new Vector2(-Size.X / 2, Size.Y / 2), new Vector2(20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vector2(-Math.Cos(inc % Math.PI), 0));

                    new LaserBall(Position + new Vector2(Size.X / 2, Size.Y / 2), new Vector2(-20, 100),
                        null, (obj, deltaT, inc) => obj.Position + new Vector2(Math.Cos(inc % Math.PI), 0));


                    // Target the player
                    Vector2 newBulletPosition = Position + new Vector2(-Size.X / 2, Size.Y / 2);
                    new LaserBall(newBulletPosition, Vector2.FromTargetObject(newBulletPosition, Game.game.Mode.Player.Position, baseBulletSpeed));

                    Vector2 newBulletPosition2 = Position + new Vector2(Size.X / 2, Size.Y / 2);
                    new LaserBall(newBulletPosition, Vector2.FromTargetObject(newBulletPosition, Game.game.Mode.Player.Position, baseBulletSpeed));


                }, true, false, 10));
            }, true));
            AddNewAction(new TimedAction(1.5, () =>
            {
                int offSet = 10;
                for (int i = -offSet; i <= offSet; i+= offSet * 2)
                {
                    Vector2 initPosition = new Vector2(i, 0);
                    new LaserBall(initPosition, Vector2.FromTargetObject(initPosition, Game.game.Mode.Player.Position, baseBulletSpeed));
                }
                for (int i = -offSet; i <= offSet; i += offSet * 2)
                {
                    Vector2 initPosition = new Vector2(Game.game.gameSize.Width - i, 0);
                    new LaserBall(initPosition, Vector2.FromTargetObject(initPosition, Game.game.Mode.Player.Position, baseBulletSpeed));
                }
            }, true, false));
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            Image sprite = GetImage();
            Size = new Vector2(sprite.Width, sprite.Height);
            graphics.DrawImage(sprite, GetAnchorX(), GetAnchorY(), sprite.Width, sprite.Height);
        }

        public override bool IsPointOnPixel(Vector2 position)
        {
            return IsPointSuperposingSquare(position);
        }

        public override void DestroyPixel(Vector2 position)
        {
            return;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);
        }

    }
}
