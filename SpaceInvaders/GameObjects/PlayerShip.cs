
using SpaceInvaders;
using System.Windows.Forms;

namespace GameObjects
{
    class PlayerShip : Ship
    {
        public PlayerShip(Vecteur2D v) : base(v)
        {

        }

        public override void Update(Game gm, double deltaT)
        {
            if (gm.keyPressed.Contains(Keys.Q))
            {
                Speed = new Vecteur2D(-200, 0);
                gm.ReleaseKey(Keys.Q);
            }

            else if (gm.keyPressed.Contains(Keys.D))
            {
                Speed = new Vecteur2D(200, 0);
                gm.ReleaseKey(Keys.D);
            }
            else
            {
                Speed = Speed * 0.99; // Slow down when A or D is not pressed
            }


            base.Update(gm, deltaT);
        }
    }
}
