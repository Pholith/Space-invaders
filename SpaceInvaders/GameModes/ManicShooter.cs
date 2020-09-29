using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Utils;
using System.Collections.Generic;

namespace SpaceInvaders.GameModes
{
    class ManicShooter : GameMode
    {
        private int waveCreated = 0;
        private Dictionary<int, TimedAction> map;

        public ManicShooter() : base()
        {
            map = new Dictionary<int, TimedAction>();

            // Add some predefined waves 
            map.Add(0, new TimedAction(0, () =>
            {
                // Creating 10 mob for this wave
                int type = Game.game.random.Next(2, 9);
                AddNewAction(new TimedAction(0.7, () =>
                    new AutoInvader(new Vecteur2D(10, 20), type, speed: new Vecteur2D(Invader.baseSpeed * 2, 0)), true, true, 20));

            }, true, true, 1));


            map.Add(1, new TimedAction(0, () =>
            {
                int type = Game.game.random.Next(2, 9);
                AddNewAction(new TimedAction(0.5, () =>
                    new AutoInvader(new Vecteur2D(10, 20), type, hp: 2, speed: new Vecteur2D(Invader.baseSpeed * 4, 20)), true, true, 50));
            }, true, true, 1));


            map.Add(2, new TimedAction(0, () =>
            {
                new AutoInvader(new Vecteur2D(10, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                new AutoInvader(new Vecteur2D(40, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                AddNewAction(new TimedAction(3, () => new BigBuggedBoss(new Vecteur2D(50, 50)), true, false, 2));
            }, true, true, 1));


            map.Add(3, new TimedAction(3, () => new InvaderBigBoss(new Vecteur2D(50, 50)), true, false, 5));

            map.Add(4, new TimedAction(3, () => new UltimateBoss(new Vecteur2D(50, 0)), true, false, 1));


            // Setup the spawn waves
            AddNewAction(new TimedAction(14, () =>
            {
                int rand = 0;
                if (waveCreated < 4) rand = 0;
                else if (waveCreated < 8) rand = Game.game.random.Next(1, 4);
                else rand = Game.game.random.Next(1, 5);
                AddNewAction((TimedAction) map[rand].Clone());
                waveCreated++;

            }, true, true));

        }

        public override void Update(double deltaT)
        {
            base.Update(deltaT);
            ManageActions(deltaT);
        }

        #region Actions managment

        private HashSet<TimedAction> actions = new HashSet<TimedAction>();
        private HashSet<TimedAction> pendingNewActions = new HashSet<TimedAction>();

        public TimedAction AddNewAction(TimedAction action)
        {
            pendingNewActions.Add(action);
            return action;
        }
        public void ManageActions(double deltaT)
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
