using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Invaders;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameModes
{
    class ManicShooter : GameMode
    {
        private int waveCreated;
        private bool spawnLocked;
        private Dictionary<int, TimedAction> map;

        public ManicShooter() : base()
        {
        }

        private bool AllEnemyDestroyed()
        {
            foreach (GameObject go in Game.game.gameObjects)
            {
                if (go is Invader) return false;
            }
            spawnLocked = false;
            return true;
        }

        public override void Init()
        {
            actions.Clear();

            base.Init();
            waveCreated = 0;
            spawnLocked = false;
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
                    hp: 2, speed: new Vecteur2D((side ? Invader.baseSpeed : -Invader.baseSpeed) * 4, 20)), true, true, 30));
            }, true, true, 1));


            map.Add(3, new TimedAction(0, () =>
            {
                if (Game.game.random.NextDouble() < 0.15)
                {
                    new AutoInvader(new Vecteur2D(0, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                    new AutoInvader(new Vecteur2D(Invader.baseSize, 20), 0, speed: new Vecteur2D(Invader.baseSpeed * 2, 20));
                    AddNewAction(new TimedAction(3, () => new BigBuggedBoss(new Vecteur2D(0, 50)), true, false, 1));
                } else
                {
                    for (int i = 0; i < (Game.game.gameSize.Width - Invader.baseSize) / Invader.baseSize; i++)
                    {
                        new AutoInvader(new Vecteur2D(Invader.baseSize + i * Invader.baseSize, 0), 0, hp: 2, speed: new Vecteur2D(0, Invader.baseSpeed));
                    }
                }
            }, true, true, 1));


            map.Add(4, new TimedAction(8, () => {
                spawnLocked = true;
                new InvaderBigBoss(new Vecteur2D(0, 50));
            }, true, false, 2));

            map.Add(5, new TimedAction(3, () => {
                spawnLocked = true;
                new UltimateBoss(new Vecteur2D(0, Invader.baseSize));
            }, true, false, 1));

            map.Add(6, new TimedAction(3, () => {
                spawnLocked = true;
                new BulletSpammerBoss(new Vecteur2D(Game.game.gameSize.Width / 2, Game.game.gameSize.Height / 4));
            }, true, false, 1));
        


            // Setup the spawn waves
            waveSpawnAction = AddNewAction(new TimedAction(25, () =>
            {
                if (spawnLocked) return;
                int rand = 0;
                if (waveCreated < 1) rand = 0;
                else if (waveCreated < 2) rand = Game.game.random.Next(1, 4);
                else if (waveCreated < 3) rand = Game.game.random.Next(2, 5);
                else rand = Game.game.random.Next(3, 7);

                Debug.Print("New wave - Rand=" + rand.ToString() + " waveCreated=" + waveCreated);

                if (rand != -1) AddNewAction((TimedAction)map[rand].Clone());
                waveCreated++;

            }, true, true));

        }

        private TimedAction waveSpawnAction;
        public override void Update(double deltaT)
        {
            // Check if the player finished the wave to start another - Should not be used after ManageActions because it need to compute the number of enemy in Game
            if (AllEnemyDestroyed() && actions.Count == 1 && pendingNewActions.Count == 0) waveSpawnAction.ForceAction();

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
