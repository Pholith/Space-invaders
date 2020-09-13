
using SpaceInvaders;
using SpaceInvaders.Utils;
using System.Diagnostics;
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
                MoveLeft();
                gm.ReleaseKey(Keys.Q);
            }

            else if (gm.keyPressed.Contains(Keys.D))
            {
                MoveRight();
                gm.ReleaseKey(Keys.D);
            }
            else
            {
                Speed = Speed * 0.99; // Slow down when A or D is not pressed
            }
            if (gm.keyPressed.Contains(Keys.G))
            {
                gm.ReleaseKey(Keys.G);
                MessageBox.Show(gm.keyPressed.ToReadableList());
            }

            if (gm.keyPressed.Contains(Keys.Escape))
            {
                Shoot();
                gm.ReleaseKey(Keys.Space);
            }

            base.Update(gm, deltaT);
        }
    }
}
