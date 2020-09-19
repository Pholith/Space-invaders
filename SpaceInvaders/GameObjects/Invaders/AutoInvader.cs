using SpaceInvaders.GameObjects.Invaders;

namespace SpaceInvaders.GameObjects
{
    class AutoInvader : Invader
    {
        public AutoInvader(Vecteur2D position, int invaderType = 0) : base(position, invaderType)
        {

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
