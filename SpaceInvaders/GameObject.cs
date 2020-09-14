using SpaceInvaders.Utils;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {

        public Vecteur2D Position { get; protected set; }
        public Vecteur2D Speed { get; protected set; }

        public Vecteur2D Size { get; protected set; } = Vecteur2D.zero;

        protected Dictionary<string, TimedAction> actions = new Dictionary<string, TimedAction>();
        public float GetMiddleX()
        {
            return (float)(Position.X - Size.X / 2f);
        }
        public float GetMiddleY()
        {
            return (float)(Position.Y - Size.Y / 2f);
        }


        public GameObject(Vecteur2D v1 = null)
        {
            if (v1 is null) v1 = Vecteur2D.zero;

            Position = v1;
            Speed = Vecteur2D.zero;
            Game.game.AddNewGameObject(this);

            if (this is IImage)
            {
                IImage go = this as IImage;
                sprite = go.GetImage();//.GetThumbnailImage(45, 45, null, IntPtr.Zero);
                Size = new Vecteur2D(sprite.Width, sprite.Height);
            }
        }


        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public virtual void Update(Game gameInstance, double deltaT)
        {
            Position = Position + Speed * deltaT;
            // Load deltaT in functions that need to wait
            foreach (var action in actions.Values)
            {
                action.LoadTimer(deltaT);
            }
        }
        /// <summary>
        /// Field to store image to not read it at every frame
        /// </summary>
        Image sprite;
        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public virtual void Draw(Game gameInstance, Graphics graphics)
        {
            if (sprite != null)
            {
                graphics.DrawImage(sprite, GetMiddleX(), GetMiddleY(), sprite.Width, sprite.Height);
                graphics.DrawRectangle(new Pen(Color.Blue, 1), GetMiddleX(), GetMiddleY(), sprite.Width, sprite.Height);
                graphics.DrawEllipse(new Pen(Color.Red, 3), GetMiddleX(), GetMiddleY(), 1, 1);
                graphics.DrawEllipse(new Pen(Color.Green, 3), (float)Position.X, (float)Position.Y, 1, 1);
            }
        }



        private bool alive = true;
        public void Kill()
        {
            alive = false;
        }
        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public virtual bool IsAlive()
        {
            return alive;
        }

    }
}
