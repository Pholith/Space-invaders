namespace SpaceInvaders.GameObjects
{

    class LivingEntity : GameObject
    {
        public LivingEntity(Vecteur2D v1, int hP) : base(v1)
        {
            HP = hP;
        }

        public int HP { get; private set; }

        public override void OnHit(Laser laser)
        {
            if (laser.CanHit(this))
            {
                laser.Kill();
                HP -= laser.Damage;
                if (HP <= 0)
                {
                    Kill();
                }

            }
        }
    }
}
