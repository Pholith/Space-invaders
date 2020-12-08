using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class LaserBall : Laser
    {
        public LaserBall(Vector2 position, Vector2 speed, Vector2 acceleration = null, DelegateMovement del = null, Tag tag = Tag.Invader, bool overrideMovement = false, int ttl = 15) : base(position, speed, tag, ttl:ttl)
        {
            if (acceleration is null) acceleration = Vector2.zero;
            Acceleration = acceleration;
            this.del = del;
            this.overrideMovement = overrideMovement;
        }

        /// <summary>
        /// Describe a function to manage the movements of the balls without need to create a class for a new pattern.
        /// </summary>
        /// <param name="ball">The laser object</param>
        /// <param name="deltaT">The deltaT of the Update</param>
        /// <param name="increment">A var auto-incremented that can be usefull for computing (incremented by 0.02) </param>
        /// <returns> return the new Position </returns>
        public delegate Vector2 DelegateMovement(LaserBall ball, double deltaT, double increment);
        private DelegateMovement del = null;


        public override Bitmap GetImage()
        {
            Bitmap img = Resources.shootball;
            if (Tag == Tag.Player) img = Utils.Utils.RecolorImage(img, Color.DeepSkyBlue);
            return img;
        }

        private bool overrideMovement;
        private double inc = 0;
        protected override void ApplyMovement(double deltaT)
        {
            if (!overrideMovement) base.ApplyMovement(deltaT);
            if (del != null) Position = del.Invoke(this, deltaT, inc);
            inc += 0.02;
        }

    }
}
