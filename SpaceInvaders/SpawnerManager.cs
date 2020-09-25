using SpaceInvaders.GameModes;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Invaders;
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
            if (Game.game.Mode is ManicShooter)
                AddNewAction(new TimedAction(13, () =>
                {
                    if (waveCreated > 7 && waveCreated % 3 == 0)
                    {
                        AddNewAction(new TimedAction(3, () => new UltimateBoss(new Vecteur2D(50, 0)), true, false, 1));
                    }
                    else if (waveCreated > 4 && waveCreated % 2 == 0)
                    {
                        AddNewAction(new TimedAction(3.5, () => new InvaderBigBoss(new Vecteur2D(50, 50)), true, false, 5));
                    }
                    else if (waveCreated > 1 && waveCreated % 5 == 0)
                    {
                        new AutoInvader(new Vecteur2D(10, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                        new AutoInvader(new Vecteur2D(40, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                        AddNewAction(new TimedAction(3, () => new BigBuggedBoss(new Vecteur2D(50, 50)), true, false, 2));
                    }
                    else if (waveCreated >= 4)
                    {
                        int type = Game.game.random.Next(2, 9);
                        AddNewAction(new TimedAction(0.5, () =>
                            new AutoInvader(new Vecteur2D(10, 20), type, hp: 2, speed: new Vecteur2D(Invader.baseSpeed * 4, 20)), true, true, 50));
                    }
                    else
                    {
                        // Creating 10 mob for this wave
                        int type = Game.game.random.Next(2, 9);
                        AddNewAction(new TimedAction(0.7, () => 
                            new AutoInvader(new Vecteur2D(10, 20), type, speed: new Vecteur2D(Invader.baseSpeed * 2, 0)), true, true, 20));
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
