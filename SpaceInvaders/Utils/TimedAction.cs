using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// An Action is something a gameobject can do.
    /// 
    /// </summary>
    public class TimedAction
    {
        private double timer = 0;
        private double couldown;
        private readonly bool isAuto;
        private Delegate action;
        private bool ready = false;

        public TimedAction(double couldown, Action action, bool isAuto = false)
        {
            this.couldown = couldown;
            this.isAuto = isAuto;
            this.action = action;
        }

        public bool DoIfPossible()
        {
            if (ready)
            {
                action.DynamicInvoke();
                ready = false;
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
                
                if (isAuto) DoIfPossible();
            }
        }
    }
}
