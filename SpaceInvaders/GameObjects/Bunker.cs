using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A bunker is a object with life used to protect the player from lasers.
    /// </summary>
    public class Bunker : LivingEntity, IImage
    {
        public Bunker(Vector2 v1) : base(v1, -1)
        {

        }

        public override Tag GetTag()
        {
            return Tag.Neutral;
        }

        public Bitmap GetImage()
        {
            return Resources.bunker;
        }

        protected override int DestroyPixelSize()
        {
            return 1;
        }

    }
}
