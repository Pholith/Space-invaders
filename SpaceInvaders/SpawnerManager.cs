using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpawnerManager : IActionsManager
    {

        public SpawnerManager()
        {
            Random r = new Random();
            // Creating a wave every 10 secondes
            AddNewAction(new TimedAction(15, () =>
            {
                if (waveCreated == 0)
                {
                    new BigBuggedBoss(new Vecteur2D(50, 50));
                }
                else if (waveCreated % 3 == 0)
                {
                    new InvaderBigBoss(new Vecteur2D(50, 50));
                }
                else
                {
                    // Creating 10 mob for this wave
                    int type = r.Next(2, 9);
                    AddNewAction(new TimedAction(0.8, () => new Invader(new Vecteur2D(10, 20), type), true, true, 20));
                }
                waveCreated ++;
            }
            , true, true));

        }
        int waveCreated = 0;

        #region Actions managment

        private HashSet<TimedAction> actions = new HashSet<TimedAction>();
        private HashSet<TimedAction> pendingNewActions = new HashSet<TimedAction>();

        public TimedAction AddNewAction(TimedAction action)
        {
            pendingNewActions.Add(action);
            return action;
        }
        #endregion


        public void Update(double deltaT)
        {
            foreach (var action in actions)
            {
                action.LoadTimer(deltaT);
            }
            actions.UnionWith(pendingNewActions);
            pendingNewActions.Clear();
            actions.RemoveWhere(action => action.Finished());
        }
    }
}
