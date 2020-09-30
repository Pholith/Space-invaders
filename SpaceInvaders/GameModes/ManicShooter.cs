using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Utils;
using System.Collections.Generic;

namespace SpaceInvaders.GameModes
{
    class ManicShooter : GameMode
    {
        private int waveCreated;
        private Dictionary<int, TimedAction> map;

        public ManicShooter() : base()
        {
        }

        public override void Init()
        {
            base.Init();
            waveCreated = 0;
            map = new Dictionary<int, TimedAction>();

            // Add some predefined waves 
            map.Add(0, new TimedAction(0, () =>
            {
                int type = Game.game.random.Next(2, 9);
                bool side = Game.game.random.NextBool();
                AddNewAction(new TimedAction(0.7, () =>
                    new AutoInvader(new Vecteur2D(side ? 0 : Game.game.gameSize.Width, 20),
                        type, speed: new Vecteur2D((side ? Invader.baseSpeed : -Invader.baseSpeed) * 2, 0)), true, true, 15));

            }, true, true, 1));

            map.Add(1, new TimedAction(4, () =>
            {
                for (int i = 0; i < (Game.game.gameSize.Width - Invader.baseSize) / Invader.baseSize; i++)
                {
                    new AutoInvader(new Vecteur2D(Invader.baseSize + i * Invader.baseSize, 0), 0, hp: 2, speed: new Vecteur2D(0, Invader.baseSpeed / 2));
                }
            }, true, true, 3));

            map.Add(2, new TimedAction(0, () =>
            {
                int type = Game.game.random.Next(2, 9);
                bool side = Game.game.random.NextBool();
                AddNewAction(new TimedAction(0.5, () =>
                    new AutoInvader(new Vecteur2D(side ? 0 : Game.game.gameSize.Width, 20), type,
                    hp: 2, speed: new Vecteur2D((side ? Invader.baseSpeed : -Invader.baseSpeed) * 4, 20)), true, true, 40));
            }, true, true, 1));


            map.Add(3, new TimedAction(0, () =>
            {
                new AutoInvader(new Vecteur2D(0, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                new AutoInvader(new Vecteur2D(Invader.baseSize, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));

                AddNewAction(new TimedAction(3, () => new BigBuggedBoss(new Vecteur2D(0, 50)), true, false, 1));
            }, true, true, 1));


            map.Add(4, new TimedAction(7, () => new InvaderBigBoss(new Vecteur2D(0, 50)), true, false, 3));

            map.Add(5, new TimedAction(3, () => new UltimateBoss(new Vecteur2D(0, Invader.baseSize)), true, false, 1));


            // Setup the spawn waves
            AddNewAction(new TimedAction(16, () =>
            {
                int rand = 0;
                if (waveCreated < 2) rand = 0;
                else if (waveCreated < 4) rand = Game.game.random.Next(1, 3);
                else if (waveCreated < 8) rand = Game.game.random.Next(1, 4);
                else rand = Game.game.random.Next(4, 6);

                //rand = 3;

                AddNewAction((TimedAction)map[rand].Clone());
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
