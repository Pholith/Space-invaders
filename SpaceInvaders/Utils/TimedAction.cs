using System;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// An Action is something a gameobject can do.
    /// </summary>
    public class TimedAction
    {
        private double timer = 0;
        private double couldown;
        private readonly bool isAuto;
        private Delegate action;
        private bool ready = false;
        
        /// <summary>
        /// Set a limit of calling this action, -1 is unlimited 
        /// </summary>
        private int limitOfCall;
        
        public TimedAction(double couldown, Action action, bool isAuto = false, bool readyOnStart = false, int limitOfCall = -1)
        {
            ready = readyOnStart;
            this.couldown = couldown;
            this.isAuto = isAuto;
            this.action = action;
            this.limitOfCall = limitOfCall;
        }

        public bool DoIfPossible()
        {
            if (ready && limitOfCall != 0)
            {
                action.DynamicInvoke();
                ready = false;
                limitOfCall -= 1;
                timer = 0;
                return true;
            }
            return false;
        }

        public void LoadTimer(double deltaT)
        {
            timer += deltaT;
            if (timer >= couldown)
            {
                timer = 0;
                ready = true;
            }
            if (isAuto) DoIfPossible();

        }

        public bool Finished()
        {
            return limitOfCall == 0;
        }
    }
}
