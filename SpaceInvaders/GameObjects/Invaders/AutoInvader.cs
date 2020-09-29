using SpaceInvaders.GameObjects.Invaders;

namespace SpaceInvaders.GameObjects
{

    /// <summary>
    /// An AutoInvader is an invider which isn't in a invader block.
    /// </summary>
    /// <seealso cref="Invader" />
    /// <seealso cref="InvaderBlock" />
    class AutoInvader : Invader
    {
        public AutoInvader(Vecteur2D position, int invaderType = 0, int hp = 1, Vecteur2D speed = null) : base(position, invaderType, hp)
        {
            if (!(speed is null)) Speed = speed;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);


            if (IsOnBorder())
            {
                SwitchDirection();
            }
        }

    }
}
