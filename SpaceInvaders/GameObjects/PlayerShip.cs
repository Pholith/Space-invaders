
using SpaceInvaders;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// A ship that can be controlled using inputs
    /// </summary>
    /// <seealso cref="Ship" />
    class PlayerShip : Ship
    {
        public PlayerShip(Vecteur2D v) : base(v)
        {

        }

        public override void Update(Game gm, double deltaT)
        {

            if (gm.keyPressed.Contains(Keys.Q) || gm.keyPressed.Contains(Keys.Left))
            {
                MoveLeft();
                //gm.ReleaseKey(Keys.Q);
            }

            if (gm.keyPressed.Contains(Keys.D) || gm.keyPressed.Contains(Keys.Right))
            {
                MoveRight();
                //gm.ReleaseKey(Keys.D);
            } 
            if (gm.keyPressed.Contains(Keys.Z) || gm.keyPressed.Contains(Keys.Up))
            {
                MoveUp();
                //gm.ReleaseKey(Keys.Z);
            }
            if (gm.keyPressed.Contains(Keys.S) || gm.keyPressed.Contains(Keys.Down))
            {
                MoveDown();
                //gm.ReleaseKey(Keys.S);
            }
            Speed = Speed * 0.95; // Slow down every tic

            if (gm.keyPressed.Contains(Keys.Space))
            {
                UseMegaShoot();
                if (Game.game.Mode.IsNormalMode()) Shoot();
                //gm.ReleaseKey(Keys.Space);
            }
#if DEBUG
            if (gm.keyPressed.Contains(Keys.B))
            {
                Ship p = Game.game.Mode.Player;
                p.AddBullet();
                p.AddBullet();
                p.AddBullet();
                p.AddBullet();
                p.ApplyAttackSpeedBonus();
                p.ApplyAttackSpeedBonus();
                p.ApplyAttackSpeedBonus();
                p.ApplyHealBonus();
            }
            if (gm.keyPressed.Contains(Keys.I))
            {
                 Game.game.Mode.Player.ToggleInvicibility();
            }
#endif
            if (!Game.game.Mode.IsNormalMode()) Shoot(); // Automaticly shoot in others modes
            base.Update(gm, deltaT);
        }
    }
}
