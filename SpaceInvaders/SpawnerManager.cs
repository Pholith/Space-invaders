using SpaceInvaders.GameModes;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    class SpawnerManager : IActionsManager
    {

        public SpawnerManager()
        {
            // Creating a wave every 12 secondes
            if (Game.game.Mode is EndlessMode)
                AddNewAction(new TimedAction(12, () =>
                {
                    if ( waveCreated > 1 && waveCreated % 5 == 0)
                    {
                        AddNewAction(new TimedAction(5, () => new BigBuggedBoss(new Vecteur2D(50, 50)), true, false, 1));
                    }
                    else if (waveCreated > 1 && waveCreated % 3 == 0)
                    {
                        AddNewAction(new TimedAction(5, () => new InvaderBigBoss(new Vecteur2D(50, 50)), true, false, 1));
                    }
                    else
                    {
                        // Creating 10 mob for this wave
                        int type = Game.game.random.Next(2, 9);
                        AddNewAction(new TimedAction(0.9, () => new AutoInvader(new Vecteur2D(10, 20), type), true, true, 15));
                    }
                    waveCreated++;
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
        #endregion

    }
}
