using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject : IHitable
    {

        public Vecteur2D Position { get; protected set; }
        public Vecteur2D Speed { get; protected set; }

        public Vecteur2D Size { get; protected set; } = Vecteur2D.zero;

        /// <summary>
        /// Allow to center images and hitbox test on the positionX
        /// </summary>
        /// <returns></returns>
        public float GetAnchorX()
        {
            return (float) (Position.X - Size.X / 2f);
        }
        public float GetAnchorY()
        {
            return (float) (Position.Y - Size.Y / 2f);
        }


        public GameObject(Vecteur2D v1 = null)
        {
            if (v1 is null) v1 = Vecteur2D.zero;

            Position = v1;
            Speed = Vecteur2D.zero;
            Game.game.AddNewGameObject(this);
        }

        /// <summary>
        /// Init the gameobjects for things that cannot be init in the constructor (some actions)
        /// </summary>
        /// <param name="gameInstance"> instance of the current game </param>
        public virtual void Init(Game gameInstance)
        {

        }

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public virtual void Update(Game gameInstance, double deltaT)
        {
            // Load deltaT in functions that need to wait and other actions managment
            foreach (var action in actions)
            {
                action.LoadTimer(deltaT);
            }
            actions.UnionWith(pendingNewActions);
            pendingNewActions.Clear();
            actions.RemoveWhere(action => action.Finished());

            Position = Position + Speed * deltaT;

            // Automaticly kill gameobject if out of screen
            if (Position.X < -10 || Position.X > gameInstance.gameSize.Width + 10 || Position.Y < 0 || Position.Y > gameInstance.gameSize.Height + 10) Kill();
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
            if (sprite == null && this is IImage)
            {
                IImage go = this as IImage;
                sprite = go.GetImage();//.GetThumbnailImage(45, 45, null, IntPtr.Zero);
                Size = new Vecteur2D(sprite.Width, sprite.Height);
            }
            if (sprite != null)
            {
                graphics.DrawImage(sprite, GetAnchorX(), GetAnchorY(), sprite.Width, sprite.Height);
                graphics.DrawRectangle(new Pen(Color.Blue, 1), GetAnchorX(), GetAnchorY(), sprite.Width, sprite.Height);
                graphics.DrawEllipse(new Pen(Color.Red, 3), GetAnchorX(), GetAnchorY(), 1, 1);
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

        public virtual bool OnHit(Laser laser)
        {
            Kill();
            laser.Kill();
            return false;
        }


        #region Actions managment

        private HashSet<TimedAction> actions = new HashSet<TimedAction>();
        private HashSet<TimedAction> pendingNewActions = new HashSet<TimedAction>();

        public TimedAction AddNewAction(TimedAction action)
        {
            pendingNewActions.Add(action);
            return action;
        }
        #endregion

    }
}
