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
    /// <seealso cref="AutoInvader" />
    public class Invader : LivingEntity, IImage, IHitable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Invader"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="invaderType">The skin of the invader.</param>
        /// <param name="hp">The hp.</param>
        public Invader(Vector2 position, int invaderType = 0, int hp = 1) : base(position, hp)
        {
            this.invaderType = invaderType;
            Speed = new Vector2(baseSpeed, 0);

        }

        public override void Init(Game gameInstance)
        {
            base.Init(gameInstance);
            AddNewAction(new TimedAction(6 + gameInstance.random.NextDouble() * 10, () =>
            {
                if (shoot == null || !shoot.IsAlive()) shoot = new Laser(Position + new Vector2(0, Size.Y), new Vector2(0, baseBulletSpeed));
            }, true));

        }
        public static readonly int baseBulletSpeed = 120;
        public static readonly int baseSize = 45;
        public static readonly int baseSpeed = 30;
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
            if (GetAnchorY() > Game.game.gameSize.Height - 40)
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
            Position = new Vector2(Position.X, GetAnchorY() + Size.Y + 20);
            Speed = new Vector2(-Speed.X, Speed.Y);
            // Increase the speed 
            int amount = 10;
            Speed = new Vector2(Speed.X + ((Speed.X > 0) ? amount : -amount), Speed.Y);

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
