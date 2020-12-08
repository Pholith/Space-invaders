namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A living Entity is a gameobject with HPs.
    /// </summary>
    /// <seealso cref="GameObject" />
    public class LivingEntity : GameObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LivingEntity"/> class.
        /// </summary>
        /// <param name="v1"> Position of the bunker </param>
        /// <param name="hP"> Amount of HP (-1 for infinite) .</param>
        public LivingEntity(Vector2 v1, int hP) : base(v1)
        {
            HP = hP;
            BaseHP = hP;
        }


        public int HP { get; protected set; }
        public int Power { get; protected set; }
        protected int BaseHP { get; private set; }

        public override void OnHit(Laser laser)
        {
            if (HP == -1) return;
            HP -= laser.Damage;
            if (HP <= 0)
            {
                Kill();
            }
        }
    }
}
