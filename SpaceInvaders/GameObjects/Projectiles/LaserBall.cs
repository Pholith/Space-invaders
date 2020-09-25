using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class LaserBall : Laser
    {
        public LaserBall(Vecteur2D v1, Vecteur2D speed, Vecteur2D acceleration = null, DelegateMovement del = null, Tag tag = Tag.Invader) : base(v1, speed, tag)
        {
            if (acceleration is null) acceleration = Vecteur2D.zero;
            Acceleration = acceleration;
            this.del = del;
        }

        /// <summary>
        /// Describe a function to manage the movements of the balls without need to create a class for a new pattern
        /// </summary>
        /// <param name="ball">The laser</param>
        /// <param name="deltaT">The deltaT of the Update</param>
        /// <param name="increment">A var auto-incremented that can be usefull for computing</param>
        /// <returns> return the new Position </returns>
        public delegate Vecteur2D DelegateMovement(LaserBall ball, double deltaT, double increment);
        private DelegateMovement del = null;


        public override Bitmap GetImage()
        {
            Bitmap img = Resources.shootball;
            if (Tag == Tag.Player) img = Utils.Utils.RecolorImage(img, Color.DarkBlue);
            return img;
        }


        private double inc = 0;
        protected override void ApplyMovement(double deltaT)
        {
            base.ApplyMovement(deltaT);
            if (del != null) Position = del.Invoke(this, deltaT, inc);
            inc += 0.02;
        }

    }
}
