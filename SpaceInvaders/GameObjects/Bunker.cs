using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Bunker : LivingEntity, IImage
    {
        public Bunker(Vecteur2D v1) : base(v1, -1)
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


    }
}
