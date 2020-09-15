using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Laser : GameObject, IImage
    {


        public int Damage { get; private set; }
        public Tag Tag { get; private set; }

        public Laser(Vecteur2D v1, Vecteur2D speed, Tag tag = Tag.Invader) : base(v1)
        {
            Speed = speed;
            Damage = 1;
            Tag = tag;
        }
        public Laser(Vecteur2D v1, Tag tag = Tag.Invader) : this(v1, new Vecteur2D(0, -200), tag)
        {

        }

        public virtual Bitmap GetImage()
        {
            return Resources.shoot1;
        }

        public bool CanHit(GameObject go)
        {
            // Lasers destroy lasers
            if (go.GetType() == GetType()) return true;

            return go.GetTag() != Tag;

        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            foreach (var obj in gameInstance.gameObjects)
            {
                if (GetAnchorX() > obj.GetAnchorX() && GetAnchorY() > obj.GetAnchorY() && GetAnchorY() < obj.GetAnchorY() + obj.Size.Y && GetAnchorX() < obj.GetAnchorX() + obj.Size.X)
                    //|| Position.X + Size.X < obj.Position.X && Position.Y < obj.Position.Y + obj.Size.Y && Posi)
                {
                    obj.OnHit(this);
                }
            }
        }
    }
}
