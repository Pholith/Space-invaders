using System;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {

        /// <summary>
        /// Position
        /// </summary>
        public Vecteur2D Position { get; private set; }
        public Vecteur2D Speed { get; protected set; }

        public GameObject(Vecteur2D v1 = null)
        {
            if (v1 is null) v1 = Vecteur2D.zero;

            Position = v1;
            Speed = Vecteur2D.zero;
            Game.game.AddNewGameObject(this);
        }



        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public virtual void Update(Game gameInstance, double deltaT)
        {
            Position = Position + Speed * deltaT;
        }

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);



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
