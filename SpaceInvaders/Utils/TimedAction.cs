using System;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// An Action is something a gameobject can do.
    /// </summary>
    public class TimedAction : ICloneable
    {

        public double Couldown { get; set; }
        public Action Action { get; set; }

        private double timer = 0;
        private readonly bool isAuto;
        
        private bool ready = false;
        
        /// <summary>
        /// Set a limit of calling this action, -1 is unlimited 
        /// </summary>
        private int limitOfCall;
        
        public TimedAction(double couldown, Action action, bool isAuto = false, bool readyOnStart = false, int limitOfCall = -1)
        {
            ready = readyOnStart;
            Couldown = couldown;
            Action = action;
            this.isAuto = isAuto;
            this.limitOfCall = limitOfCall;
        }

        /// <summary>
        /// Do the action
        /// </summary>
        /// <returns></returns>
        public bool DoIfPossible()
        {
            if (ready && limitOfCall != 0)
            {
                Action.Invoke();
                ready = false;
                limitOfCall -= 1;
                timer = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Do the actions even if it is not ready. Should be use only if you know what you're doing <see cref="DoIfPossible"/>
        /// </summary>
        public void ForceAction()
        {
            ready = true;
            DoIfPossible();
        }

        public void LoadTimer(double deltaT)
        {
            timer += deltaT;
            if (timer >= Couldown)
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

        public object Clone()
        {
            return new TimedAction(Couldown, Action, isAuto, ready, limitOfCall);
        }
    }
}
