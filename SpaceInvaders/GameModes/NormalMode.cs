using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Invaders;
using System;
using System.Linq;

namespace SpaceInvaders.GameModes
{
    class NormalMode : GameMode
    {
        public NormalMode()
        {
        }
        public override void Init()
        {
            base.Init();
            new InvaderBlock();
        }

        public override void Update(double deltaT)
        {
            base.Update(deltaT);
            if (Game.game.gameObjects.All((GameObject obj) => !(obj is Invader)))
            {
                Win = true;
                Ended = true;
            }

        }
    }
}
