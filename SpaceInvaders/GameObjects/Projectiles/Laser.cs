using SpaceInvaders.GameModes;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A laser is a simple projectile shooted by the ship and the invaders.
    /// </summary>
    /// <seealso cref="Ship" />
    /// <seealso cref="AutoInvader" />
    public class Laser : GameObject, IImage
    {

        public int Damage { get; private set; }

        public Tag Tag { get; private set; }


        public static readonly int baseSpeed = 300;
        public Laser(Vector2 v1, Vector2 speed, Tag tag = Tag.Invader, double ttl = 15) : base(v1)
        {
            Speed = speed;
            Damage = 1;
            Tag = tag;
            this.ttl = ttl;
        }
        public Laser(Vector2 v1, Tag tag = Tag.Invader) : this(v1, new Vector2(0, -baseSpeed), tag)
        {

        }
        public override Tag GetTag()
        {
            return Tag;
        }

        public virtual Bitmap GetImage()
        {
            Bitmap img = Resources.shoot1;
            if (Tag == Tag.Player) img = Utils.Utils.RecolorImage(img, Color.DeepSkyBlue);
            return img;
        }

        public bool CanHit(GameObject go)
        {
            return go.GetTag() != Tag && go.GetTag() != Tag.Invincible;
        }

        private double ttl; // time to live
        private double ttlCount = 0;
        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            ttlCount += deltaT;
            if (ttlCount >= ttl) Kill();

            foreach (var obj in gameInstance.gameObjects)
            {
                if (obj == this || !CanHit(obj)) continue;
                if (!Game.game.Mode.IsNormalMode() && obj is Laser) continue;

                // If the squares intersect
                if (AreSquareSuperposing(obj))
                {
                    if (obj is Laser)
                    {
                        obj.OnHit(this);
                        Kill();
                        return;
                    }

                    bool hited = false;

                    // Check if a pixel of the laser is on a pixel of the gameobject
                    for (int i = (int)GetAnchorX(); i < GetAnchorX() + Size.X; i++)
                        for (int j = (int)GetAnchorY(); j < GetAnchorY() + Size.Y; j++)
                            // Destroy the pixel, apply hit and destroy the laser
                            if (obj.IsPointOnPixel(new Vector2(i, j)))
                            {
                                obj.DestroyPixel(new Vector2(i, j));
                                if (!hited)
                                {
                                    Kill();
                                    obj.OnHit(this);
                                    hited = true;
                                }
                            }

                }
            }
        }
    }
}
