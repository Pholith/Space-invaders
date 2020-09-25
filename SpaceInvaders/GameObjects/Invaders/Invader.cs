using SpaceInvaders.GameObjects.Bonus;
using SpaceInvaders.Properties;
using SpaceInvaders.Utils;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Invaders
{
    /// <summary>
    /// An invader is the common ennemy of the game
    /// </summary>
    /// <seealso cref="LivingEntity" />
    class Invader : LivingEntity, IImage, IHitable
    {

        public Invader(Vecteur2D v1, int invaderType = 0, int hp = 1) : base(v1, hp)
        {
            this.invaderType = invaderType;
            Speed = new Vecteur2D(baseSpeed, 0);

        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);
            AddNewAction(new TimedAction(8 + gameInstance.random.NextDouble() * 12, () =>
            {
                if (shoot == null || !shoot.IsAlive()) shoot = new Laser(Position + new Vecteur2D(0, Size.Y), new Vecteur2D(0, 200));
            }, true));

        }

        public static int baseSpeed = 30;
        private int invaderType;
        private Laser shoot = null;

        public virtual Bitmap GetImage()
        {
            if (invaderType > 0)
            {
                object obj = Resources.ResourceManager.GetObject("ship" + invaderType);
                return (Bitmap)obj;
            }
            return new List<Bitmap>() {Resources.ship2,
                Resources.ship3,
                //Resources.ship4,
                Resources.ship5,
                Resources.ship6,
                Resources.ship7,
                Resources.ship8,
                Resources.ship9 }.GetRandom();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);
            if (GetAnchorY() > Game.game.gameSize.Height - 100)
            {
                Game.game.Mode.Lose();
            }

        }

        public override int GetNumberOfParticles()
        {
            return 4;
        }


        /// <summary>
        /// Switches the speed direction of invader and go down
        /// </summary>
        public void SwitchDirection()
        {
            Position = new Vecteur2D(Position.X, GetAnchorY() + Size.Y + 20);
            Speed = new Vecteur2D(-Speed.X, Speed.Y);
            // Increase the speed 
            int amount = 10;
            Speed = new Vecteur2D(Speed.X + ((Speed.X > 0) ? amount : -amount), Speed.Y);

        }

        /// <summary>
        /// Determines whether the invader is at the limite of border left or right.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the invader is on a border ; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOnBorder()
        {
            return GetAnchorX() < 0 && Speed.X < 0 || GetAnchorX() + Size.X > Game.game.gameSize.Width && Speed.X > 0;
        }


        /// <summary>
        /// Kills this instance and drop a bonus.
        /// </summary>
        public override void Kill()
        {
            base.Kill();
            double rand = Game.game.random.NextDouble();

            if (rand < 0.05) new AttackSpeedBonus(Position);
            else if (rand < 0.1) new BulletsBonus(Position);
            else if (rand < 0.2) new MegaShoot(Position);
            else if (rand < 0.25) new HealBonus(Position);
        }

        public override int GetScore()
        {
            return BaseHP;
        }
    }
}
